using System.Security.Cryptography;
using System.Text;
using LiteDB;
using NKnife.Circe.Base.Exceptions;
using NKnife.Circe.Base.Exceptions.Data;

namespace NKnife.Module.Data.Repository.Internal
{
    internal static class DbFactory
    {
        private static readonly Dictionary<string, LiteDatabase> s_dbDict = new();

        private static string HashValue(string inputString)
        {
            var inputBytes = Encoding.Unicode.GetBytes(inputString);

            // 创建SHA256哈希算法实例
            using var sha256 = SHA256.Create();

            // 计算哈希值
            var hashBytes = sha256.ComputeHash(inputBytes);

            // 将哈希值转换为十六进制字符串
            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        internal static LiteDatabase GetDatabase(string connectingString, out bool created)
        {
            var key = HashValue(connectingString);

            created = false;
            if (s_dbDict.Keys.Contains(key))
            {
                if(s_dbDict[key] == null)
                {
                    s_dbDict.Remove(key);
                }
                else
                {
                    return s_dbDict[key];
                }
            }

            try
            {
                created = !File.Exists(new ConnectionString(connectingString).Filename);
                var db = new LiteDatabase(connectingString);
                db.CheckpointSize = 1000;
                s_dbDict.Add(key, db);

                return s_dbDict[key];
            }
            catch (Exception e)
            {
                throw new DatabaseNotFoundException(ErrorNumber.Database.UnableToConnectToDatabase, $"无法连接到数据库：{connectingString}", e);
            }
        }

        internal static bool DisposeDatabase(string connectingString)
        {
            var key = HashValue(connectingString);

            if(s_dbDict.Keys.Contains(key))
            {
                s_dbDict.Remove(key);

                return true;
            }

            return false;
        }
    }
}               