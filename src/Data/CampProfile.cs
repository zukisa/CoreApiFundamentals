using AutoMapper;
using CoreCodeCamp.Modelss;

namespace CoreCodeCamp.Data
{
    public class CampProfile: Profile
    {
        public CampProfile() {
            this.CreateMap<Camp, CampModel>()
                .ForMember(c => c.Venue, o => o.MapFrom(m => m.Location.VenueName))
                .ForMember(c => c.CityTown, o => o.MapFrom(m => m.Location.CityTown));

            this.CreateMap<Talk, TalkModel>();

            this.CreateMap<Speaker, SpeakerModel>();
        }
    }

}