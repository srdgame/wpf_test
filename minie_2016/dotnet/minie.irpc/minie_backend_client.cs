using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minie.irpc
{
    public class minie_backend_client
    {
        static Ice.Communicator s_ic;
        static Ice.ObjectAdapter s_adapter;
        static minie_backend_client()
        {
            string[] parray = new string[] {"--Ice.Plugin.IceSSL=IceSSL.dll:IceSSL.PluginFactory",
                "--IceSSL.DefaultDir=.",
                "--IceSSL.CAs=root.pem",
                "--IceSSL.CertFile=client.p12",
                "--IceSSL.VerifyPeer=2",
                "--Ice.ThreadPool.Server.SizeMax=8",
                "--Ice.ThreadPool.Client.SizeMax=16",
                "--Ice.MessageSizeMax=10485760",
                "--Ice.Override.ConnectTimeout=5000",
                "--Ice.Override.Timeout=5000"};
            //Ice.InitializationData inid = new Ice.InitializationData();
            //inid.properties = Ice.Util.createProperties();
            //inid.properties.setProperty("Ice.Plugin.IceSSL", "IceSSL.dll:IceSSL.PluginFactory");
            //inid.properties.setProperty("IceSSL.DefaultDir", ".");
            //inid.properties.setProperty("IceSSL.CAs", "root.pem");
            //inid.properties.setProperty("IceSSL.CertFile", "client.p12");
            //inid.properties.setProperty("IceSSL.VerifyPeer", "2");
            //inid.properties.setProperty("Ice.ThreadPool.Server.SizeMax", "8");
            //inid.properties.setProperty("Ice.ThreadPool.Client.SizeMax", "16");
            //inid.properties.setProperty("Ice.MessageSizeMax", "10485760");
            //inid.properties.setProperty("Ice.Override.ConnectTimeout", "5000");
            //inid.properties.setProperty("Ice.Override.Timeout", "5000");
            s_ic = Ice.Util.initialize(ref parray);
            s_adapter = s_ic.createObjectAdapter("");
            s_adapter.activate();
        }
        protected string proxy_str_;
        protected BackendAuthPrx auth_prx_;
        protected BackendServicePrx svc_prx_;
        protected sys_user_rpc user_ctx_;


        public minie_backend_client(string proxyStr)
        {
            auth_prx_ = BackendAuthPrxHelper.uncheckedCast(s_ic.stringToProxy(proxyStr));
        }
        public minie_backend_client(string ip, int port, string proto="ssl", string svcName = "minie_backend_auth_service")
            : this(string.Format("{0}:{1} -h {2} -p {3}", svcName, proto, ip, port))
        {
        }
        public void UpdateEndpoint(string ip, int port, string proto = "ssl", string svcName = "minie_backend_auth_service")
        {
            string prxStr = string.Format("{0}:{1} -h {2} -p {3}", svcName, proto, ip, port);
            auth_prx_ = BackendAuthPrxHelper.uncheckedCast(s_ic.stringToProxy(prxStr));
        }
        public BackendServicePrx Proxy
        {
            get { return svc_prx_; }
        }
        public bool login(string un, string pw)
        {
            var prx = auth_prx_.login_backend(un, pw);
            if(prx != null)
            {
                svc_prx_ = BackendServicePrxHelper.uncheckedCast(prx.ice_endpoints(auth_prx_.ice_getEndpoints()));
                if (svc_prx_ != null)
                {
                    return true;
                }
            }
            return false;
        }
        public void logout()
        {
            if (svc_prx_ != null)
            {
                svc_prx_.begin_logout();
            }
        }
    }
}
