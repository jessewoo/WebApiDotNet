using ActivityTracker.Dtos;
using ActivityTracker.Models;
using AutoMapper;

namespace ActivityTracker.Profiles
{
  public class ActivitiesProfile : Profile
  {
    public ActivitiesProfile()
    {
      // READING: Source (db) -> Target (client)
      CreateMap<Activity, ActivityReadDto>();

      // CREATION: Source (activity) -> Target (db)
      CreateMap<ActivityCreateDto, Activity>();

      // PUT: Source (client) -> Target (db)
      // The source is our updated detail from our client, then we will apply that to activity object that we have retrieved from our respository
      CreateMap<ActivityUpdateDto, Activity>();

      // PATCH: Source (db) -> Target (dto)
      CreateMap<Activity, ActivityUpdateDto>();
    }
  }
}