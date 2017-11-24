using System;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Entities
{
    public class Appointment : IEntityWithRedundancyCheck
    {
        public Guid Id { get; set; }

        [FieldMarkedForRedundancy(Order = 1)]
        public string Title { get; set; }

        [FieldMarkedForRedundancy(Order = 2)]
        public DateTime Start { get; set; }

        [FieldMarkedForRedundancy(Order = 3)]
        public DateTime? End { get; set; }

        public string Url { get; set; }

        public byte[] CRC { get; set; }
    }
}
