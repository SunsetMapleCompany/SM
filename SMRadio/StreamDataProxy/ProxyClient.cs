using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace StreamData.Core
{
    public class ProxyClient
    {

        /// <summary>
        /// 请求
        /// </summary>
        private ClientRequest Request { get; set; }

        /// <summary>
        /// 响应
        /// </summary>
        private ClientResponse Response { get; set; }


        private Socket socket;

        public ProxyClient()
        {
            

        }

    }
}
