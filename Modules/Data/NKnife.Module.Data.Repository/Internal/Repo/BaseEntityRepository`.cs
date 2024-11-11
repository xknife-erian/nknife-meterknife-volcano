using System.Drawing;
using System.Linq.Expressions;
using System.Numerics;
using LiteDB;
using RAY.Common.Entities;
using RAY.Common.Provider;
using RAY.Storages;

namespace NKnife.Module.Data.Repository.Internal.Repo
{
    /// <summary>
    ///     The LiteDB repository pattern. A simple way to access your documents in a single class with fluent query api
    /// </summary>
    public abstract class BaseEntityRepository<T> : INoSQLRepository<T, BsonValue>
    {
        private readonly LiteRepository _liteRepository;
        private ILiteDatabase _database;
        private object s_asyncReadWriteLockObject = new object();

        static BaseEntityRepository()
        {
            BsonMapper.Global.MaxDepth = 40;
            //向LiteDB中注册DateTime类型，LiteDB内部实现中对毫秒数的处理会丢失精度。
            BsonMapper.Global.RegisterType(serialize: dtOffset => dtOffset.ToUnixTimeMilliseconds(), deserialize: value =>
            {
                try
                {
                    var array = value.AsInt64;

                    return DateTimeOffset.FromUnixTimeMilliseconds(array);
                }
                catch (Exception)
                {
                    return value.AsDateTime;
                }
            });
            BsonMapper.Global.RegisterType(serialize: dtOffset => dtOffset.Ticks, deserialize: value =>
            {
                try
                {
                    var array = value.AsInt64;

                    return DateTime.FromBinary(array);
                }
                catch (Exception)
                {
                    return value.AsDateTime;
                }
            });
            BsonMapper.Global.RegisterType(serialize: pair => $"{pair.Item1},{pair.Item2}", deserialize: value =>
            {
                var array = value.AsString.Split(',');

                return (uint.Parse(array[0]), uint.Parse(array[1]));
            });
            //向LiteDB中注册Version类型
            BsonMapper.Global.RegisterType(
                serialize: version => $"{version.Major},{version.Minor},{version.Build},{version.Revision}", deserialize: value =>
                {
                    var array    = value.AsString.Split(',');
                    var major    = int.Parse(array[0]);
                    var minor    = int.Parse(array[1]);
                    var build    = int.Parse(array[2]);
                    var revision = int.Parse(array[3]);

                    return new Version(major, minor >= 0 ? minor : 0, build >= 0 ? build : 0, revision >= 0 ? revision : 0);
                });
            //向LiteDB中注册RectangleF类型
            BsonMapper.Global.RegisterType(serialize: x => $"{x.X},{x.Y},{x.Width},{x.Height}", deserialize: value =>
            {
                var array = value.AsString.Split(',');

                return new RectangleF(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
            });
            //向LiteDB中注册Vector2类型
            BsonMapper.Global.RegisterType(serialize: vector2 => $"{vector2.X},{vector2.Y}", deserialize: value =>
            {
                var array = value.AsString.Split(',');

                return new Vector2(float.Parse(array[0]), float.Parse(array[1]));
            });
            //向LiteDB中注册Vector3类型
            BsonMapper.Global.RegisterType(serialize: vector3 => $"{vector3.X},{vector3.Y},{vector3.Z}", deserialize: value =>
            {
                var array = value.AsString.Split(',');

                return new Vector3(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]));
            });
            //向LiteDB中注册Vector4类型
            BsonMapper.Global.RegisterType(serialize: vector4 => $"{vector4.X},{vector4.Y},{vector4.Z},{vector4.W}",
                                           deserialize: value =>
                                           {
                                               var v = value.AsString.Split(',');

                                               return new Vector4(float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]),
                                                                  float.Parse(v[3]));
                                           });
        }

        protected BaseEntityRepository(string dbFile)
        {
            _liteRepository = new LiteRepository(dbFile);
            _database = _liteRepository.Database;
        }

        protected BaseEntityRepository(LiteDatabase database)
        {
            _database = database;
            _liteRepository = new LiteRepository(database);
        }

        #region Implementation of IRepository<T,BsonValue>

        public void Checkpoint()
        {
            //_database.SafeCheckpoint();
        }

        /// <summary>
        ///     Insert a new document into collection. Document Id must be a new value in collection - Returns document Id
        /// </summary>
        public void Insert(T entity, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                _liteRepository.Insert(entity, collectionName);
            }
            //Checkpoint();
        }

        /// <summary>
        ///     Insert an array of new documents into collection. Document Id must be a new value in collection. Can be set buffer
        ///     size to commit at each N documents
        /// </summary>
        public int Insert(IEnumerable<T> entities, string? collectionName = null)
        {
            int count;
            lock (s_asyncReadWriteLockObject)
            {
                count = _liteRepository.Insert(entities, collectionName);
            }
            //Checkpoint();

            return count;
        }

        /// <summary>
        ///     Update a document into collection. Returns false if not found document in collection
        /// </summary>
        public bool Update(T entity, string? collectionName = null)
        {
            bool flag;
            lock (s_asyncReadWriteLockObject)
            {
                flag = _liteRepository.Update(entity, collectionName);
            }
            //Checkpoint();

            return flag;
        }

        /// <summary>Update all documents</summary>
        public int Update(IEnumerable<T> entities, string? collectionName = null)
        {
            int count;
            lock (s_asyncReadWriteLockObject)
            {
                count = _liteRepository.Update(entities, collectionName);
            }
            //Checkpoint();

            return count;
        }

        /// <summary>
        ///     Insert or Update a document based on _id key. Returns true if insert entity or false if update entity
        /// </summary>
        public bool Upsert(T entity, string? collectionName = null)
        {
            bool flag;
            lock (s_asyncReadWriteLockObject)
            {
                flag = _liteRepository.Upsert(entity, collectionName);
            }
            //Checkpoint();

            return flag;
        }

        /// <summary>
        ///     Insert or Update all documents based on _id key. Returns entity count that was inserted
        /// </summary>
        public int Upsert(IEnumerable<T> entities, string? collectionName = null)
        {
            int count;
            lock (s_asyncReadWriteLockObject)
            {
                count = _liteRepository.Upsert(entities, collectionName);
            }
            //Checkpoint();

            return count;
        }

        IQueryable<T> INoSQLRepository<T, BsonValue>.Query()
        {
            return this.Query() as IQueryable<T>;
        }

        public ILiteQueryable<T> Query(string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.Query<T>(collectionName);
            }
        }

        /// <summary>Delete entity based on _id key</summary>
        public bool Delete(BsonValue id, string? collectionName = null)
        {
            bool flag;
            lock (s_asyncReadWriteLockObject)
            {
                flag = _liteRepository.Delete<T>(id, collectionName);
            }
            //Checkpoint();

            return flag;
        }

        /// <summary>Delete entity based on predicate filter expression</summary>
        public int DeleteMany(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            int count;
            lock (s_asyncReadWriteLockObject)
            {
                count = _liteRepository.DeleteMany(predicate, collectionName);
            }
            //Checkpoint();

            return count;
        }

        /// <summary>
        ///     Create a new permanent index in all documents inside this collections if index not exists already.
        /// </summary>
        /// <param name="keySelector">LinqExpression to be converted into BsonExpression to be indexed</param>
        /// <param name="unique">Create a unique keys index?</param>
        /// <param name="collectionName">Collection Name</param>
        public bool EnsureIndex<TK>(Expression<Func<T, TK>> keySelector, bool unique = false, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.EnsureIndex(keySelector, unique, collectionName);
            }
        }

        /// <summary>
        ///     Create a new permanent index in all documents inside this collections if index not exists already.
        /// </summary>
        /// <param name="name">Index name - unique name for this collection</param>
        /// <param name="keySelector">LinqExpression to be converted into BsonExpression to be indexed</param>
        /// <param name="unique">Create a unique keys index?</param>
        /// <param name="collectionName">Collection Name</param>
        public bool EnsureIndex<TK>(string name, Expression<Func<T, TK>> keySelector, bool unique = false, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.EnsureIndex(name, keySelector, unique, collectionName);
            }
        }

        /// <summary>
        ///     Search for a single instance of L by Id. Shortcut from Query.SingleById
        /// </summary>
        public T SingleById(BsonValue id, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.SingleById<T>(id, collectionName);
            }
        }

        /// <summary>Execute Query[L].Where(predicate).ToList();</summary>
        public List<T> Fetch(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.Fetch(predicate, collectionName);
            }
        }

        /// <summary>Execute Query[L].Where(predicate).First();</summary>
        public T First(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.First(predicate, collectionName);
            }
        }

        /// <summary>Execute Query[L].Where(predicate).FirstOrDefault();</summary>
        public T FirstOrDefault(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.FirstOrDefault(predicate, collectionName);
            }
        }

        /// <summary>Execute Query[L].Where(predicate).Single();</summary>
        public T Single(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.Single(predicate, collectionName);
            }
        }

        /// <summary>Execute Query[L].Where(predicate).SingleOrDefault();</summary>
        public T SingleOrDefault(Expression<Func<T, bool>> predicate, string? collectionName = null)
        {
            lock (s_asyncReadWriteLockObject)
            {
                return _liteRepository.SingleOrDefault(predicate, collectionName);
            }
        }

        #endregion

        #region Implementation of IDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
            Dispose(true);
            // GC.SuppressFinalize((object)this);
        }

        ~BaseEntityRepository()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;

            lock (s_asyncReadWriteLockObject)
            {
                _liteRepository.Database.Dispose();
            }
        }
        #endregion

        #region method
        //public Dictionary<string, BaseVariety> Domains { get; set; }

        //public virtual Dictionary<string, BaseVariety> GetDomains()
        //{
        //    var vv = new Dictionary<string, BaseVariety>();

        //    return vv;
        //}

        /// <summary>
        ///     根据ID获取数据库中实例
        /// </summary>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public virtual BaseVariety? GetOneDomainWithId(string domainId)
        {
            return null;
        }

        /// <summary>
        ///     获取数据库中所有的振荡器模块
        /// </summary>
        /// <returns></returns>
        public virtual List<BaseVariety> GetDomains()
        {
            return new List<BaseVariety>();
        }
        #endregion
    }
}