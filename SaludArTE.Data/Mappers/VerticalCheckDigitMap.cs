using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data.Mappers
{
    public class VerticalCheckDigitMap : EntityTypeConfiguration<VerticalCheckDigit>
    {
        public VerticalCheckDigitMap()
        {
            this.HasKey(l => l.EntityName);
            this.Property(l => l.EntityName)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
