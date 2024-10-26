using LEIAO.Module.LogService.Internal.Util;
using NLog;
using RAY.Common;
using RAY.Common.Authentication;
using RAY.Common.Services.LogService;
using System.Collections.Immutable;
using System.Collections.Specialized;

namespace LEIAO.Module.LogService.Internal
{
    internal class DefaultNLogService : ILogService
    {
        private const string EXP_TARGET_NAME = "ExpCsvFile";
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();
        private readonly IAuthenticationManager _authManager;

        private readonly Dictionary<string, LogStack> _logStacks;
        
        public DefaultNLogService(IAuthenticationManager authManager)
        {
            _authManager = authManager;
            _logStacks = new Dictionary<string, LogStack>
            {
                { $"{nameof(LogTargetEnum.All)}", new LogStack() },
                { $"{nameof(LogTargetEnum.Runtime)}", new LogStack() }
            };

            if(expManager == null) 
                return;
            expCoordinator.FlowStarting += (s, e) =>
            {
                var config  = LogManager.Configuration;
                var success = expManager.TryGetActivatedExperiment(out var expDetails, false);

                if(success)
                {
                    var target     = config.FindTargetByName(EXP_TARGET_NAME);
                    var fileTarget = TargetHelper.FindTargetByNameWhenExistWrapped(target);

                    if(fileTarget == null)
                        return;

                    var expDir  = expDetails.FileInfo.ExperimentLocation();
                    var expName = expDetails.Experiment.Name;
                    fileTarget.KeepFileOpen = false;
                    var rule = TargetHelper.UpdateAndBuildRule(fileTarget, expDir, expName);
                    config.AddRule(rule);

                    LogManager.ReconfigExistingLoggers();
                }
            };
            expCoordinator.FlowFinished += (s, e) =>
            {
                var config = LogManager.Configuration;

                if(config.RemoveRuleByName($"{EXP_TARGET_NAME}_wrapped"))
                {
                    LogManager.ReconfigExistingLoggers();

                    var success = expManager.TryGetActivatedExperiment(out var expDetails, false);

                    if(success)
                    {
                        var dir     = expDetails.FileInfo.ExperimentLocation();
                        var expName = expDetails.Experiment.Name;
                        Task.Run(() =>
                        {
                            dir = Path.Combine(dir, $"Log");

                            return LogFiles.CompressTargetFilesFromDirectory(expName, dir);
                        });
                    }
                }
            };
        }

        /// <inheritdoc />
        public IImmutableDictionary<string, LogStack> LogStacks => _logStacks.ToImmutableDictionary();

        /// <inheritdoc />
        public void Write(object logWrapper)
        {
            bool Match(StringCollection strings, string target)
            {
                foreach (var str in strings)
                {
                    if (target.Contains(str))
                        return true;
                }
                return false;
            }

            if(logWrapper is not LogEventInfo logInfo)
                return;
            if (LogStacks.TryGetValue(nameof(LogTargetEnum.All), out var allStack))
                allStack.AddLog(logInfo, _authManager.GetCurrentUser());
            if (IsIncludedIn(logInfo, LogTargetEnum.Runtime) && LogStacks.TryGetValue(nameof(LogTargetEnum.Runtime), out var expStack))
                expStack.AddLog(logInfo, _authManager.GetCurrentUser());
            // 如果logInfo.LoggerName在Context.TargetDict这个字典中以target为key的Value中
            // 那么则将logInfo添加到对应的LogStack中
            var logStackTargets = Context.TargetDict.Where(x => logInfo.LoggerName != null && Match(x.Value, logInfo.LoggerName)).ToList();// logInfo.LoggerName.Contains(x.Key)).ToList();

            foreach (var target in logStackTargets)
            {
                if(LogStacks.TryGetValue(target.Key, out var targetStack))
                {
                    targetStack.AddLog(logInfo, _authManager.GetCurrentUser());
                }
            }
        }

        private bool IsIncludedIn(object logWrapper, LogTargetEnum logTarget)
        {
            if (logWrapper is not LogEventInfo logInfo)
                return false;

            if (logInfo.LoggerName == null)
                return false;
            switch (logTarget)
            {
                case LogTargetEnum.Runtime:
                {
                    var targetQualifiedNames = GetRuntimeQualifiedNames();
                    return targetQualifiedNames.Any(name => logInfo.LoggerName.Contains(name));
                }
                default:                return false;
                case LogTargetEnum.All: return true;
            }
            return false;
        }
        
        private static string[] GetRuntimeQualifiedNames()
        {
            return new string[]
            {
                "LEIAO.Kernel.Flows",
                typeof(BaseAction).Namespace!
            };
        }
        
    }
}