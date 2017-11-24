using System;

namespace SaludArTE.Entities.RedundancyCheck
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class FieldMarkedForRedundancyAttribute : Attribute
    {
        public int Order { get; set; }
    }
}
