using AutoMapper;
using DU.Themes.Models;

namespace DU.Themes.Mappings
{
    /// <summary>
    /// AutoMapper Profile containing all neccessary mappings
    /// </summary>
    public class AppProfile : Profile
    {
        /// <summary>
        /// Constructor where happens mappings defintion
        /// </summary>
        public AppProfile()
        {
            this.CreateMap<Person, PersonModel>();
            this.CreateMap<Request, RequestModel>();
        }
    }
}