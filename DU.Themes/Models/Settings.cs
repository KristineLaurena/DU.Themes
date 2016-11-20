using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DU.Themes.Models
{
    public class Settings : EntityBase
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}