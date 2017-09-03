﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Base;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using NKnife.IoC;

namespace MeterKnife.Keysights
{
    public class KeysightDiscover : GatewayDiscoverBase
    {
        private static readonly ILog _logger = LogManager.GetLogger<KeysightDiscover>();

        private readonly KeysightChannel _Channel;

        public KeysightDiscover()
        {
            _Channel = DI.Get<KeysightChannel>();
            _Channel.Open();
        }

        #region Implementation of IGatewayDiscover

        /// <summary>
        /// 本发现器的通道模式
        /// </summary>
        public override GatewayModel GatewayModel { get; set; } = GatewayModel.Aglient82357A;

        /// <summary>
        /// 手动添加仪器
        /// </summary>
        public override void CreateInstrument()
        {
            var inst = new Instrument("NF", "1915", "NF1915", 5);
            Instruments.Add(inst);
        }

        public override void BeginDiscover()
        {
            var group = new KeysightQuestionGroup();
            _Channel.UpdateQuestionGroup(group);
            _Channel.SendReceiving(SendAction, ReceivedFunc);
            foreach (var instrument in Instruments)
            {
                UpdateInstrument(instrument);
            }
            OnDiscovered();
        }

        /// <summary>
        /// 刷新本测量途径挂接的仪器或设备列表
        /// </summary>
        public override List<InstrumentConnectionState> Refresh()
        {
            throw new NotImplementedException();
        }

        private void UpdateInstrument(Instrument instrument)
        {
            
        }

        private void SendAction(IQuestion<string> question)
        {
            _logger.Debug(question.Data);
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            return true;
        }

        #endregion
    }
}
