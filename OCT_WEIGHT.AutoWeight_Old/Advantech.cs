using System;
using System.Runtime.InteropServices;

namespace DK_WEIGHT.AutoWeight
{
    #region PT_DEVLIST

    /// <summary>
    /// 安装在主机上所有的自动控制板卡设备结构。
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PT_DEVLIST
    {
        public UInt32 dwDeviceNum;
        /// <summary>
        /// 设备名，50个字符长。
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 50)]
        public string szDeviceName;
        /// <summary>
        /// 设备的端口数，一般为8个端口。
        /// </summary>
        public Int16 nNumOfSubdevices;
    }

    /// <summary>
    /// 用于 DRV_DeviceGetList 调用返回的设备列表结构。
    /// 这一块真难改呀～～，耗费了我一整个晚上的时间～～～～～～～
    /// 网上也找不到资料，最可气的是.Net带的函数都不支持结构数组，只好变相再来一个结构了～～
    /// xiefang 2007/09/02
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct PT_DEVLISTARRAY
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = AdvantechAPI.MaxEntries)]
        public PT_DEVLIST[] Devices;
    }

    #endregion

    #region PT_DeviceGetFeatures

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PT_DeviceGetFeatures
    {
        public IntPtr Buffer;
        public int Size;
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct GainListBlob
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 448)]
        public byte[] gainArr;
    }

    /// <summary>
    /// Define hardware board(device) features.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct DEVFEATURES
    {
        /// <summary>
        /// device driver version, array[0..7]
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8)]
        public string szDriverVer;
        /// <summary>
        /// device driver name, array[0..MAX_DRIVER_NAME_LEN-1]
        /// </summary>
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = AdvantechAPI.MAX_DRIVER_NAME_LEN)]
        public string szDriverName;
        /// <summary>
        /// board ID, DWORD 4 bytes
        /// </summary>
        public uint dwBoardID;
        /// <summary>
        /// Max. number of differential channel
        /// </summary>
        public ushort usMaxAIDiffChl;
        /// <summary>
        /// Max. number of single-end channel
        /// </summary>
        public ushort usMaxAISiglChl;
        /// <summary>
        /// Max. number of D/A channel
        /// </summary>
        public ushort usMaxAOChl;
        /// <summary>
        /// Max. number of digital out channel
        /// </summary>
        public ushort usMaxDOChl;
        /// <summary>
        /// Max. number of digital input channel
        /// </summary>
        public ushort usMaxDIChl;
        /// <summary>
        /// specifies if programmable or not
        /// </summary>
        public ushort usDIOPort;
        /// <summary>
        /// Max. number of Counter/Timer channel
        /// </summary>
        public ushort usMaxTimerChl;
        /// <summary>
        /// Max number of  alram channel
        /// </summary>
        public ushort usMaxAlarmChl;
        /// <summary>
        /// number of bits for A/D converter
        /// </summary>
        public ushort usNumADBit;
        /// <summary>
        /// A/D channel width in bytes.
        /// </summary>
        public ushort usNumADByte;
        /// <summary>
        /// number of bits for D/A converter. 
        /// </summary>
        public ushort usNumDABit;
        /// <summary>
        /// D/A channel width in bytes.
        /// </summary>
        public ushort usNumDAByte;
        /// <summary>
        /// Max. number of gain code
        /// </summary>
        public ushort usNumGain;
        /// <summary>
        /// Gain listing array[0..15]
        /// </summary>
        public GainListBlob glGainList;
        /// <summary>
        /// Permutation array[0..3]
        ///   Bit 0: Software AI                                           
        ///   Bit 1: DMA AI                                                
        ///   Bit 2: Interrupt AI                                          
        ///   Bit 3: Condition AI
        ///   Bit 4: Software AO
        ///   Bit 5: DMA AO
        ///   Bit 6: Interrupt AO
        ///   Bit 7: Condition AO
        ///   Bit 8: Software DI
        ///   Bit 9: DMA DI
        ///   Bit 10: Interrupt DI
        ///   Bit 11: Condition DI
        ///   Bit 12: Software DO
        ///   Bit 13: DMA DO
        ///   Bit 14: Interrupt DO
        ///   Bit 15: Condition DO
        ///   Bit 16: High Gain
        ///   Bit 17: Auto Channel Scan
        ///   Bit 18: Pacer Trigger
        ///   Bit 19: External Trigger
        ///   Bit 20: Down Counter
        ///   Bit 21: Dual DMA
        ///   Bit 22: Monitoring
        ///   Bit 23: QCounter                                             
        /// </summary>
        public uint dwPermutation0;
        public uint dwPermutation1;
        public uint dwPermutation2;
        public uint dwPermutation3;
    }

    #endregion

    /// <summary>
    /// 写入设备的数据结构。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PT_DioWritePortByte
    {
        [FieldOffset(0)]
        public short Port;
        [FieldOffset(2)]
        public short Mask;
        [FieldOffset(4)]
        public short State;
    }

    /// <summary>
    /// 读取IO设备的数据结构。
    /// </summary>
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi, Pack = 1)]
    internal struct PT_DioReadPortByte
    {
        [FieldOffset(0)]
        public short Port;
        [FieldOffset(4)]
        public IntPtr value;
    }

    public static class AdvantechAPI
    {
        public const short MaxEntries = 255;
        public const short MaxDevices = 255;
        public const short MAX_DRIVER_NAME_LEN = 16;

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DeviceGetList(IntPtr deviceList, Int16 maxEntries, ref short outEntries);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DeviceGetFeatures(int DriverHandle, ref PT_DeviceGetFeatures lpDeviceGetFeatures);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        public static extern int DRV_DeviceGetNumOfList(ref short numOfDevices);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DeviceOpen(uint deviceNum, ref int deviceHandle);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DeviceClose(ref int deviceHandle);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DioWritePortByte(int driverHandle, ref PT_DioWritePortByte lpDioWritePortByte);

        [DllImport("adsapi32.dll", CharSet = CharSet.Ansi)]
        static extern int DRV_DioReadPortByte(int driverHandle, ref PT_DioReadPortByte lpDioReadPortByte);

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <param name="deviceNum">设备号</param>
        /// <param name="deviceHandle">设备句柄</param>
        /// <returns>返回错误代码，如果无错误，则返回0。</returns>
        public static int OpenDevice(uint deviceNum, ref int deviceHandle)
        {
            return DRV_DeviceOpen(deviceNum, ref deviceHandle);
        }

        /// <summary>
        /// 关闭设备。
        /// </summary>
        /// <param name="deviceHandle"></param>
        public static void CloseDevice(int deviceHandle)
        {
            DRV_DeviceClose(ref deviceHandle);
        }

        /// <summary>
        /// 获取主机上安装的设备列表。
        /// </summary>
        /// <returns>返回错误代码，如果无错误，则返回0。</returns>
        public static int GetDeviceList(out PT_DEVLISTARRAY deviceList, ref short outEntries)
        {
            deviceList = new PT_DEVLISTARRAY();
            int _DeviceListLenght = Marshal.SizeOf(deviceList);
            IntPtr _DeviceListPoint = Marshal.AllocHGlobal(_DeviceListLenght);
            //打开设备的检查过程
            int errorCode = AdvantechAPI.DRV_DeviceGetList(_DeviceListPoint, MaxEntries, ref outEntries);
            deviceList = (PT_DEVLISTARRAY)Marshal.PtrToStructure(_DeviceListPoint, typeof(PT_DEVLISTARRAY));
            Marshal.FreeHGlobal(_DeviceListPoint);
            return errorCode;
        }

        public static int GetFeatures(int deviceHandle, out DEVFEATURES outDevFeatures)
        {
            int iLength;
            int ErrCde = 0;
            PT_DeviceGetFeatures ptDevGetFeatures = new PT_DeviceGetFeatures();
            outDevFeatures = new DEVFEATURES();

            outDevFeatures.szDriverVer = "?";
            outDevFeatures.szDriverName = "?";
            iLength = Marshal.SizeOf(outDevFeatures);
            //and reserve the space 
            IntPtr DevFeaturesPointer = Marshal.AllocHGlobal(iLength);
            //Copy the pointer into the struct
            ptDevGetFeatures.Buffer = DevFeaturesPointer;
            //and get the features
            ErrCde = DRV_DeviceGetFeatures(deviceHandle, ref ptDevGetFeatures);
            if (ErrCde == 0)
            {
                outDevFeatures = (DEVFEATURES)Marshal.PtrToStructure(DevFeaturesPointer, typeof(DEVFEATURES));
            }
            else
            {
                //Error
            }
            Marshal.FreeHGlobal(DevFeaturesPointer);
            return ErrCde;
        }

        /// <summary>
        /// 数字信号按端口输出。
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="port"></param>
        /// <param name="value"></param>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static int Digital_WriteByteToPort(int deviceHandle, short port, short value, short mask)
        {
            PT_DioWritePortByte data = new PT_DioWritePortByte();
            data.Port = port;
            data.State = value;
            data.Mask = mask;
            return DRV_DioWritePortByte(deviceHandle, ref data);
        }

        public static int Digital_WriteByteToPort(int deviceHandle, short port, short value)
        {
            return Digital_WriteByteToPort(deviceHandle, port, value, 255);
        }

        /// <summary>
        /// 从指定的端口获取数据。
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="port"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int Digital_ReadByteFromPort(int deviceHandle, short port, out short value)
        {
            int error = 0;
            short tempValue = 0;
            IntPtr vpoint = Marshal.AllocHGlobal(Marshal.SizeOf(tempValue));
            try
            {
                Marshal.StructureToPtr(tempValue, vpoint, false);
                PT_DioReadPortByte data = new PT_DioReadPortByte();
                data.Port = port;
                data.value = vpoint;
                error = DRV_DioReadPortByte(deviceHandle, ref data);
                tempValue = (short)Marshal.PtrToStructure(vpoint, typeof(short));
                value = tempValue;
            }
            finally
            {
                Marshal.FreeHGlobal(vpoint);
            }
            return error;
        }
    }


}
