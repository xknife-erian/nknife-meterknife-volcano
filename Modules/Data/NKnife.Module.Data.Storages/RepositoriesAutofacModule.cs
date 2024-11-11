using Autofac;
using RAY.Storages;

namespace NKnife.Module.Data.Storages
{
    public class RepositoriesAutofacModule : Autofac.Module
    {
        internal static List<Type> RepositoryTypes { get; } = [];

        public static void ConfiguresRepository(Type type)
        {
            if (typeof(IRdbRepository<,>).IsAssignableFrom(type)
                && type is { IsAbstract: false, IsGenericType: false })
            {
                RepositoryTypes.Add(type);
            }
        }

        protected override void Load(ContainerBuilder builder)
        {
            foreach (var repositoryType in RepositoryTypes)
            {
                builder.RegisterType(repositoryType).AsSelf().AsImplementedInterfaces();
            }
        }
    }
}