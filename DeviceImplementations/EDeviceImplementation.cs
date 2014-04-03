﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ECore.DeviceMemories;
using ECore.DataSources;

namespace ECore
{
    //abstract class for all device-specific implementations
    //all new HW devices should inherit from this class
    //groups all information in memories and functionalities
    abstract public class EDeviceImplementation
    {
        //////////////////////////////////////////////////////////////////
        //private properties        
        //private Dictionary<Type, List<object>> sortedInterfaceDictionary;

        //////////////////////////////////////////////////////////////////
        //shared properties        
        protected EDevice eDevice;
        //FIXME: make me protected
        public EDeviceHWInterface hardwareInterface;
        protected List<DeviceMemory<MemoryRegister<byte>>> byteMemories;
        protected List<DataSource> dataSources;
        public List<DataSource> DataSources { get { return this.dataSources; } }

        //////////////////////////////////////////////////////////////////
        //contract for inheriters
        abstract public void InitializeMemories();
        abstract public void InitializeHardwareInterface();
        abstract public void InitializeDataSources();
        abstract public bool Start();
        abstract public void Stop();
        //FIXME: these are too specific for a "Device" -> either call it scope or move them down to the scope
        //abstract public DeviceImplementations.ScopeV2.ScopeV2RomManager CreateRomManager();

        //////////////////////////////////////////////////////////////////
        //base functionality implementation
        protected EDeviceImplementation(EDevice eDevice)
        {
            this.eDevice = eDevice;

            //automatically calls the methods which need to be executed during initialization
            byteMemories = new List<DeviceMemory<MemoryRegister<byte>>>();
            InitializeMemories();
            dataSources = new List<DataSource>();
            InitializeDataSources();
        }

        //getters
        virtual public List<DeviceMemory<MemoryRegister<byte>>> Memories { get { return byteMemories; } }
        /*
        public virtual List<object> GetInterfaces(Type interfaceType)
        {
            //if this method is called the first time: init list
            if (this.sortedInterfaceDictionary == null)
                InitSortedInterfaceDictionary();

            //if the interface is implemented, return it. otherwise, return null
            if (sortedInterfaceDictionary.ContainsKey(interfaceType))
                return sortedInterfaceDictionary[interfaceType];
            else
                return null;
        }*/
        /*
        private void InitSortedInterfaceDictionary()
        {
            this.sortedInterfaceDictionary = new Dictionary<Type, List<object>>();

            //scroll over all implemented interfaces
            foreach (object functionality in this.functionalities)
            {
                Type currentType = functionality.GetType();
                Type[] iList = currentType.FindInterfaces(new System.Reflection.TypeFilter(MyInterfaceFilter), null);
                Type currentInterface = iList[0];

                //if dictionary does not contains a list for this type of funcs: create and add
                if (!sortedInterfaceDictionary.ContainsKey(currentType)) 
                    sortedInterfaceDictionary.Add(currentInterface, new List<object>());

                //now add this functionality to the correct list
                sortedInterfaceDictionary[currentInterface].Add(functionality);//if dictionary does not contain a list for this type of funcs: create
            }
        }*/

        private bool MyInterfaceFilter(Type typeObj, Object criteriaObj)
        {
            return true;
        }

        public bool HasSetting(Setting s) 
        {
            return Utils.HasMethod(this, EDevice.SettingSetterMethodName(s));
        }
    }
}
