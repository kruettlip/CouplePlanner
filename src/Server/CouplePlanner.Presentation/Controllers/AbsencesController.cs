using System;
using System.Collections.Generic;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Services;
using CouplePlanner.Application.Schema;
using Microsoft.AspNetCore.Mvc;

namespace CouplePlanner.Presentation.Controllers
{
  /// <summary>
  /// Manage absences that one partner has
  /// </summary>
  [Route("api/[controller]")]
  [ApiController]
  public class AbsencesController : ControllerBase
  {
    public ISchemaProvider SchemaProvider { get; set; }

    public IHappeningApplicationService<Absence, Domain.Entities.Absence> ApplicationService { get; set; }

    public AbsencesController(ISchemaProvider schemaProvider, IHappeningApplicationService<Absence, Domain.Entities.Absence> applicationService)
    {
      SchemaProvider = schemaProvider;
      ApplicationService = applicationService;
    }

    /// <summary>
    /// Get all absences
    /// </summary>
    /// <returns>List of Absences</returns>
    [HttpGet]
    public ActionResult<IEnumerable<Absence>> GetAll()
    {
      try
      {
        return Ok(ApplicationService.GetAll());
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Get upcoming absences
    /// </summary>
    /// <param name="take">specifies how many upcoming absences should be taken (at max)</param>
    /// <returns>List of upcoming absences</returns>
    [HttpGet("upcoming")]
    public ActionResult<IEnumerable<Absence>> GetUpcoming(int take)
    {
      try
      {
        return Ok(ApplicationService.GetUpcoming(take));
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Add a new absence
    /// </summary>
    /// <param name="newAbsence">Absence to create</param>
    /// <returns>ID of the created absence</returns>
    [HttpPost]
    public ActionResult<Guid> Add([FromBody] Absence newAbsence)
    {
      try
      {
        return Ok(ApplicationService.Create(newAbsence));
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Delete an existing absence by ID
    /// </summary>
    /// <param name="id">ID of the absence to delete</param>
    /// <returns>StatusCode 204 No Content</returns>
    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
      try
      {
        ApplicationService.Delete(id);
        return NoContent();
      }
      catch (Exception)
      {
        return BadRequest();
      }
    }

    /// <summary>
    /// Get the JSON-Schema of the absence-entity
    /// </summary>
    /// <returns>JSON-Schema</returns>
    [HttpGet("schema")]
    public ActionResult<string> GetSchema()
    {
      try
      {
        var schema = SchemaProvider.GetSchema<Absence>();

        return Ok(schema);
      }
      catch (Exception e)
      {
        return BadRequest(e);
      }
    }
  }
}
