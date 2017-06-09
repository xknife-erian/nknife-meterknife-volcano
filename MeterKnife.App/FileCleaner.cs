﻿using System;
using System.IO;
using System.Windows.Forms;
using NKnife.ControlKnife;

namespace MeterKnife.App
{
    class FileCleaner
    {
        public static void Run()
        {
            ClearSingleFile("Pansoft.CQMS.Queue.DriverService");
            ClearSingleFile("Pansoft.CQMS.Queue.CustomerVerifyService");
            ClearSingleFile("Pansoft.CQMS.Queue.DoorService");
            ClearSingleFile("Pansoft.CQMS.Queue.SOMSService");
            ClearSingleFile("Pansoft.CQMS.Queue.UIService");
            ClearSingleFile("Pansoft.CQMS.Queue.Plugin.TouchUI", CleanType.Dll);
        }

        private static void ClearSingleFile(string assemblyFileName, CleanType cleanType = CleanType.All)
        {
            string file = assemblyFileName;
            try
            {
                if (cleanType == CleanType.All || cleanType == CleanType.Dll)
                {
                    file = Path.Combine(Application.StartupPath, assemblyFileName + ".dll");
                    if (File.Exists(file))
                        File.Delete(file);
                }
                if (cleanType == CleanType.All || cleanType == CleanType.Exe)
                {
                    file = Path.Combine(Application.StartupPath, assemblyFileName + ".exe");
                    if (File.Exists(file))
                        File.Delete(file);
                }
            }
            catch (Exception)
            {
                Splasher.Status = ("删除过时程序文件异常:" + file);
            }
        }

        enum CleanType
        {
            All,
            Dll,
            Exe
        }
    }
}
