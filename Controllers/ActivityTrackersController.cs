using System.Collections.Generic;
using ActivityTracker.Data;
using ActivityTracker.Dtos;
using ActivityTracker.Models;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    [HttpGet("{id}", Name = "GetActivityTrackersById")]
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

      var activityReadDto = _mapper.Map<ActivityReadDto>(activityModel);

      // Past back the location of the resources you created
      // 201 Created
      return CreatedAtRoute(nameof(GetActivityTrackersById), new { Id = activityReadDto.Id }, activityReadDto);
    }

    // PUT api/activityTrackers/{id} 
    [HttpPut("{id}")]
    public ActionResult UpdateActivity(int id, ActivityUpdateDto activityUpdateDto)
    {
      var activityModelFromRepo = _repository.GetActivityById(id);

      if (activityModelFromRepo == null)
      {
        return NotFound();
      }

      // Map the source to the where we want to go
      // This has already been implemented in DB Context
      // Previously we were mapping to a new or empty object of some type via our model i.e. _mapper.Map<CommandReadDto>(commandItem)
      // Mapped model that contained data to a new command read detail
      // Here we have a command detail that has data, and a command model that has data. 
      // So it'll be _mapper.Map(SOURCE - the data we want to map from, OUTPUT - the data we want to map to)
      // It has updated the MODEL and DB Context is tracker it. 
      _mapper.Map(activityUpdateDto, activityModelFromRepo);

      // Empty method with SQL Server, but good practice
      _repository.UpdateActivity(activityModelFromRepo);

      // Flush down the changes
      _repository.SaveChanges();

      // 204
      return NoContent();

    }

    // PATCH api/activityTrackers/{id} 
    // What we are receiving from the Client - patchJson, ultimately want to supply / pass to activity model
    // We can't DIRECTLY pass it because this is a patch
    // Need to create a new DTO effectively

    // (1) Get PATCHDOC from our request
    // (2) Check if we have the resource to update from our repository
    // (3) Generate a new ActivityUpdateDto from the data of our repository model
    // (4) We apply a patch to that. 
    // (5) Apply a validation check

    [HttpPatch("{id}")]
    public ActionResult PartialActivityUpdate(int id, JsonPatchDocument<ActivityUpdateDto> patchDoc)
    {
      var activityModelFromRepo = _repository.GetActivityById(id);

      if (activityModelFromRepo == null)
      {
        return NotFound();
      }

      // Source = activityModelFromRepo which is Activity model
      // Effectively creating a new ActivityUpdateDto, content from our repository, putting it into activityToPatch
      // Making use of CreateMap<Activity, ActivityUpdateDto>
      var activityToPatch = _mapper.Map<ActivityUpdateDto>(activityModelFromRepo);

      // Model State make sure things are valid
      patchDoc.ApplyTo(activityToPatch, ModelState);
      // Do a validation check
      if (!TryValidateModel(activityToPatch))
      {
        return ValidationProblem(ModelState);
      }

      // DB context is tracking the changes
      _mapper.Map(activityToPatch, activityModelFromRepo);
      _repository.UpdateActivity(activityModelFromRepo);

      // Flush down the changes
      _repository.SaveChanges();

      // 204
      return NoContent();

    }

    // DELETE api/commands/{id}
    [HttpDelete("{id}")]
    public ActionResult DeleteCommand(int id)
    {
      var itemFromRepo = _repository.GetActivityById(id);

      if (itemFromRepo == null)
      {
        return NotFound();
      }

      _repository.DeleteActivity(itemFromRepo);
      _repository.SaveChanges();

      return NoContent();
    }

  }
}