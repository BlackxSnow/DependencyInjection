using DIContainer.Provider;

namespace DIContainer
{
    public delegate object ServiceFactory(IServiceProvider provider);
    
    public delegate object? ServiceResolver(ServiceProviderScope scope);
    
    public delegate object? ServiceInjector(IServiceProviderScope scope, object instance);
}