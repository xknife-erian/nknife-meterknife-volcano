using Autofac;
using NKnife.Module.Data.Storages.Internal;
using NLog;
using RAY.Storages;

namespace NKnife.Module.Data.Storages
{
    public class RepositoriesAutofacModule : Autofac.Module
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        internal static List<Type> RepositoryTypes { get; } = [];

        public static void ConfiguresRepository(Type type)
        {
            if (typeof(IRepository).IsAssignableFrom(type))
            {
                s_logger.Trace($"{type.Name} is {nameof(IRepository)}");

                if (type is { IsAbstract: false, IsGenericType: false })
                {
                    // 获取所有实现的接口
                    var interfaces = type.GetInterfaces();

                    // 构造泛型接口的类型定义
                    var genericInterfaceDefinition = typeof(IRdbRepository<,>);

                    // 检查是否有任何一个接口与给定的泛型接口定义匹配
                    if (interfaces.Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceDefinition))
                    {
                        RepositoryTypes.Add(type);
                        s_logger.Info($"{type.Name} registered to IoC.");
                    }
                }
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