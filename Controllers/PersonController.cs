using Microsoft.AspNetCore.Mvc;
using PersonManagementAPI.DTOs;
using PersonManagementAPI.Services;
using PersonManagementAPI.Validations;
using static PersonManagementAPI.Enums.Enums;

namespace PersonManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IConnectedPersonService _connectedPersonService;
        private readonly PersonValidator _personValidator; 

        public PersonController(IPersonService personService,
                                IConnectedPersonService connectedPersonService,
                                PersonValidator personValidator) 
        {
            _personService = personService;
            _connectedPersonService = connectedPersonService;
            _personValidator = personValidator; 
        }

        
        [HttpPost("add")]
        public async Task<IActionResult> AddPerson([FromBody] PersonDTO personDto)
        {
            
            var validationResult = await _personValidator.ValidateAsync(personDto);
            if (!validationResult.IsValid)
            {
                
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            var newPerson = await _personService.AddPersonAsync(personDto);
            return Ok(newPerson);
        }

        
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDTO personDto)
        {
            
            var validationResult = await _personValidator.ValidateAsync(personDto);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
                return BadRequest(new { Message = "Validation failed", Errors = errors });
            }

            var updatedPerson = await _personService.UpdatePersonAsync(id, personDto);
            return Ok(updatedPerson);
        }


        [HttpPost("upload-picture/{id}")]
        public async Task<IActionResult> UploadProfilePicture(int id, [FromForm] IFormFile file)
        {
            var result = await _personService.UploadProfilePictureAsync(id, file);
            return result ? Ok("Profile picture uploaded successfully") : BadRequest("Upload failed");
        }

        [HttpPost("add-connection")]
        public async Task<IActionResult> AddConnectedPerson(int personId, int connectedPersonId, ConnectionType connectionType)
        {
            var result = await _connectedPersonService.AddConnectionAsync(personId, connectedPersonId, connectionType);
            return result ? Ok("Connection added successfully") : BadRequest("Failed to add connection");
        }

        [HttpDelete("remove-connection")]
        public async Task<IActionResult> RemoveConnectedPerson(int personId, int connectedPersonId, ConnectionType connectionType)
        {
            var result = await _connectedPersonService.RemoveConnectionAsync(personId, connectedPersonId, connectionType);
            return result ? Ok("Connection removed successfully") : BadRequest("Failed to remove connection");
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personService.DeletePersonAsync(id);
            return result ? Ok("Person deleted successfully") : BadRequest("Delete failed");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonById(int id)
        {
            var person = await _personService.GetPersonByIdAsync(id);
            return Ok(person);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPersons(string searchTerm, int pageNumber = 1, int pageSize = 10)
        {
            var persons = await _personService.SearchPersonsAsync(searchTerm, pageNumber, pageSize);
            return Ok(persons);
        }
        [HttpGet("connection-report")]
        public async Task<IActionResult> GetConnectionReport()
        {
            var report = await _personService.GetConnectionReportAsync();
            return Ok(report);
        }

    }
}
