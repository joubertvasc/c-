using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.WindowsMobile.Status;
using System.Runtime.InteropServices;

namespace JVUtils
{
    public class BatteryMetter
    {
        #region Internal Variables
        private BatteryData batteryData = new BatteryData();
        SystemState batteryState;
        SystemState batteryCharging;
        #endregion

        #region Events
        public delegate void BatteryEventHandler(object sender, BatteryEventArgs args);
        event BatteryEventHandler batteryEvent;
        public event BatteryEventHandler BatteryEvent
        {
            add { batteryEvent += value; }
            remove { batteryEvent -= value; }
        }
        #endregion

        #region Public declarations
        public BatteryMetter()
        {
            // Set Battery metter
            batteryState = new SystemState(SystemProperty.PowerBatteryStrength);
            batteryState.Changed += new ChangeEventHandler(batteryState_Changed);

            UpdateBatteryStrength(SystemState.PowerBatteryStrength);

            batteryCharging = new SystemState(SystemProperty.PowerBatteryState);
            batteryCharging.Changed += new ChangeEventHandler(batteryCharging_Changed);
            UpdateBatteryState(SystemState.PowerBatteryState);
        }

        public int BatteryLifePercent()
        {
            SYSTEM_POWER_STATUS_EX status = new SYSTEM_POWER_STATUS_EX();

            if (Kernel.GetSystemPowerStatusEx(status, false) == 1)
                return status.BatteryLifePercent;
            else
                return 0;
        }

        public int BackupBatteryLifePercent()
        {
            SYSTEM_POWER_STATUS_EX2 status2 = new SYSTEM_POWER_STATUS_EX2();

            if (Kernel.GetSystemPowerStatusEx2(status2, (uint)Marshal.SizeOf(status2), false) == (uint)Marshal.SizeOf(status2))
                return status2.BackupBatteryLifePercent;
            else
                return 0;
        }
        #endregion

        #region Private declarations
        void batteryCharging_Changed(object sender, ChangeEventArgs args)
        {
            Debug.AddLog("batteryCharging_Changed. args is null? " + Utils.iif(args == null, "yes", "no"), true);
            if (args != null)
            {
                UpdateBatteryState((BatteryState)args.NewValue);
            }
        }

        void batteryState_Changed(object sender, ChangeEventArgs args)
        {
            Debug.AddLog("batteryState_Changed. args is null? " + (args == null ? "yes" : "no"), true);
            if (args != null)
            {
                UpdateBatteryStrength((BatteryLevel)args.NewValue);
            }
        }

        void UpdateBatteryState(BatteryState newLevel)
        {
            Debug.AddLog("UpdateBatteryState. newLevel is Charging? " + 
                (newLevel == BatteryState.Charging ? "yes" : "no"), true);

            try
            {
                batteryData.BatteryLifePercent = BatteryLifePercent();
                batteryData.BackupBatteryLifePercent = BackupBatteryLifePercent();
                batteryData.IsCharging = newLevel == BatteryState.Charging;

                //            if (batteryData.IsCharging)
                //                batteryData.BatteryLevel = BatteryLevel.VeryHigh;

                if (batteryEvent != null)
                {
                    BatteryEventArgs args = new BatteryEventArgs(batteryData);
                    batteryEvent(this, args);
                }
            }
            catch (Exception ex)
            {
                Debug.AddLog("UpdateBatteryState. Exception: " + ex.Message.ToString(), true);
            }
        }

        void UpdateBatteryStrength(BatteryLevel newLevel)
        {
            batteryData.BatteryLevel = newLevel;

            if (batteryEvent != null)
            {
                BatteryEventArgs args = new BatteryEventArgs(batteryData);
                batteryEvent(this, args);
            }
        }
        #endregion
    }

    public class BatteryEventArgs : EventArgs
    {
        private BatteryData batteryData;

        public BatteryEventArgs(BatteryData batteryData)
        {
            this.batteryData = batteryData;
        }

        public BatteryData BatteryData
        {
            get { return batteryData; }
        }
    }

    public class BatteryData
    {
        public bool IsCharging;
        public BatteryLevel BatteryLevel;
        public int BatteryLifePercent;
        public int BackupBatteryLifePercent;
    }
}