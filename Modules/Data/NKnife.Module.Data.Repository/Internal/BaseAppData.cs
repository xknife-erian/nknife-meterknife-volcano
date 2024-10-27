using System.Text;
using LiteDB;
using NKnife.Circe.Base.Modules.Data;

namespace NKnife.Module.Data.Repository.Internal
{
    public abstract class BaseAppData : ICirceData
    {
        public abstract string DatabasePath { get; protected set; }
        public abstract string ConnectingString { get; set; }

        #region Implementation of IDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public abstract void Dispose();
        #endregion

        public abstract LiteDatabase GetDatabase();

        protected static string BuildConnectionString(string initDir, string fileName, string password, bool needJournal = false)
        {
            var sb = new StringBuilder();
            sb.Append($"Filename={Path.Combine(initDir, fileName)};");
            sb.Append($"journal={needJournal.ToString().ToLower()};");
            sb.Append($"Password={password};");

            return sb.ToString();
        }
        protected static string BuildConnectionString(string fileName, string password, bool needJournal = false)
        {
            var sb = new StringBuilder();
            sb.Append($"Filename={fileName};");
            sb.Append($"journal={needJournal.ToString().ToLower()};");
            sb.Append($"Password={password};");

            return sb.ToString();
        }

        public BaseAppData? AssertDbVersion(int version)
        {
            return GetDatabase().UserVersion == version ? this : null;
        }

        private static BsonValue RenameDocKey(BsonValue bvalue, string path, string oldKey, string newKey)
        {
            if (bvalue is BsonDocument doc)
            {
                if (path.IsNullOrEmpty())
                {
                    if (doc.ContainsKey(oldKey))
                    {
                        doc[newKey] = doc[oldKey];
                        doc.Remove(oldKey);
                    }
                }
                else
                {
                    var key = path.Split('.')[0];
                    path = path.Substring(path.IndexOf('.') + 1);

                    if (doc.TryGetValue(key, out var value))
                    {
                        doc[key] = RenameDocKey(value, path, oldKey, newKey);
                    }
                }

                return doc;
            }

            return bvalue;
        }

        private static BsonDocument RenameDocKey(BsonDocument doc, string path, string oldKey, string newKey)
        {
            if(path.IsNullOrEmpty())
            {
                if(doc.ContainsKey(oldKey))
                {
                    doc[newKey] = doc[oldKey];
                    doc.Remove(oldKey);
                }
            }
            else
            {
                var key = path.Split('.')[0];
                path = path.Substring(path.IndexOf('.')+1);

                if(doc.TryGetValue(key, out var value))
                {
                    doc[key] = RenameDocKey(value, path, oldKey, newKey);
                }
            }

            return doc;
        }

        public BaseAppData RenameKey<T>(string path, string oldKey, string newKey)
        {
            var col = GetDatabase().GetCollection(typeof(T).Name);
            
            foreach (var doc in col.FindAll())
            {
                var newDoc = RenameDocKey(doc, path, oldKey, newKey);
                col.Update(newDoc);
            }

            return this;
        }

        public virtual void UpdateDbVersion(int version)
        {
            GetDatabase().UserVersion = version;
        }
    }
}