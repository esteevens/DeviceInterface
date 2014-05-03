﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECore.DataPackages;

namespace ECore.Devices
{
    public enum TriggerMode { ANALOG, DIGITAL, FREE_RUNNING };
    public enum TriggerDirection { RISING, FALLING };
    public enum Coupling { AC, DC };

    public delegate void ScopeConnectHandler(IScope scope);

    public interface IScope
    {
        DataPackageScope GetScopeData();

        bool Connected { get; }
        double GetDefaultTimeRange();
        double GetSamplePeriod();
        int GetNumberOfSamples();
        void SetTriggerHoldOff(double time);
        void SetTriggerAnalog(float voltage);
        void SetTriggerDigital(byte condition);
        void SetYOffset(uint channel, float offset);
        void SetTriggerChannel(uint channel);
        void SetTriggerDirection(TriggerDirection direction);
        void SetTriggerMode(TriggerMode mode);
        void SetCoupling(uint channel, Coupling coupling);
        Coupling GetCoupling(uint channel);
        void SetTimeRange(double timeRange);
        void Configure();
        DataSources.DataSourceScope DataSourceScope { get; }
    }
}