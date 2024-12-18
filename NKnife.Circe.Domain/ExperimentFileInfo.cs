using RAY.Common.Authentication;
using RAY.Common.Entities;
using RAY.Common.Enums;
using RAY.Library.Maths;
using System;
using System.IO;
using RAY.Library.Utils;
using Path = System.IO.Path;
// ReSharper disable CheckNamespace

namespace RAY.Common.Flow
{
    public class ExperimentFileInfo : IUnique, IReproducible //: IExperimentFileIndex
    {
        public ExperimentFileInfo()
        {
            var now = DateTime.Now;
            CreatedTime = now;
            ModifyTime  = now;
            LastTime    = now;
        }

        /// <summary>
        ///     实验管理器中实验的Id
        /// </summary>
        public string UniqueCode { get; set; } = Guid.NewGuid().ToString();

        public void CreateInstance()
        {
            UniqueCode = Guid.NewGuid().ToString();
        }
        /// <summary>
        ///     用户自定义的实验名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        ///     创建实验的描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        ///     实验创建的时间
        /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        ///     实验创建人
        /// </summary>
        public IUser? Creator { get; set; }

        /// <summary>
        ///     修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        ///     修改人
        /// </summary>
        public IUser? Modifier { get; set; }

        /// <summary>
        ///     实验存储的路径
        /// </summary>
        public string ExperimentPath { get; set; } = string.Empty;

        /// <summary>
        ///     只读模式
        /// </summary>
        public bool IsReadOnly { get; set; }

        /// <summary>
        ///     最后打开时间
        /// </summary>
        public DateTime LastTime { get; set; }

        public string Password { get; set; } = "";

        public object Reproduce()
        {
            return new ExperimentFileInfo()
            {
                UniqueCode = Guid.NewGuid().ToString(),
                Name = Name,
                Description = Description,
                CreatedTime = CreatedTime,
                ModifyTime = ModifyTime,
                Modifier = Modifier,
                LastTime = LastTime,
                Password = Password,
                ExperimentPath = ExperimentPath,
                IsReadOnly = IsReadOnly
            };
        }

        /// <summary>
        ///     得到用户选择的路径
        /// </summary>
        /// <param name="experimentPath"></param>
        /// <param name="experimentName"></param>
        private static string GetPathFromUser(string experimentPath, string experimentName, string exts)
        {
            return Path.Combine(experimentPath, $"{experimentName}.{exts}");
        }
        
        private string _cacheId = string.Empty;

        public string CacheId => _cacheId;
        
        public string ExperimentFullPath()
        {
            return ExperimentPath;
        }

        public string ExperimentLocation()
        {
            return ExperimentPath;
        }

        public string ExperimentParentDirectory()
        {
            return Path.GetDirectoryName(ExperimentPath) ?? string.Empty;
        }

        public string ExperimentFileCacheFullPath()
        {
            return GetPathFromUser(ExperimentFullPath(), $"~{Name}", "hdr");
        }

        public string ExperimentMethodFileFullPath()
        {
            return GetPathFromUser(ExperimentFullPath(), Name, "rmed");
        }

        public void EnsureSubFolders()
        {
            void EnsureFolder(string dir)
            {
                if(!Directory.Exists(dir))
                {
                    FileUtil.CreateDirectory(dir);
                }
            }
            
            EnsureFolder(ExperimentFullPath());
            EnsureFolder(BluePrintPath());
            EnsureFolder(ExperimentBlackboardPath());
            EnsureFolder(PipetteTechniquePath());
            EnsureFolder(RayLiquidClassPath());
            EnsureFolder(RayPipetteTechniquePath());
            EnsureFolder(ExperimentBlackboardPath());
            EnsureFolder(PipetteTemplatesPath());
            EnsureFolder(FittingsPath());
            EnsureFolder(LiquidTypePath());
            EnsureFolder(RecipePath());
        }

        public string BluePrintPath()
        {
            return Path.Combine(ExperimentFullPath(), "Layout");
        }
        
        public string ExperimentBlackboardPath()
        {
            return Path.Combine(ExperimentFullPath(), "Resource");
        }

        public string PipetteTechniquePath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "Technique");
        }

        public string RayLiquidClassPath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "RayLiquidClass");
        }

        public string RayLiquidClassSimPropPath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "RayLiquidClassSimProp");
        }
        
        public string RayPipetteTechniquePath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "RayTechnique");
        }

        public string PipetteTemplatesPath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "Templates");
        }
        
        public string FittingsPath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "Fittings");
        }

        public string LiquidTypePath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "Liquid Type");
        }

        public string RecipePath()
        {
            return Path.Combine(ExperimentBlackboardPath(), "Recipes");
        }
    }
}