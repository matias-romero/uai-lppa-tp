namespace SaludArTE.Entities.RedundancyCheck
{
    public interface IEntityWithRedundancyCheck
    {
        byte[] CRC { get; set; }
    }
}
