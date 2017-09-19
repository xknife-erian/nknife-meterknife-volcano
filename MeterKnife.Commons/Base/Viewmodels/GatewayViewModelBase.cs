using System.Collections.Generic;
using MeterKnife.Interfaces.Gateways;
using MeterKnife.Models;
using NKnife.IoC;

namespace MeterKnife.Base.Viewmodels
{
    public class GatewayViewModelBase : CommonViewModelBase
    {
        public GatewayViewModelBase()
        {
            DiscoverMap = Load(Habited.Gateways);
        }

        public Dictionary<GatewayModel, IGatewayDiscover> DiscoverMap { get; protected set; }

        /// <summary>
        ///     ���ӱ�����û�ϰ��������ȡ��������ת����Discover���ֵ�
        /// </summary>
        /// <param name="map">�ӱ�����û�ϰ��������ȡ��������</param>
        protected static Dictionary<GatewayModel, IGatewayDiscover> Load(Dictionary<GatewayModel, List<Instrument>> map)
        {
            var result = new Dictionary<GatewayModel, IGatewayDiscover>();
            foreach (var pair in map)
            {
                var discover = DI.Get<IGatewayDiscover>(pair.Key.ToString());
                foreach (var instrument in pair.Value)
                    discover.Instruments.Add(instrument);
                result.Add(pair.Key, discover);
            }
            return result;
        }

        /// <summary>
        ///     ��Discover���ֵ�ת���ɿ��Ա�����û�ϰ�����ݵĸ�ʽ
        /// </summary>
        protected static Dictionary<GatewayModel, List<Instrument>> ToMap(Dictionary<GatewayModel, IGatewayDiscover> discoverMap)
        {
            var map = new Dictionary<GatewayModel, List<Instrument>>();
            foreach (var pair in discoverMap)
            {
                var list = new List<Instrument>();
                list.AddRange(pair.Value.Instruments);
                map.Add(pair.Key, list);
            }
            return map;
        }

    }
}