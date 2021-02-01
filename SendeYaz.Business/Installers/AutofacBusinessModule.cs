using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using SendeYaz.Core.Utilities.Interceptors;

namespace SendeYaz.Business.Installers
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var x = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(x).AsImplementedInterfaces().EnableInterfaceInterceptors(new ProxyGenerationOptions { Selector = new AspectInterceptorSelector() }).SingleInstance();
        }
    }
}
