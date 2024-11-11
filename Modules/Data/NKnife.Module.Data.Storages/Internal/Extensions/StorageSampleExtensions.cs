using Dapper.Contrib.Extensions;
using RAY.Storages;
using RAY.Storages.Common;

namespace NKnife.Module.Data.Storages.Internal.Extensions
{
#if DEBUG
    internal static class StorageSampleExtensions //试验用的类，忽略。
    {
        // 试验用扩展方法的方式为泛型仓储添加方法。正式的方法应该使用public。
        private static void Operate(this IGenericRepository<Hello> repo, params int[] idArray)
        {
            foreach (var id in idArray)
            {
                repo.DeleteAsync(id);
            }
        }

        private static void Test1(IGenericRepository<Hello> repo)
        {
            repo.Operate(1, 2, 3);
        }

        private static void Test2(IGenericRepository<BasicEntity> repo)
        {
            //因泛型对象不同，所以无法调用。所以正确。
            //repo.Operate(1, 2, 3);
        }

        private class HelloRepository : BaseRdbInternalRepository<Hello>
        {
            public async Task DeleteAsync(Hello hello)
            {
                await _Connection.DeleteAsync(hello);
            }
        }

        private class Hello : BasicEntity
        {
            public string? Alias { get; set; }
            public string? Description { get; set; }
            public float Speed { get; set; }
        }
    }
#endif
}