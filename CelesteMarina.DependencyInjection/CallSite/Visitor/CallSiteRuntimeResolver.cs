using System;
using System.Collections.Generic;
using System.Reflection;
using CelesteMarina.DependencyInjection.Injector.Visitor;
using CelesteMarina.DependencyInjection.Provider;
using CelesteMarina.DependencyInjection.Injector;

namespace CelesteMarina.DependencyInjection.CallSite.Visitor
{
    /// <summary>
    /// <inheritdoc cref="ICallSiteRuntimeResolver"/>
    /// </summary>
    internal class CallSiteRuntimeResolver : CallSiteVisitor<RuntimeResolverContext, object?>, ICallSiteRuntimeResolver
    {
        public IInjectorRuntimeResolver RuntimeInjector { get; }

        public object? Resolve(ServiceCallSite callSite, ServiceProviderScope scope)
        {
            if (scope.IsRootScope && callSite.Value != null) return callSite.Value;
            return VisitCallSiteCache(callSite, new RuntimeResolverContext { Scope = scope });
        }
        
        protected override object? VisitConstructor(ConstructorCallSite callSite, RuntimeResolverContext context)
        {
            var arguments = new object?[callSite.ParameterCallSites.Length];
            for (var i = 0; i < arguments.Length; i++)
            {
                ServiceCallSite parameterCallSite = callSite.ParameterCallSites[i];
                arguments[i] = VisitCallSiteCache(parameterCallSite, context);
            }

            object? result = callSite.ConstructorInfo.Invoke(arguments);
            RuntimeInjector.Inject(callSite.InjectorCallSite,
                new InjectorRuntimeResolverContext(context.Scope, result));
            return result;
        }

        protected override object? VisitConstant(ConstantCallSite callSite, RuntimeResolverContext context)
        {
            return callSite.Value;
        }

        protected override object? VisitServiceProvider(ServiceProviderCallSite callSite, RuntimeResolverContext context)
        {
            return context.Scope;
        }

        protected override object? VisitServiceInjector(ServiceInjectorCallSite callSite, RuntimeResolverContext context)
        {
            return context.Scope;
        }

        protected override object? VisitEnumerable(EnumerableCallSite callSite, RuntimeResolverContext context)
        {
            var results = Array.CreateInstance(callSite.SingleServiceType, callSite.CallSites.Length);
            for (var i = 0; i < callSite.CallSites.Length; i++)
            {
                object? value = VisitCallSite(callSite.CallSites[i], context);
                results.SetValue(value, i);
            }

            return results;
        }

        protected override object? VisitFactory(FactoryCallSite callSite, RuntimeResolverContext context)
        {
            return callSite.Factory(context.Scope);
        }

        protected override object? VisitRootCache(ServiceCallSite callSite, RuntimeResolverContext context)
        {
            if (callSite.Value is not null) return callSite.Value;

            ServiceProviderScope rootScope = context.Scope.RootProvider.RootScope;

            object? resolved = VisitCallSite(callSite, new RuntimeResolverContext() { Scope = rootScope });
            rootScope.CaptureIfDisposable(resolved);
            callSite.Value = resolved;
            return resolved;
        }

        protected override object? VisitScopeCache(ServiceCallSite callSite, RuntimeResolverContext context)
        {
            if (context.Scope.IsRootScope) return VisitRootCache(callSite, context);

            if (context.Scope.ResolvedServices.TryGetValue(callSite.CacheInfo.CacheKey, out object? resolved))
            {
                return resolved;
            }

            resolved = VisitCallSite(callSite, context);
            context.Scope.CaptureIfDisposable(resolved);
            context.Scope.ResolvedServices.Add(callSite.CacheInfo.CacheKey, resolved);
            return resolved;
        }

        protected override object? VisitDisposeCache(ServiceCallSite callSite, RuntimeResolverContext context)
        {
            return context.Scope.CaptureIfDisposable(VisitCallSite(callSite, context));
        }

        public CallSiteRuntimeResolver(Func<CallSiteRuntimeResolver, IInjectorRuntimeResolver> injectorResolverBuilder)
        {
            RuntimeInjector = injectorResolverBuilder(this);
        }
    }
}