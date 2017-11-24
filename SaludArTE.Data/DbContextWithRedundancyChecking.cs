using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SaludArTE.Data.RedundancyCheck;
using System.Data.Entity.Infrastructure;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data
{
    /// <inheritdoc />
    /// <summary>
    /// Ofrece una capa de abstracción para abstraer al Contexto final de las cuestiones relacionadas con los dígitos verificadores
    /// </summary>
    public abstract class DbContextWithRedundancyChecking : System.Data.Entity.DbContext
    {
        private readonly IRedundancyCheckAnalyzer _redundancyCheckAnalyzer;

        public DbContextWithRedundancyChecking(string cnnString, IRedundancyCheckAnalyzer redundancyCheckAnalyzer)
            :base(cnnString)
        {
            _redundancyCheckAnalyzer = redundancyCheckAnalyzer;
        }

        public override int SaveChanges()
        {
            var affectedTypes = this.RecalculateCRCs();
            var retCode = base.SaveChanges();
            this.UpdateVerticalCheckDigits(affectedTypes);
            return retCode;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var affectedTypes = this.RecalculateCRCs();
            var retCode = base.SaveChangesAsync(cancellationToken);
            this.UpdateVerticalCheckDigits(affectedTypes);
            return retCode;
        }

        private Type[] RecalculateCRCs()
        {
            var affectedEntityGroupTypes = new List<Type>();
            var added = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Added);
            var deleted = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);
            var modified = this.ChangeTracker.Entries().Where(e => e.State == EntityState.Modified);

            this.RecalculateIndividualCRCs(affectedEntityGroupTypes, added.Concat(modified)); //Afectan cada Digito Horizontal
            this.RecalculateGroupCRCs(affectedEntityGroupTypes, deleted); //Afectan solo al Digito Vertical
            return affectedEntityGroupTypes.Distinct().ToArray();
        }

        private void RecalculateIndividualCRCs(IList<Type> affectedEntityGroupTypes, IEnumerable<DbEntityEntry> entries)
        {
            foreach (var dbEntityEntry in entries)
            {
                var entityWithCRC = dbEntityEntry.Entity as IEntityWithRedundancyCheck;
                if (entityWithCRC != null)
                {
                    entityWithCRC.CRC = _redundancyCheckAnalyzer.CalculateHashForEntity(entityWithCRC);
                    var entityType = GetEntityType(entityWithCRC);
                    affectedEntityGroupTypes.Add(entityType);
                }
            }
        }

        private void UpdateVerticalCheckDigits(Type[] affectedTypes)
        {
            foreach (var affectedGroupType in affectedTypes)
            {
                var entityName = affectedGroupType.FullName;
                var checksum = this.CalculateChecksumForEntityType(affectedGroupType);
                var vcd = this.Set<VerticalCheckDigit>().Find(entityName);
                if (vcd == null)
                {
                    vcd = this.Set<VerticalCheckDigit>().Create();
                    this.Set<VerticalCheckDigit>().Add(vcd);
                }
                
                vcd.EntityName = entityName;
                vcd.Checksum = checksum;
            }

            //Vuelvo a grabar los cambios de los dígitos verificadores verticales
            base.SaveChanges();
        }

        private static Type GetEntityType(IEntityWithRedundancyCheck entityWithCRC)
        {
            return System.Data.Entity.Core.Objects.ObjectContext.GetObjectType(entityWithCRC.GetType());
        }

        private void RecalculateGroupCRCs(IList<Type> affectedEntityGroupTypes, IEnumerable<DbEntityEntry> entries)
        {
            foreach (var dbEntityEntry in entries)
            {
                var entityWithCRC = dbEntityEntry.Entity as IEntityWithRedundancyCheck;
                if (entityWithCRC != null)
                    affectedEntityGroupTypes.Add(GetEntityType(entityWithCRC));

            }  
        }

        private byte[] CalculateChecksumForEntityType(Type entityType)
        {
            var crcs = new List<byte[]>();
            foreach (IEntityWithRedundancyCheck entity in this.Set(entityType))
                crcs.Add(entity.CRC);

            var verticalChecksum = _redundancyCheckAnalyzer.CalculateHashFromMultipleCRCs(crcs);
            return verticalChecksum;
        }
    }
}
