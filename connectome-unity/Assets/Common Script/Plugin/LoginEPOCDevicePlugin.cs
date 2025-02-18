﻿using Connectome.Unity.Template;
using Connectome.Unity.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connectome.Emotiv.Interface;
using System;
using Connectome.Unity.Expection;
using Connectome.Emotiv.Implementation;

namespace Connectome.Unity.Plugin
{
    public class LoginEPOCDevicePlugin : EPOCDevicePlugin
    {
        public EmotivLoginDisplayPanel LoginWindow;

        public IEmotivDevice DeviceInstance; 

        protected override IEmotivDevice CreateDevice()
        {
            if(DeviceInstance == null)
            {
                DeviceInstance = base.CreateDevice();

                bool suc = false; 
                DeviceInstance.OnConnectAttempted += (b,m) => { suc = b;};
                DeviceInstance.Connect();

                if (suc == false)
                {
                    throw new NullEmotivDeviceException();
                }
            }

            return DeviceInstance; 
        }

        public void Start()
        {
            LoginWindow.OnLoginedIn += (d) => DeviceInstance = d; 
        }
    }
}
