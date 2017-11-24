using StructureMap;

namespace SaludArTE.IoC
{
    public class BllRegistry : Registry
    {
        public BllRegistry()
        {
            this.Scan(x =>
            {
                x.TheCallingAssembly();
                x.AssemblyContainingType<BLL.Appointments>();
                x.WithDefaultConventions();
            });
        }
    }
}