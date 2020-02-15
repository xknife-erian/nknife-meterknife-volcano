﻿using Autofac;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Holistic;

namespace NKnife.MeterKnife.Logic.IoC
{
    public class InsideModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Kernel>().As<IGlobal>().SingleInstance();
            builder.RegisterType<AntService>().As<IAntService>().SingleInstance();
            builder.RegisterType<AntCollectService>().As<IAntCollectService>().SingleInstance();

            builder.RegisterType<MeasuringLogic>().As<IMeasuringLogic>().SingleInstance();
            builder.RegisterType<EngineeringLogic>().As<IEngineeringLogic>().SingleInstance();
        }
    }
}
