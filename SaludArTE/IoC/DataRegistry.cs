using System;
using SaludArTE.Data;
using StructureMap;

namespace SaludArTE.IoC
{
    public class DataRegistry : Registry
    {
        public DataRegistry()
        {
            this.Scan(x =>
            {
                x.AssemblyContainingType<Data.IDbContext>();
                x.WithDefaultConventions();
            });

            this.For<IDbContext>().Use(container => container.GetInstance<IUnitOfWorkHelper>().DbContext);
        }
    }
}