using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AUBGbay.Models
{
    public class Classified
    {
        public int ClassifiedID { get; set; }
        public string UserId { get; set; }
        public int CategoryID { get; set; }

        public string Title { get; set; }
        public string ShortCaption { get; set; }
        public string Description { get; set; }
        public DateTime? DateCreated { get; set; }
        public decimal Price { get; set; }

        public virtual ApplicationUser User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Image> Images { get; set; }

    }
}