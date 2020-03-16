using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CouplePlanner.Presentation.Controllers
{
    /// <summary>
    /// Manage events that the couple plans together
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
      /// <summary>
      /// Is just here for testing a get-method
      /// </summary>
      /// <returns>"Hello World"</returns>
      [HttpGet]
      public ActionResult<string> Test()
      {
        return Ok("Hello World");
      }

      /// <summary>
      /// Is just here for testing a post-method
      /// </summary>
      /// <param name="myObject">The object that will be validated</param>
      /// <returns>The passed object, if validation passes</returns>
      [HttpPost]
      public IActionResult TestPost(MyObject myObject)
      {
        return Ok(myObject);
      }
    }

    /// <summary>
    /// Own class for testing purposes
    /// </summary>
    public class MyObject
    {
      /// <summary>
      /// The name of the object
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// The content, for example a message
      /// </summary>
      public string Content { get; set; }
    }

    /// <summary>
    /// Validator for MyObject
    /// </summary>
    public class MyObjectValidator : AbstractValidator<MyObject>
    {
      /// <summary>
      /// Constructor for validator
      /// </summary>
      public MyObjectValidator()
      {
        RuleFor(o => o.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters long.");
        RuleFor(o => o.Content).Must(c => c.StartsWith("#")).Must(c => c.EndsWith("#"));
      }
    }
}
