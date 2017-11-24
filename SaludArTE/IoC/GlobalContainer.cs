using System;

namespace SaludArTE.IoC
{
    public class GlobalContainer : IContainer
    {
        private readonly StructureMap.IContainer _container;
        
        #region "Singleton"

        private static readonly Lazy<GlobalContainer> GlobalContainerInstance = new Lazy<GlobalContainer>(() => new GlobalContainer());
        public static GlobalContainer DefaultInstance
        {
            get { return GlobalContainerInstance.Value; }
        }
        
        #endregion

        public GlobalContainer()
        {
            _container = new StructureMap.Container(_ =>
            {
                _.AddRegistry<DataRegistry>();
                _.AddRegistry<BllRegistry>();
            });
        }

        private GlobalContainer(StructureMap.IContainer structureMapContainer)
        {
            _container = structureMapContainer;
        }

        public IContainer GetNestedContainer()
        {
            return new GlobalContainer(_container.GetNestedContainer());
        }

        internal StructureMap.IContainer GetStructureMapContainer()
        {
            return _container;
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public object GetInstance(Type type)
        {
            return _container.GetInstance(type);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}