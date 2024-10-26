using RAY.Library.Utils;
using System.Security.Cryptography;
using System.Text;

namespace LEIAO.Module.LogService.Internal.Util
{
    internal class LogFiles
    {
        public static async Task CompressTargetFilesFromDirectory(string expName, string dir)
        {
            /*需求：
                1.将本目录中除了zip文件以外的文件全部压缩到一个文件中，
                2.文件名用实验名 + 时间（到秒）
                3.对每个文件计算md5,保存到一个md5.txt中，并将这个md5.txt保存入压缩包中
                4.删除所有已压缩到压缩包的文件
            */

            var zipFileName     = $"{expName}-{DateTime.Now:yyMMdd-HHmmss}.zip";
            var zipFilePath     = Path.Combine(dir, zipFileName);
            var filesToCompress = Directory.GetFiles(dir, "*", SearchOption.TopDirectoryOnly)
                                           .Where(file => Path.GetExtension(file) != ".zip")
                                           .ToArray();

            var md5File = Path.Combine(dir, "md5.txt");

            try
            {
                foreach (var file in filesToCompress)
                {
                    if(Path.GetExtension(file) != ".zip")
                    {
                        using var fileStream =
                            new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 512, true);
                        {
                            var       buffer       = new byte[fileStream.Length];
                            var       readAsync    = await fileStream.ReadAsync(buffer, 0, buffer.Length);
                            using var memoryStream = new MemoryStream(buffer);
                            {
                                var md5 = CalculateMd5(memoryStream);
                                var txt = $"{file}:\t{md5}\r\n";

                                using (var md5FileStream = new FileStream(md5File, FileMode.Append))
                                {
                                    md5FileStream.Write(Encoding.UTF8.GetBytes(txt));
                                }
                            }
                        }
                    }
                }
                var files = new List<string>(filesToCompress) { md5File };

                ZipUtil.CreateZipFile(files, zipFilePath);

                foreach (var file in files)
                {
                    if(Path.GetExtension(file) != ".zip")
                    {
                        File.Delete(file);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static string CalculateMd5(Stream stream)
        {
            using var md5  = MD5.Create();
            var       hash = md5.ComputeHash(stream);

            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}