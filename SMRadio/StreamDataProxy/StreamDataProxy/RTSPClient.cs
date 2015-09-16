using StreamData.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace StreamData.Core
{
    public class RTSPClient : ProtocolHeader
    {

        private String requestUrl = String.Empty;
        private Boolean isConnected = false;
        private Int32 rtspSeqNum = 0;
        private Socket socket;
        private String response;
        private String sessionId;
        private Double Duration;
        private MediaSession mediaSession;

        private Double SeekTime;

        public EventHandler OnStreamOpened;
        public EventHandler OnConnected;

        public RTSPClient(String ip, Int32 port)
        {
            this.OnStreamOpened += new EventHandler(RTSPClient_OnStreamOpened);
            this.OnConnected += new EventHandler(RTSPClient_OnConnected);
            if (ConnectionServer(ip, port))
            {
                Console.WriteLine("Connect Success!");
            }
            else
            {
                Console.WriteLine("Connect failed!");
            }
            //this.requestUrl = "rtsp://alive.rbc.cn/fm974";
        }


        private Boolean ConnectionServer(String ip, Int32 port)
        {
            try{
                this.socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                socket.Connect(IPAddress.Parse(ip), port);
                this.isConnected = true;
            }
            catch(Exception ex)
            {

            }
            
            return isConnected;
        }


        /// <summary>
        /// Opens the stream.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Succeeded or failed.</returns>
        public bool OpenStream(string url)
        {
            if (!this.isConnected)
            {
                return false;
            }

            // Sets the request url:
            this.requestUrl = url;

            // Sends "OPTIONS" command and then gets the response:
            bool result = this.SendOptionsCmd();
            if (!result)
            {
                this.CloseStream();
                return false;
            }

            // Sends "DESCRIBE" command and then gets the SDP description:
            string sdpDescription = this.SendDescribeCmd();
            if (string.IsNullOrEmpty(sdpDescription))
            {
                this.CloseStream();
                return false;
            }

            // Creates a media session object from the SDP description which
            // we have just received from the server:
            this.mediaSession = new MediaSession(sdpDescription);

            // Then, resolves the SDP description and initializes all basic
            // information:
            result = this.mediaSession.ResolveSdpDescription();
            if (!result)
            {
                this.CloseStream();
                return false;
            }

            // And then, creates the output file to write the data:
            result = this.CreateOutFile();
            if (!result)
            {
                this.CloseStream();
                return false;
            }

            // After that, sends the "SETUP" command and setups the stream:
            result = this.SendSetupCmd();
            if (!result)
            {
                this.CloseStream();
                return false;
            }

            // Finally, sends the "PLAY" command and starts playing stream:
            result = this.PlayStream();
            if (!result)
            {
                this.CloseStream();
                return false;
            }

            //this.OnStreamOpened();

            return true;
        }

        /// <summary>
        /// Close the stream.
        /// </summary>
        /// <returns>Success or failed.</returns>
        public Boolean CloseStream()
        {
            throw new NotImplementedException();
            return true;
        }

        /// <summary>
        /// Receive server stream data and generate new files.
        /// </summary>
        /// <returns></returns>
        private Boolean CreateOutFile()
        {
            throw new NotImplementedException();
            return true;
        }


        #region Operation Method


        /// <summary>
        /// DESCRIBE
        /// </summary>
        /// <returns></returns>
        public String SendDescribeCmd()
        {
            if (!this.isConnected)
            {
                return string.Empty;
            }  

            StringBuilder sbHeader = new StringBuilder();
            sbHeader.AppendFormat("{0} ", Constants.RTSP_CMD_DESCRIBE);
            sbHeader.AppendFormat("{0} RTSP/1.0\r\n", this.requestUrl);   // request url  
            sbHeader.AppendFormat("CSeq: {0}\r\n", ++rtspSeqNum); // sequence number  
            sbHeader.AppendFormat("User-Agent: {0}\r\n\r\n", Constants.USER_AGENT_HEADER);    // user agent header 

            Boolean isSuccessed = SendRtspRequest(sbHeader.ToString());
            if (!isSuccessed)
            {
                return String.Empty;
            }
            Boolean isOk = GetRtspResponse();
            return isOk ? response : String.Empty;
        }

        /// <summary>
        /// Sends the option CMD.
        /// </summary>
        /// <returns>Succeeded or failed.</returns>
        public Boolean SendOptionsCmd()
        {
            if (!this.isConnected)
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ", Constants.RTSP_CMD_OPTIONS);    // command name of 'OPTIONS'  
            sb.AppendFormat("{0} RTSP/1.0\r\n", this.requestUrl);   // request url  
            sb.AppendFormat("CSeq: {0}\r\n", ++rtspSeqNum); // sequence number  
            sb.AppendFormat("User-Agent: {0}\r\n\r\n", Constants.USER_AGENT_HEADER);    // user agent header  

            bool isSucceeded = SendRtspRequest(sb.ToString());
            if (!isSucceeded)
            {
                return false;
            }

            bool isOk = GetRtspResponse();
            if (!isOk)
            {
                return false;
            }
            return true;  
        }


        /// <summary>
        /// Sends the setup CMD.
        /// </summary>
        /// <returns>Succeeded or failed.</returns>
        private bool SendSetupCmd()
        {
            if (!this.isConnected)
            {
                return false;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ", Constants.RTSP_CMD_SETUP);    // command name of 'SETUP'
            sb.AppendFormat("{0} RTSP/1.0\r\n", this.requestUrl);   // request url
            sb.AppendFormat("CSeq: {0}\r\n", ++rtspSeqNum); // sequence number
            sb.AppendFormat("Session: {0}\r\n", this.sessionId);   // session id
            sb.AppendFormat("User-Agent: {0}\r\n\r\n", Constants.USER_AGENT_HEADER);    // user agent header

            bool isSucceeded = SendRtspRequest(sb.ToString());
            if (!isSucceeded)
            {
                return false;
            }

            bool isOk = GetRtspResponse();
            if (!isOk)
            {
                return false;
            }

            return true;
        }


        /// <summary>
        /// Plays the stream.
        /// </summary>
        /// <returns>Succeeded or failed.</returns>
        public bool PlayStream()
        {
            if (!this.isConnected)
            {
                return false;
            }

            if (this.Duration < 0)
            {
                this.Duration = 0;
            }
            else if (this.Duration == 0 || this.Duration > this.mediaSession.PlayEndTime)
            {
                this.Duration = this.mediaSession.PlayEndTime - this.SeekTime;
            }

            double startTime = this.SeekTime;
            double endTime = this.SeekTime + this.Duration;

            string range;
            if (startTime < 0)
            {
                range = string.Empty;
            }
            else if (endTime < 0)
            {
                range = string.Format("Range: npt={0}-", startTime);
            }
            else
            {
                range = string.Format("Range: npt={0}-{1}", startTime, endTime);
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ", Constants.RTSP_CMD_PLAY);    // command name of 'PLAY'
            sb.AppendFormat("{0} RTSP/1.0\r\n", this.requestUrl);   // request url
            sb.AppendFormat("CSeq: {0}\r\n", ++rtspSeqNum); // sequence number
            sb.AppendFormat("Session: {0}\r\n", this.sessionId);    // session id
            sb.AppendFormat("{0}\r\n", range);  // range, 'Range: npt='
            sb.AppendFormat("User-Agent: {0}\r\n\r\n", Constants.USER_AGENT_HEADER);    // user agent header

            bool isSucceeded = SendRtspRequest(sb.ToString());
            if (!isSucceeded)
            {
                this.CloseStream();
                return false;
            }

            bool isOk = GetRtspResponse();
            if (!isOk)
            {
                this.CloseStream();
                return false;
            }

            //this.OnStreamPlaying();

            return true;
        }


        private Boolean SendRtspRequest(string request)
        {
            if (this.socket == null)
            {
                return false;
            }
            try
            {
                Byte[] sendBuffer = Encoding.Default.GetBytes(request);
                Int32 sendBytesCount = this.socket.Send(sendBuffer, sendBuffer.Length, SocketFlags.None);
                if (sendBytesCount < 1)
                {
                    return false;
                }
                return true;
            }
            catch (SocketException e)
            {
                return false;
            }
        }

        private Boolean GetRtspResponse()
        {
            Boolean isOk = false;
            Int32 revBytesCount = 0;

            Byte[] revBuffer = new Byte[1024 * 4]; // 4KB buffer
            response = String.Empty;

            // Set the timeout for synchronous receive methods to   
            // 5 seconds (5000 milliseconds.)  
            socket.ReceiveTimeout = 5000;

            while (true)
            {
                try
                {
                    revBytesCount = socket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                    if (revBytesCount >= 1)
                    {
                        // Here, we have received the data from the server successfully, so we break the loop.  
                        break;  
                    }
                }
                catch (SocketException ex)
                {
                    // Receive data exception, may be it has come to the ReceiveTimeout or other exception.
                    break;
                }
            }

            // Just looking for the RTSP status code:  
            if (revBytesCount >= 1)
            {
                response = Encoding.Default.GetString(revBuffer, 0,revBytesCount);

                if (response.StartsWith(Constants.RTSP_HEADER_VERSION))
                {
                    String[] dstArray = response.Split(' '); //separate by a blank
                    if (dstArray.Length > 1)
                    {
                        String code = dstArray[1];
                        if (code.Equals(Constants.RTSP_STATUS_CODE_OK))
                        {
                            isOk = true;
                        }
                    }
                }
            }
            return isOk;
        }

        #endregion


        #region EventArgs

        //private void OnStreamOpened()
        //{ 

        //}


        private void RTSPClient_OnStreamOpened(object sender, EventArgs e)
        { 

        }

        private void RTSPClient_OnConnected(object sender, EventArgs e)
        { 

        }

        #endregion
    }
}
