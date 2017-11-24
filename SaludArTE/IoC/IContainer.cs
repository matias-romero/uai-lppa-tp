using System;

namespace SaludArTE.IoC
{
    public interface IContainer : IDisposable
    {
        T GetInstance<T>();
        object GetInstance(Type type);
    }
}