using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace IM.Common
{
    public class DbObservableObject : ObservableObject
    {
        private DBChangeType m_ChangeType;

        public DBChangeType ChangeType
        {
            get { return m_ChangeType; }
            set { Set(ref m_ChangeType, value); }
        }

        protected void SetChanged()
        {
            if (ChangeType == DBChangeType.NoChange)
                ChangeType = DBChangeType.Update;
        }
    }
}
