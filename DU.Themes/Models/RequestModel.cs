using System;
using DU.Themes.Infrastructure;

namespace DU.Themes.Models
{
    public class RequestModel : ModelBase
    {
        public PersonModel Student { get; set; }
        public PersonModel Teacher { get; set; }
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
        public string StatusRepresentation
        {
            get
            {
                return this.Status.FromResource();
            }
        }

        public RequestModel()
        {
            this.Student = new PersonModel();
            this.Teacher = new PersonModel();
            this.Status = RequestStatus.New;
        }
    }
}