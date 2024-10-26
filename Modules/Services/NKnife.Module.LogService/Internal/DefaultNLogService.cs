﻿using System.Collections.Immutable;
using System.Collections.Specialized;
using NKnife.Circe.Base;
using NKnife.Module.LogService.Internal.Util;
using NLog;
using RAY.Common;
using RAY.Common.Authentication;
using RAY.Common.Services.LogService;

namespace NKnife.Module.LogService.Internal
{
    internal class DefaultNLogService(IAuthenticationManager __authManager) : ILogService
    {
        private static readonly Logger s_logger = LogManager.GetCurrentClassLogger();

        private readonly Dictionary<string, LogStack> _logStacks = new()
        {
            { $"{nameof(LogTargetEnum.All)}", new LogStack() }
        };

        /// <inheritdoc />
        public IImmutableDictionary<string, LogStack> LogStacks => _logStacks.ToImmutableDictionary();

        /// <inheritdoc />
        public void Write(object logWrapper)
        {
            if(logWrapper is not LogEventInfo logInfo)
                return;
            if (LogStacks.TryGetValue(nameof(LogTargetEnum.All), out var allStack))
                allStack.AddLog(logInfo, __authManager.GetCurrentUser());
        }
    }
}