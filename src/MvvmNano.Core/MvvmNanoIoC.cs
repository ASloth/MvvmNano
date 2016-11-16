﻿  
using System;
using Microsoft.Practices.Unity; 

namespace MvvmNano
{
    /// <summary>
    /// A simple Service Locator, which is mainly used to enable
    /// Dependency Injection within the View Models, so they can
    /// be built in a testable fashion.
    /// 
    /// Static and unique within the whole application.
    /// </summary>
    public static class MvvmNanoIoC
    {
        public static UnityContainer _container;

        static MvvmNanoIoC()
        {
            //_container = new UnityContainer();
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve<TInterface>() is called.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface, for example IUserRepository.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation, for example SqliteUserRepository.</typeparam>
        public static void Register<TInterface, TImplementation>()
            where TImplementation :  TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(); 
        }

        /// <summary>
        /// Registers an Interface and the Implementation type which should be used
        /// at runtime for this Interface when Resolve<TInterface>() is called.
        /// 
        /// The Implementation is only crated once and then being held in memory
        /// for the lifetime of this application.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface, for example IUserRepository.</typeparam>
        /// <typeparam name="TImplementation">The type of the implementation, for example SqliteUserRepository.</typeparam>
        public static void RegisterAsSingleton<TInterface, TImplementation>() 
            where TImplementation : TInterface
        {
            _container.RegisterType<TInterface, TImplementation>(new ContainerControlledLifetimeManager()); 
        }

        /// <summary>
        /// Registers and Interface and a concrete instance implementing this
        /// interface, so the instance is not created when resolving the Interface
        /// but it is passed this concrete instance back.
        /// </summary>
        /// <param name="instance">The concrete instance.</param>
        /// <typeparam name="TInterface">The type of the Interface.</typeparam>
        public static void RegisterAsSingleton<TInterface>(TInterface instance) 
        {
            _container.RegisterInstance<TInterface>(instance, new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// Resolves the implemenation of the Interface, if properly registered before.
        /// </summary>
        /// <typeparam name="TInterface">The type of the Interface.</typeparam>
        public static TInterface Resolve<TInterface>() 
        {
            return _container.Resolve<TInterface>();
        }
    }
}

