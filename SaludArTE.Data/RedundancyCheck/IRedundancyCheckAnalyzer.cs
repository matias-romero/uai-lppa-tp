using System.Collections.Generic;
using SaludArTE.Entities.RedundancyCheck;

namespace SaludArTE.Data.RedundancyCheck
{
    public interface IRedundancyCheckAnalyzer
    {
        bool IsValid(IEntityWithRedundancyCheck entity);
        byte[] CalculateHashForEntity(IEntityWithRedundancyCheck entity);
        byte[] CalculateHashFromMultipleCRCs(IEnumerable<byte[]> crcs);
    }
}