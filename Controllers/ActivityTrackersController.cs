using System.Collections.Generic;
using ActivityTracker.Data;
using ActivityTracker.Dtos;
using ActivityTracker.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace ActivityTracker.Controllers
{

  [Route("api/activityTrackers")]
  [ApiController]
  public class ActivityTrackersController : ControllerBase
  {
    private readonly IActivityTrackerRepo _repository;
    private readonly IMapper _mapper;

    // In order for dependency injection to work
    // Dependency injection
    public ActivityTrackersController(IActivityTrackerRepo repository, IMapper mapper)
    {
      _repository = repository;
      _mapper = mapper;
    }

    // private readonly MockActivityTrackerRepo _repository = new MockActivityTrackerRepo();
    // GET api/activityTrackers 
    [HttpGet]
    public ActionResult<IEnumerable<ActivityReadDto>> GetAllActivityTrackers()
    {
      var items = _repository.GetAllActivities();

      return Ok(_mapper.Map<IEnumerable<ActivityReadDto>>(items));
    }

    // GET api/activityTrackers/{id} 
    [HttpGet("{id}")]
    public ActionResult<ActivityReadDto> GetActivityTrackersById(int id)
    {
      var item = _repository.GetActivityById(id);

      if (item != null)
      {
        return Ok(_mapper.Map<ActivityReadDto>(item));
      }

      return NotFound();
    }

    // POST api/activityTrackers/
    [HttpPost]
    public ActionResult<ActivityReadDto> CreateActivity(ActivityCreateDto activityCreateDto)
    {
      // Source is activityCreateDto, what you will map to is Activity
      var activityModel = _mapper.Map<Activity>(activityCreateDto);

      // Create Activity - added to our DB set but it didn't persist the changes down to the database
      _repository.CreateActivity(activityModel);

      // Save the changes to DB
      _repository.SaveChanges();

      return Ok(activityModel);
    }

  }
}