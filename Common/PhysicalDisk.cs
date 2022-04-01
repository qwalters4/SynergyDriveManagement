using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POReportGen
{
    /// <summary>
    /// Represents a physical disk.
    /// </summary>
    internal class PhysicalDisk
    {
        private string formfactor;
        public string FormFactor
        {
            get
            {
                return formfactor;
            }
            set
            {
                formfactor = value;
            }
        }

        private int ponumber;
        public int PONumber
        {
            get
            {
                return ponumber;
            }
            set
            {
                ponumber = value;
            }
        }

        private string technician;
        public string Technician
        {
            get
            {
                return technician;
            }
            set
            {
                technician = value;
            }
        }

        private string smart;
        public string SMART
        {
            get
            {
                return smart;
            }
            set
            {
                smart = value;
            }
        }

        private volatile bool _seaToolsTestPassed = false;
        public bool SeaToolsTestPassed
        {
            get
            {
                return _seaToolsTestPassed;
            }

            set
            {
                _seaToolsTestPassed = value;
            }
        }

        private int _healthScore = 0;
        [Range(0, 100)]
        public int HealthScore
        {
            get
            {
                return _healthScore;
            }

            set
            {
                _healthScore = value;
            }
        }

        private ulong _size;
        [Range(0, ulong.MaxValue)]
        public ulong Size
        {
            get
            {
                return _size;
            }

            set
            {
                _size = value;
            }
        }

        private string _deviceName;
        [MinLength(1)]
        public string DeviceName
        {
            get
            {
                return _deviceName;
            }
            set
            {
                _deviceName = value;
            }
        }

        private ulong _hoursOperational;
        [Range(0, ulong.MaxValue)]
        public ulong HoursOperational
        {
            get
            {
                return _hoursOperational;
            }

            set
            {
                _hoursOperational = value;
            }
        }

        [Range(0, ulong.MaxValue)]
        public ulong DaysOperational
        {
            get
            {
                return HoursOperational / 24;
            }
        }

        private int _deviceIndex;
        [Range(0, int.MaxValue)]
        public int DeviceIndex
        {
            get
            {
                return _deviceIndex;
            }

            set
            {
                _deviceIndex = value;
            }
        }

        private string _modelId;
        [MinLength(1)]
        public string ModelId
        {
            get
            {
                return _modelId;
            }

            set
            {
                _modelId = value;
            }
        }

        private string _serialNumber;
        [MinLength(1)]
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                _serialNumber = value;
            }
        }

        private int _totalBadSectors;
        [Range(0, int.MaxValue)]
        public int TotalBadSectors
        {
            get
            {
                return _totalBadSectors;
            }

            set
            {
                _totalBadSectors = value;
            }
        }

        private DEV_BROADCAST_DEVICEINTERFACE _deviceInterface;
        public DEV_BROADCAST_DEVICEINTERFACE DeviceInterface
        {
            get
            {
                return _deviceInterface;
            }

            private set
            {
                _deviceInterface = value;
            }
        }
    }
}