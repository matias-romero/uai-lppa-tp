using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data.RedundancyCheck
{
    public class RedundancyCheckAnalyzer : IRedundancyCheckAnalyzer
    {
        private Hash _hash = new Hash();

        public bool IsValid(IEntityWithRedundancyCheck entity)
        {
            var calculatedHash = this.CalculateHashForEntity(entity);
            var currentHash = entity.CRC ?? new byte[0];
            return calculatedHash.SequenceEqual(currentHash);
        }

        public byte[] CalculateHashFromMultipleCRCs(IEnumerable<byte[]> crcs)
        {
            return _hash.CreateHash(crcs);
        }

        public byte[] CalculateHashForEntity(IEntityWithRedundancyCheck entity)
        {
            var seed = new StringBuilder();
            var entityType = entity.GetType();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(entityType))
            {
                var fieldMarkedForRc = propertyDescriptor.Attributes
                    .OfType<FieldMarkedForRedundancyAttribute>()
                    .FirstOrDefault();

                if (fieldMarkedForRc != null)
                {
                    //TODO: Verificar el orden del armado de la semilla
                    var propertyValueSerialized = Convert.ToString(propertyDescriptor.GetValue(entity) ?? string.Empty);
                    seed.Append(propertyValueSerialized);
                }
            }

            return _hash.CreateHash(seed.ToString());
        }
    }
}
