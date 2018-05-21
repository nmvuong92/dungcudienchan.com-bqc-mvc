using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VD.Data.Entity;

namespace Web.ViewModels.WebParts
{
    public class HeaderView
    {
        public int _current_culture { get; set; }
        public IEnumerable<Lang> Langs { get; set; }
      
    }
}