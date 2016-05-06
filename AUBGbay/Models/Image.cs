using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AUBGbay.Models
{
    public class Image
    {
        public int ImageID { get; set; }
        public int ClassifiedID { get; set; }
        public string ImageName { get; set; }
        public byte[] ImageContent { get; set; }

        public virtual Classified Classified { get; set; }

    }
}