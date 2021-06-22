//Code for generating a minidump of a defined process (HP.SkyRoom.exe)This code changed in the release version.





using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;
using Microsoft.Win32.SafeHandles;


namespace DumpGeneration
{
    public static class DumpHelper
    {
        public enum DumpType
        {
            MiniDumpNormal = 0,
            MiniDumpWithDataSegs = 1,
            MiniDumpWithFullMemory = 2,
            MiniDumpWithHandleData = 4,
            MiniDumpFilterMemory = 8,
            MiniDumpScanMemory = 16,
            MiniDumpWithUnloadedModules = 32,
            MiniDumpWithIndirectlyReferencedMemory = 64,
            MiniDumpFilterModulePaths = 128,
            MiniDumpWithProcessThreadData = 256,
            MiniDumpWithPrivateReadWriteMemory = 512,
            MiniDumpWithoutOptionalData = 1024,
            MiniDumpWithFullMemoryInfo = 2048,
            MiniDumpWithThreadInfo = 4096,
            MiniDumpWithCodeSegs = 8192,
        }
        [DllImportAttribute("dbghelp.dll")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]//dll from c++ used  number parameters above passed to DLL for greating the minidump
        private static extern bool MiniDumpWriteDump(
            [In] IntPtr hProcess,
            uint ProcessId,
            SafeFileHandle hFile,
            DumpType DumpType,
            [In] IntPtr ExceptionParam,
            [In] IntPtr UserStreamParam,
            [In] IntPtr CallbackParam);

        //different types of memory dumps supported by Dump Helper

        public static void WriteTinyDumpForThisProcess(string fileName)
        {
            WriteDumpForThisProcess(fileName, DumpType.MiniDumpNormal);
        }

        public static void WriteFullDumpForThisProcess(string fileName)
        {
WriteDumpForThisProcess(fileName, DumpType.MiniDumpWithFullMemory);
        }


        public static void WriteDumpForThisProcess(string fileName, DumpType dumpType)
        {
            WriteDumpForProcess(Process.GetCurrentProcess(), fileName, dumpType);
        }

        public static void WriteTinyDumpForProcess(Process process, string fileName)
        {
            WriteDumpForProcess(process, fileName, DumpType.MiniDumpNormal);
        }

        public static void WriteFullDumpForProcess(Process process, string fileName)
        {
            WriteDumpForProcess(process, fileName, DumpType.MiniDumpWithFullMemory);
        }

        public static void WriteDumpForProcess(Process process, string fileName, DumpType dumpType)
        {
            using (FileStream fs = File.Create(fileName))
            {
                if (!MiniDumpWriteDump(Process.GetCurrentProcess().Handle,
                    (uint)process.Id, fs.SafeFileHandle, dumpType,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error(), "Error calling MiniDumpWriteDump.");
                }
            }
        }
    }

    class Program  //This is an example of getting an active process then creating a DMP file of it.
    {
        static void Main(string[] args)
        {


            
             Process process = Process.GetProcessesByName("notepad")[0];//out of range exception thrown if the process is not currently running
            //This is a workaround, when you get processes returned it's in an array, parameter [0] returns the first element
            process.WaitForInputIdle();//WaitForChangedResult for any input ToolboxItemAttribute cease
            DumpHelper.WriteFullDumpForProcess(
                process, @"C:\Skyroom.dmp");//Write file location has to be the desktop folder, need a variable value for the folder path.

           //process.Kill(); Process will continue to run after the dump
        }
    }

}
