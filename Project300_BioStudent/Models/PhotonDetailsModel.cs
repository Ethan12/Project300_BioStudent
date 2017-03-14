using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project300_BioStudent.Models
{
    public class PhotonDetailsModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Last_app { get; set; }
        public string Last_ip_address { get; set; }
        public string Last_heard { get; set; }
        public string Product_id { get; set; }
        public string Platform_id { get; set; }
        public string Status { get; set; }
        public string Current_build_target { get; set; }
        public string Default_build_target { get; set; }
        public bool Cellular { get; set; }
        public bool Connected { get; set; }
    }
}