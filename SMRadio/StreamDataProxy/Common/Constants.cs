using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StreamData.Core.Common
{
    public static class Constants
    {
        #region RTSP Area

        #region RTSP Method
        /// <summary>
        /// 检查演示或媒体对象的描述, 也允许使用接收头指定用户理解的描述格式. DESCRIBE的答复-响应组成媒体RTSP初始阶段
        /// </summary>
        public const String RTSP_CMD_DESCRIBE = "DESCRIBE";
        /// <summary>
        /// 当从用户发往服务器时, ANNOUNCE将请求URL识别的演示或媒体对象描述发送给服务器; 反之, ANNOUNCE实时更新连接描述. 
        /// 如新媒体流加入演示, 整个演示描述再次发送, 而不仅仅是附加组件, 使组件能被删除
        /// </summary>
        public const String RTSP_CMD_ANNOUNCE = "ANNOUNCE";
        /// <summary>
        /// GET_PARAMETER请求检查RUL指定的演示与媒体的参数值。没有实体体时，GET_PARAMETER也许能用来测试用户与服务器的连通情况
        /// </summary>
        public const String RTSP_CMD_GET_PARAMETER = "GET_PARAMETER";
        /// <summary>
        /// 可在任意时刻发出OPTIONS请求，如用户打算尝试非标准请求，并不影响服务器状态
        /// </summary>
        public const String RTSP_CMD_OPTIONS = "OPTIONS";
        /// <summary>
        /// PAUSE请求引起流发送临时中断。如请求URL命名一个流，仅回放和记录被停止；如请求URL命名一个演示或流组，演示或组中所有当前活动的流发送都停止。
        /// 恢复回放或记录后，必须维持同步。在SETUP消息中连接头超时参数所指定时段期间被暂停后，尽管服务器可能关闭连接并释放资源，但服务器资源会被预订
        /// </summary>
        public const String RTSP_CMD_PAUSE = "PAUSE";
        /// <summary>
        /// PLAY告诉服务器以SETUP指定的机制开始发送数据；直到一些SETUP请求被成功响应，客户端才可发布PLAY请求。
        /// PLAY请求将正常播放时间设置在所指定范围的起始处，发送流数据直到范围的结束处。PLAY请求可排成队列，服务器将PLAY请求排成队列，顺序执行
        /// </summary>
        public const String RTSP_CMD_PLAY = "PLAY";
        /// <summary>
        /// 该方法根据演示描述初始化媒体数据记录范围，时标反映开始和结束时间；如没有给出时间范围，使用演示描述提供的开始和结束时间。
        /// 如连接已经启动，立即开始记录，服务器数据请求URL或其他URL决定是否存储记录的数据；
        /// 如服务器没有使用URL请求，响应应为201（创建），并包含描述请求状态和参考新资源的实体与位置头。
        /// 支持现场演示记录的媒体服务器必须支持时钟范围格式，smpte格式没有意义
        /// </summary>
        public const String RTSP_CMD_RECORD = "RECORD";
        /// <summary>
        /// 重定向请求通知客户端连接到另一服务器地址。它包含强制头地址，指示客户端发布URL请求；也可能包括参数范围，以指明重定向何时生效。
        /// 若客户端要继续发送或接收URL媒体，客户端必须对当前连接发送TEARDOWN请求，而对指定主执新连接发送SETUP请求
        /// </summary>
        public const String RTSP_CMD_REDIRECT = "REDIRECT";
        /// <summary>
        /// 对URL的SETUP请求指定用于流媒体的传输机制。客户端对正播放的流发布一个SETUP请求，以改变服务器允许的传输参数。
        /// 如不允许这样做，响应错误为"455 Method Not Valid In This State”。为了透过防火墙，客户端必须指明传输参数，即使对这些参数没有影响
        /// </summary>
        public const String RTSP_CMD_SETUP = "SETUP";
        /// <summary>
        /// 这个方法请求设置演示或URL指定流的参数值。请求仅应包含单个参数，允许客户端决定某个特殊请求为何失败。如请求包含多个参数，
        /// 所有参数可成功设置，服务器必须只对该请求起作用。服务器必须允许参数可重复设置成同一值，但不让改变参数值。
        /// 注意：媒体流传输参数必须用SETUP命令设置。将设置传输参数限制为SETUP有利于防火墙。将参数划分成规则排列形式，结果有更多有意义的错误指示
        /// </summary>
        public const String RTSP_CMD_SET_PARAMETER = "SET_PARAMETER";
        /// <summary>
        /// TEARDOWN请求停止给定URL流发送，释放相关资源。如URL是此演示URL，任何RTSP连接标识不再有效。
        /// 除非全部传输参数是连接描述定义的，SETUP请求必须在连接可再次播放前发布
        /// </summary>
        public const String RTSP_CMD_TEARDOWN = "TEARDOWN";
        #endregion




        #endregion



        #region Header Area

        public const String RTSP_HEADER_VERSION = "RTSP/1.0";
        public const String ACCEPT_HEADER = "Accept";
        public const String ACCEPT_ENCODING_HEADER = "Accept-Encoding";
        public const String ACCEPT_LANGUAGE_HEADER = "Accept-Language";
        public const String ALLOW_HEADER = "Allow";
        public const String AUTHORIZATION_HEADER = "Authorization";
        public const String BANDWIDTH_HEADER = "Bandwidth";
        public const String BLOCKSIZE_HEADER = "Blocksize";
        public const String CACHE_CONTROL_HEADER = "Cache-Control";
        public const String CONFERENCE_HEADER = "Conference";
        public const String CONNECTION_HEADER = "Connection";
        public const String CONTENT_BASE_HEADER = "Content-Base";
        public const String CONTENT_ENCODING_HEADER = "Content-Encoding";
        public const String CONTENT_LANGUAGE_HEADER = "Content-Language";
        public const String CONTENT_LENGTH_HEADER = "Content-Length";
        public const String CONTENT_LOCATION_HEADER = "Content-Location";
        public const String CONTENT_TYPE_HEADER = "Content-Type";
        public const String CSEQ_HEADER = "CSeq";
        public const String DATE_HEADER = "Date";
        public const String EXPIRES_HEADER = "Expires";
        public const String FROM_HEADER = "From";
        public const String IF_MODIFIED_SINCE_HEADER = "If-Modified-Since";
        public const String LAST_MODIFIED_HEADER = "Last-Modified";
        public const String PROXY_AUTHENTICATE_HEADER = "Proxy-Authenticate";
        public const String PROXY_REQUIRE_HEADER = "Proxy-Require";
        public const String PUBLIC_HEADER = "Public";
        public const String RANGE_HEADER = "Range";
        public const String REFERER_HEADER = "Referer";
        public const String REQUIRE_HEADER = "Require";
        public const String RETRY_AFTER_HEADER = "Retry-After";
        public const String RTP_INFO_HEADER = "RTP-Info";
        public const String SCALE_HEADER = "Scale";
        public const String SESSION_HEADER = "Session";
        public const String SERVER_HEADER = "Server";
        public const String SPEED_HEADER = "Speed";
        public const String TRANSPORT_HEADER = "Transport";
        public const String UNSUPPORTED_HEADER = "Unsupported";
        public const String USER_AGENT_HEADER = "User-Agent";
        public const String VIA_HEADER = "Via";
        public const String WWW_AUTHENTICATE_HEADER = "WWW-Authenticate";

        #endregion

        #region Status Code

        public const String RTSP_STATUS_CODE_CONTINUE = "100";
        public const String RTSP_STATUS_CODE_OK = "200";
        public const String RTSP_STATUS_CODE_CREATED = "201";
        public const String RTSP_STATUS_CODE_LOW_ON_STORAGE_SPACE = "250";
        public const String RTSP_STATUS_CODE_MULTIPLE_CHOICES = "300";
        public const String RTSP_STATUS_CODE_MOVED_PERMANENTLY = "301";
        public const String RTSP_STATUS_CODE_MOVED_TEMPORARILY = "302";
        public const String RTSP_STATUS_CODE_SEE_OTHER = "303";
        public const String RTSP_STATUS_CODE_NOT_MODIFIED = "304";
        public const String RTSP_STATUS_CODE_USE_PROXY = "305";
        public const String RTSP_STATUS_CODE_GOING_AWAY = "350";
        public const String RTSP_STATUS_CODE_LOAD_BALANCING = "351";
        public const String RTSP_STATUS_CODE_BAD_REQUEST = "400";
        public const String RTSP_STATUS_CODE_UNAUTHORIZED = "401";
        public const String RTSP_STATUS_CODE_PAYMENT_REQUIRED = "402";
        public const String RTSP_STATUS_CODE_FORBIDDEN = "403";
        public const String RTSP_STATUS_CODE_NOT_FOUND = "404";
        public const String RTSP_STATUS_CODE_METHOD_NOT_ALLOWED = "405";
        public const String RTSP_STATUS_CODE_NOT_ACCEPTABLE = "406";
        public const String RTSP_STATUS_CODE_PROXY_AUTHENTICATION_REQUIRED = "407";
        public const String RTSP_STATUS_CODE_REQUEST_TIME_OUT = "408";
        public const String RTSP_STATUS_CODE_GONE = "410";
        public const String RTSP_STATUS_CODE_LENGTH_REQUIRED = "411";
        public const String RTSP_STATUS_CODE_PRECONDITION_FAILED = "412";
        public const String RTSP_STATUS_CODE_REQUEST_ENTITY_TOO_LARGE = "413";
        public const String RTSP_STATUS_CODE_REQUEST_URI_TOO_LARGE = "414";
        public const String RTSP_STATUS_CODE_UNSUPPORTED_MEDIA_TYPE = "415";
        public const String RTSP_STATUS_CODE_PARAMETER_NOT_UNDERSTOOD = "451";
        public const String RTSP_STATUS_CODE_RESERVED = "452";
        public const String RTSP_STATUS_CODE_NOT_ENOUGH_BANDWIDTH = "453";
        public const String RTSP_STATUS_CODE_SESSION_NOT_FOUND = "454";
        public const String RTSP_STATUS_CODE_METHOD_NOT_VALID_IN_THIS_STATE = "455";
        public const String RTSP_STATUS_CODE_HEADER_FIELD_NOT_VALID_FOR_RESOURCE = "456";
        public const String RTSP_STATUS_CODE_INVALID_RANGE = "457";
        public const String RTSP_STATUS_CODE_PARAMETER_IS_READ_ONLY = "458";
        public const String RTSP_STATUS_CODE_AGGREGATE_OPERATION_NOT_ALLOWED = "459";
        public const String RTSP_STATUS_CODE_ONLY_AGGREGATE_OPERATION_ALLOWED = "460";
        public const String RTSP_STATUS_CODE_UNSUPPORTED_TRANSPORT = "461";
        public const String RTSP_STATUS_CODE_DESTINATION_UNREACHABLE = "462";
        public const String RTSP_STATUS_CODE_INTERNAL_SERVER_ERROR = "500";
        public const String RTSP_STATUS_CODE_NOT_IMPLEMENTED = "501";
        public const String RTSP_STATUS_CODE_BAD_GATEWAY = "502";
        public const String RTSP_STATUS_CODE_SERVICE_UNAVAILABLE = "503";
        public const String RTSP_STATUS_CODE_GATEWAY_TIME_OUT = "504";
        public const String RTSP_STATUS_CODE_RTSP_VERSION_NOT_SUPPORTED = "505";
        public const String RTSP_STATUS_CODE_OPTION_NOT_SUPPORTED = "551";

        #endregion
    }
}
