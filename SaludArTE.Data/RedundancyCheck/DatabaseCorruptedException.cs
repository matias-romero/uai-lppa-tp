using System;
using System.Collections.Generic;
using System.Linq;

namespace SaludArTE.Data.RedundancyCheck
{
    /// <summary>
    /// Modela la excepción para cuando se detectó alguna inconsistencia con los dígitos verificadores
    /// </summary>
    public class DatabaseCorruptedException : Exception
    {
        public DatabaseCorruptedException(IEnumerable<string> affectedEntities)
        {
            this.AffectedEntities = affectedEntities.ToArray();
        }

        public string[] AffectedEntities { get; private set; }
    }
}
