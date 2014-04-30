﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ECore.Devices
{

    public static class ScopeChannels
    {
        //Note: the ScopeChannel.Order field is set by order of instantiation.
        public static DigitalChannel Digi0 = new DigitalChannel("0", 0);
        public static DigitalChannel Digi1 = new DigitalChannel("1", 1);
        public static DigitalChannel Digi2 = new DigitalChannel("2", 2);
        public static DigitalChannel Digi3 = new DigitalChannel("3", 3);
        public static DigitalChannel Digi4 = new DigitalChannel("4", 4);
        public static DigitalChannel Digi5 = new DigitalChannel("5", 5);
        public static DigitalChannel Digi6 = new DigitalChannel("6", 6);
        public static DigitalChannel Digi7 = new DigitalChannel("7", 7);

        public static AnalogChannel ChA  = new AnalogChannel("A", 0);
        public static AnalogChannel ChB  = new AnalogChannel("B", 1);
        public static AnalogChannel Math = new AnalogChannel("Math", 2);
    }

    public class ScopeChannel
    {
        public static int CompareByOrder(ScopeChannel a, ScopeChannel b)
        {
            return a.Order - b.Order;
        }
        private static int order = 0;
        public string Name { get; protected set; }
        public int Value { get; protected set; }
        public int Order { get; protected set; }
        public static HashSet<ScopeChannel> list = new HashSet<ScopeChannel>();
        public ScopeChannel(string name, int value)
        {
            this.Name = name; 
            this.Value = value;
            this.Order = order;
            order++;
            list.Add(this);
        }

        public virtual void Destroy() { list.Remove(this); }
    }
    public class AnalogChannel : ScopeChannel 
    {
        new public static HashSet<AnalogChannel> list = new HashSet<AnalogChannel>();
        public AnalogChannel(string name, int value) : base(name, value) { list.Add(this); }
        override public void Destroy() { list.Remove(this); base.Destroy(); }
    }
    public class DigitalChannel : ScopeChannel {
        new public static HashSet<DigitalChannel> list = new HashSet<DigitalChannel>();
        public DigitalChannel(string name, int value) : base(name, value) { list.Add(this); }
        override public void Destroy() { list.Remove(this); base.Destroy(); }
    }
}
