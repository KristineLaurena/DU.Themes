using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DU.Themes.Models
{
    public class Theme : EntityBase
    {
        public Person Student { get; set; }
        public Person Teacher { get; set; }
        public Person Reviewer { get; set; }
        public string ThemeLV { get; set; }
        public string ThemeRU { get; set; }
        public string Year { get; set; }  // ????
        public DateTime CreatedOn { get; set; }
        public DateTime RespondedOn { get; set; }
        public DateTime TouchTime { get; set; }
        public bool SeenByTeacher { get; set; }
        public bool SeenByReviewer { get; set; }
        public bool SeenByStudent { get; set; }
    }
}