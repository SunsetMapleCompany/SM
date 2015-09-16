using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamData.Core.Events
{
    public class ClientSessionEventArgs : EventArgs
    {

        public virtual void RTSPClient_OnStreamOpened(object sender, EventArgs e)
        { 

        }
    }

}
