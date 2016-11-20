using System;

namespace DU.Themes.Models
{
    public class Request : EntityBase
    {
        public Person Student { get; set; }
        public Person Teacher { get; set; }
        public string ThemeLV { get; set; }
        public string ThemeENG { get; set; }
        public RequestStatus Status { get; set; }
        public string Year { get; set; }  // ????
        public DateTime CreatedOn { get; set; }
        public DateTime RespondedOn { get; set; }
        public bool SeenByTeacher { get; set; }
        //public bool SeenByReviewer { get; set; }
        public bool SeenByStudent { get; set; }
        public bool ThemeAccepted { get; set; }
        public bool SupervisorAccpeted { get; set; }
    }
}