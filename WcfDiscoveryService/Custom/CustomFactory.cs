using global::Ninject;
using System;
using System.Collections;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;

namespace WcfDiscoveryService.Custom
{
    using System.ServiceModel.Discovery;

    using global::Ninject.Extensions.Wcf;
    using global::Ninject.Parameters;
    using global::Ninject.Syntax;

    public class CustomFactory : ServiceHostFactory
    {
        private static IKernel kernelInstance;

        protected Type ServiceHostType
        {
            get
            {
                return typeof(CustomIISHostingServiceHost<>);
            }
        }

        public static void SetKernel(IKernel kernel)
        {
            kernelInstance = kernel;
        }

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHostType = this.ServiceHostType.MakeGenericType(new Type[1] { serviceType });
            var parameter = new IParameter[1] { (IParameter)new ConstructorArgument("baseAddresses", (object)baseAddresses) };

            ServiceHost serviceHost = 
               (ServiceHost)kernelInstance.Get(serviceHostType, parameter);

            return serviceHost;
        }
    }

    internal class CustomIISHostingServiceHost<T> : CustomAbstractServiceHost<T>
    {
        /// <summary>
        /// Initializes a new instance of the NinjectIISHostingServiceHost class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The service behavior.</param><param name="instance">The instance.</param><param name="baseAddresses">The base addresses.</param>
        public CustomIISHostingServiceHost(IServiceBehavior serviceBehavior, T instance, Uri[] baseAddresses)
            : base(serviceBehavior, instance, baseAddresses)
        {
        }
    }

    public abstract class CustomAbstractServiceHost<T> : CustomServiceHost
    {
        /// <summary>
        /// Initializes a new instance of the NinjectAbstractServiceHost class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The service behavior.</param><param name="instance">The instance.</param><param name="baseAddresses">The baseAddresses.</param>
        protected CustomAbstractServiceHost(IServiceBehavior serviceBehavior, T instance, Uri[] baseAddresses)
            : base(serviceBehavior)
        {
            UriSchemeKeyedCollection baseAddresses1 = new UriSchemeKeyedCollection(baseAddresses);
            if (IsSingletonService((object)instance))
                this.InitializeDescription((object)instance, baseAddresses1);
            else
                this.InitializeDescription(typeof(T), baseAddresses1);
        }

        public static bool IsSingletonService(object service)
        {
            ServiceBehaviorAttribute behaviorAttribute = Enumerable.SingleOrDefault<ServiceBehaviorAttribute>(Enumerable.Cast<ServiceBehaviorAttribute>((IEnumerable)service.GetType().GetCustomAttributes(typeof(ServiceBehaviorAttribute), true)));
            if (behaviorAttribute != null)
                return behaviorAttribute.InstanceContextMode == InstanceContextMode.Single;
            else
                return false;
        }
    }

    public class CustomServiceHost : ServiceHost
    {
        /// <summary>
        /// The service behavior.
        /// 
        /// </summary>
        private readonly IServiceBehavior serviceBehavior;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ninject.Extensions.Wcf.NinjectServiceHost"/> class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The behavior factory.</param>
        public CustomServiceHost(IServiceBehavior serviceBehavior)
        {
            this.serviceBehavior = serviceBehavior;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ninject.Extensions.Wcf.NinjectServiceHost"/> class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The behavior factory.</param><param name="serviceType">Type of the service.</param>
        public CustomServiceHost(IServiceBehavior serviceBehavior, TypeCode serviceType)
            : base((object)serviceType, new Uri[0])
        {
            this.serviceBehavior = serviceBehavior;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ninject.Extensions.Wcf.NinjectServiceHost"/> class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The behavior factory.</param><param name="singletonInstance">The singleton instance.</param>
        public CustomServiceHost(IServiceBehavior serviceBehavior, object singletonInstance)
            : base(singletonInstance, new Uri[0])
        {
            this.serviceBehavior = serviceBehavior;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Ninject.Extensions.Wcf.NinjectServiceHost"/> class.
        /// 
        /// </summary>
        /// <param name="serviceBehavior">The behavior factory.</param><param name="serviceType">Type of the service.</param><param name="baseAddresses">The base addresses.</param>
        public CustomServiceHost(IServiceBehavior serviceBehavior, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.serviceBehavior = serviceBehavior;
        }

        /// <summary>
        /// Invoked during the transition of a communication object into the opening state.
        /// 
        /// </summary>
        protected override void OnOpening()
        {
            this.Description.Behaviors.Add(this.serviceBehavior);
            base.OnOpening();
        }
    }
}
