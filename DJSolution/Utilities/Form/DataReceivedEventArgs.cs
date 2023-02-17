using System;

namespace DJ.LMS.Utilities
{
    public delegate void DataReceivedEventHandler(DataReceivedEventArgs e);

    public class DataReceivedEventArgs : EventArgs
    {
        public string DataReceived;
        public DataReceivedEventArgs(string m_DataReceived)
        {
            this.DataReceived = m_DataReceived;
        }
    }
}
