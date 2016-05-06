using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AUBGbay.Models
{
    public class Category
    {   
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Classified> Classifieds { get; set; }
    }
}