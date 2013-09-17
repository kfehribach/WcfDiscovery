using System;
using System.ServiceModel;

using global::Ninject;
using global::Ninject.Extensions.Wcf;
using global::Ninject.Parameters;
using global::Ninject.Syntax;

namespace WcfDiscoveryService.Ninject
{
    // To use this custom factory change the LogginService.svc markup file to set the factory attribute to: WcfDiscoveryService.Ninject.CustomNinjectFactory
    public class CustomNinjectFactory : NinjectServiceHostFactory
    {
        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            // If we just create a service host then it is discoverable, unfortunately the constructor injection on the service classes is ignored. This method requires 0 param constructor
            var serviceHost = new ServiceHost(serviceType, baseAddresses);

            // If do the normal behavior of the Ninject service host factory then the service is not discoverable

            // var serviceHost = this.BaseNinjectCreateServiceHost(serviceType, baseAddresses);
            return serviceHost;
        }

        private ServiceHost BaseNinjectCreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            var serviceHost = base.CreateServiceHost(serviceType, baseAddresses);

            // This just shows what the base create service host is doing.
            /* return
                (ServiceHost)
                ResolutionExtensions.Get(
                    (IResolutionRoot)BaseNinjectServiceHostFactory.kernelInstance,
                    this.ServiceHostType.MakeGenericType(new Type[1] { serviceType }),
                    new IParameter[1] { (IParameter)new ConstructorArgument("baseAddresses", (object)baseAddresses) });
             */

            return serviceHost;
        }

    }
}