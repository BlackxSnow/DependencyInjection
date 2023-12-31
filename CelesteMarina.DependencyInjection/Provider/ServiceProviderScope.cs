using System;
using System.Collections.Generic;
using CelesteMarina.DependencyInjection.Injector;
using CelesteMarina.DependencyInjection.Service;

namespace CelesteMarina.DependencyInjection.Provider
{
    public class ServiceProviderScope : IServiceProviderScope, IServiceProvider, IServiceInjector,
        IServiceProviderScopeFactory
    {
        public bool IsRootScope { get; }
        public bool IsDisposed { get; private set; }
        public IServiceProvider ServiceProvider => this;
        public IServiceInjector ServiceInjector => this;
        
        internal readonly IRootServiceProvider RootProvider;
        internal Dictionary<ServiceCacheKey, object?> ResolvedServices;
        private List<object> _DisposableObjects;
        
        public TService? GetService<TService>()
        {
            return (TService?)RootProvider.GetService(typeof(TService), this);
        }

        public object? GetService(Type type)
        {
            return RootProvider.GetService(type, this);
        }
        
        public void InjectServices(object instance)
        {
            RootProvider.InjectServices(instance, this);
        }

        public void Dispose()
        {
            if (IsDisposed) return;
            IsDisposed = true;
            RootProvider.Disposed -= Dispose;
            if (IsRootScope)
            {
                RootProvider.Dispose();
            }
            foreach (object obj in _DisposableObjects)
            {
                switch (obj)
                {
                    case IDisposable disposable:
                        disposable.Dispose();
                        break;
                    case IAsyncDisposable disposable:
                        disposable.DisposeAsync();
                        break;
                }
            }
        }

        public object? CaptureIfDisposable(object? instance)
        {
            if (instance is null or (not IDisposable and not IAsyncDisposable)) return instance;
            
            _DisposableObjects.Add(instance!);
            return instance;
        }
        
        public IServiceProviderScope CreateScope()
        {
            if (IsDisposed) throw new ObjectDisposedException(nameof(ServiceProviderScope));

            return RootProvider.CreateScope();
        }
        
        internal ServiceProviderScope(IRootServiceProvider rootProvider, bool isRootScope)
        {
            RootProvider = rootProvider;
            _DisposableObjects = new List<object>();
            ResolvedServices = new Dictionary<ServiceCacheKey, object?>();
            IsRootScope = isRootScope;
            rootProvider.Disposed += Dispose;
        }
    }
}