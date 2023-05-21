using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace App1.Models
{
    public class VideoModel
    {
        public string api_key { get; set; }
        public string geopos { get; set; }
        public Stream video { get; set; }
        public string video_suf { get; set; }
        public string room_type { get; set; }
        public string floor { get; set; }
        public string flat { get; set; }
    }
}
