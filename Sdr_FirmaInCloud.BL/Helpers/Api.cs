using System.Collections.Generic;

namespace SDR_FirmaInCloud.BL.Helpers
{
    class Api
    {

        public class SignProApi
        {
            public File file { get; set; }
            public Configuration configuration { get; set; }
            public List<Signature> signatures { get; set; }
        }

        public class File
        {
            public Input input { get; set; }
            public Output output { get; set; }
            public Authentication authentication { get; set; }
        }

        public class Input
        {
            public string filesystem { get; set; }
            public string http_get { get; set; }
        }

        public class Output
        {
            public string filesystem { get; set; }
            public string http_post { get; set; }
        }

        public class Authentication
        {
            public string pdf_user_password { get; set; }
            public string http_user { get; set; }
            public string http_password { get; set; }
        }

        public class Configuration
        {
            public bool show_annotate { get; set; }
            public bool show_manual_signature { get; set; }
            public string error_handler_url { get; set; }
            public bool process_text_tags { get; set; }
        }

        public class Signature
        {
            public string name { get; set; }
            public string signer { get; set; }
            public string reason { get; set; }
            public string type { get; set; }
            public bool biometric { get; set; }
            public bool required { get; set; }
            public Location location { get; set; }
        }

        public class Location
        {
            public string Page { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int W { get; set; }
            public int H { get; set; }
        }
    }
}
