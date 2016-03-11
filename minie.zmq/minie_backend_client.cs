using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minie.zmq
{
    public class minie_backend_client
    {
        protected BackendAuthPrx auth_prx_;
        protected BackendServicePrx svc_prx_;

        public BackendServicePrx Proxy
        {
            get { return svc_prx_; }
        }
        public bool login(string un, string pw)
        {
            svc_prx_ = auth_prx_.login_backend(un, pw);
            
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
