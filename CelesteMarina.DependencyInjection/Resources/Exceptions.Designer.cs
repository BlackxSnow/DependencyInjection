﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CelesteMarina.DependencyInjection {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Exceptions {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Exceptions() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("CelesteMarina.DependencyInjection.Resources.Exceptions", typeof(Exceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string ConstantCantConvertToServiceType {
            get {
                return ResourceManager.GetString("ConstantCantConvertToServiceType", resourceCulture);
            }
        }
        
        internal static string ImplementationTypeCantConvertToServiceType {
            get {
                return ResourceManager.GetString("ImplementationTypeCantConvertToServiceType", resourceCulture);
            }
        }
        
        internal static string InvalidServiceDescriptor {
            get {
                return ResourceManager.GetString("InvalidServiceDescriptor", resourceCulture);
            }
        }
        
        internal static string NoConstructor {
            get {
                return ResourceManager.GetString("NoConstructor", resourceCulture);
            }
        }
        
        internal static string NoValidConstructor {
            get {
                return ResourceManager.GetString("NoValidConstructor", resourceCulture);
            }
        }
        
        internal static string OpenGenericServiceRequiresOpenGenericImplementation {
            get {
                return ResourceManager.GetString("OpenGenericServiceRequiresOpenGenericImplementation", resourceCulture);
            }
        }
        
        internal static string TypeCannotBeConstructed {
            get {
                return ResourceManager.GetString("TypeCannotBeConstructed", resourceCulture);
            }
        }
        
        internal static string GenericParameterCountServiceImplementationNotEqual {
            get {
                return ResourceManager.GetString("GenericParameterCountServiceImplementationNotEqual", resourceCulture);
            }
        }
        
        internal static string MultipleInjectionMethodsNotSupported {
            get {
                return ResourceManager.GetString("MultipleInjectionMethodsNotSupported", resourceCulture);
            }
        }
        
        internal static string InjectionPropertyNoSetter {
            get {
                return ResourceManager.GetString("InjectionPropertyNoSetter", resourceCulture);
            }
        }
        
        internal static string MissingRequiredInjectionServiceType {
            get {
                return ResourceManager.GetString("MissingRequiredInjectionServiceType", resourceCulture);
            }
        }
    }
}
