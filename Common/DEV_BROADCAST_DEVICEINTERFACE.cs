using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IM.Common
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public unsafe struct DEV_BROADCAST_DEVICEINTERFACE
    {
        public DEV_BROADCAST_DEVICEINTERFACE(IntPtr lParam)
        {
            IntPtr lp1 = lParam;
            dbcc_size = (uint)Marshal.ReadInt32(lp1); lp1 += 4;
            dbcc_devicetype = (uint)Marshal.ReadInt32(lp1); lp1 += 4;
            dbcc_reserved = (uint)Marshal.ReadInt32(lp1); lp1 += 4;
            dbcc_classguid = Marshal.PtrToStructure<Guid>(lp1); lp1 += 16;
            uint strLen = dbcc_size - (4 + 4 + 4 + 16);
            dbcc_name = Marshal.PtrToStringUni(lp1);
        }

        /// <summary>
        /// The size of this structure, in bytes. This is the size of the members plus the actual length of the <c>dbcc_name</c> string
        /// (the null character is accounted for by the declaration of <c>dbcc_name</c> as a one-character array.)
        /// </summary>
        public uint dbcc_size;

        /// <summary>
        /// Set to <c> DBT_DEVTYP_DEVICEINTERFACE </c>.
        /// </summary>
        public uint dbcc_devicetype;

        /// <summary>
        /// Reserved; do not use.
        /// </summary>
        public uint dbcc_reserved;

        /// <summary>
        /// The GUID for the interface device class.
        /// </summary>
        public Guid dbcc_classguid;

        /// <summary>
        /// <para> A null-terminated string that specifies the name of the device. </para>
        /// <para>
        /// When this structure is returned to a window through the WM_DEVICECHANGE message, the <c>
        /// dbcc_name </c> string is converted to ANSI as appropriate. Services always receive a
        /// Unicode string, whether they call <c> RegisterDeviceNotificationW </c> or <c>
        /// RegisterDeviceNotificationA </c>.
        /// </para>
        /// </summary>        
        public string dbcc_name;
    }
}
