using ArtyfyBackend.Bll.Mapping;
using ArtyfyBackend.Bll.Services;
using ArtyfyBackend.Core.Repositories;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Core.UnitOfWork;
using ArtyfyBackend.Dal.Context;
using ArtyfyBackend.Dal.Repositories;
using ArtyfyBackend.Dal.UnitOfWork;
using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace ArtyfyBackend.API.Modules
{
    public class RepositoryServiceModule : Module
    {
        //Autofac kullanmamızın avantajı: Ne kadar repository ve service class-interface var ise,
        //Hepsini tek tek implement etmek durumunda kalmıyoruz. Built-In injection özelliklerinde bu bulunmuyor.
        protected override void Load(ContainerBuilder builder)
        {
            //Autofac'te ilk önce class daha sonra interface eklenir.
            //Repository Generic bir generic class olduğu için RegisterGeneric kullandık.
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>))
                .InstancePerLifetimeScope();

            //Generic service bir generic class olduğu için RegisterGeneric kullandık.
            builder.RegisterGeneric(typeof(GenericService<,>))
                .As(typeof(IGenericService<,>))
                .InstancePerLifetimeScope();

            //UnitOfWork Generic bir class olmadığı için RegisterType kullandık.
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            //builder.RegisterType<EmailService>().As<IEmailService>();

            //Projede service-repo interface-class'larının kullanıldığı yerlerin assembly'lerini alıyoruz.
            var apiAssembly = Assembly.GetExecutingAssembly();
            //Katman içerisinde herhangi bir class'ın tipini vermemiz, o katman için assembly almamız için yeterli.
            //Direkt katmanın kendisini de verebiliriz(NLayer.Repository) fakat tip ile almak daha güvenli.
            var repositoryAssembly = Assembly.GetAssembly(typeof(ArtyfyBackendDbContext));
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile));

            //Assembly'lerini aldığımız katmanlarda 'Repository' anahtar kelimesi geçen class'ları bellek'e ekliyoruz.
            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            //Assembly'lerini aldığımız katmanlarda 'Service' anahtar kelimesi geçen class'ları bellek'e ekliyoruz.
            builder.RegisterAssemblyTypes(apiAssembly, repositoryAssembly, serviceAssembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}