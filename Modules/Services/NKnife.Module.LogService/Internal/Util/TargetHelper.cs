using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog.Config;
using NLog;
using NLog.Targets;
using NLog.Targets.Wrappers;

namespace LEIAO.Module.LogService.Internal.Util
{
    internal class TargetHelper
    {
        public static LoggingRule UpdateAndBuildRule(FileTarget fileTarget, string expDir, string expName)
        {
            var sep = Path.DirectorySeparatorChar;
            var logFile = $"{expDir}{sep}Log{sep}{expName}-{DateTime.Now:yyMMdd-HHmmss}.log.csv";
            var logArchiveFile = $"{expDir}{sep}Log{sep}{expName}-{DateTime.Now:yyMMdd-HHmmss}{{#}}.log.zip";

            fileTarget.FileName = logFile;
            fileTarget.ArchiveFileName = logArchiveFile;

            var rule = new LoggingRule("*", LogLevel.Debug, fileTarget) { RuleName = fileTarget.Name };

            return rule;
        }

        public static FileTarget? FindTargetByNameWhenExistWrapped(Target? target)
        {
            if (target is FileTarget fileTarget)
                return fileTarget;
            if (target is AsyncTargetWrapper asyncTargetWrapper)
                return FindTargetByNameWhenExistWrapped(asyncTargetWrapper.WrappedTarget);

            return null;
        }
    }
}
