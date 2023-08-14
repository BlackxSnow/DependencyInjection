using System.Reflection.Emit;
using DIContainer.CallSite.Visitor;

namespace DIContainer.Injector.Visitor
{
    /// <summary>
    /// Contains data used during <see cref="InjectorILResolver"/> operations.
    /// </summary>
    internal class ILInjectorContext
    {
        public ILResolverContext ResolverContext { get; }
        public ILGenerator Generator => ResolverContext.Generator;
        /// <summary>
        /// A local variable containing the injection target instance.
        /// </summary>
        public LocalBuilder Instance { get; }

        public ILInjectorContext(ILResolverContext resolverContext, LocalBuilder instance)
        {
            ResolverContext = resolverContext;
            Instance = instance;
        }
    }
}