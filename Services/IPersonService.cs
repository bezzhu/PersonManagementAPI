using PersonManagementAPI.DTOs;

namespace PersonManagementAPI.Services
{
    public interface IPersonService
    {
        Task<PersonDTO> AddPersonAsync(PersonDTO personDto);
        Task<PersonDTO> UpdatePersonAsync(int id, PersonDTO personDto);
        Task<bool> UploadProfilePictureAsync(int id, IFormFile file);
        Task<bool> DeletePersonAsync(int id);
        Task<PersonDTO> GetPersonByIdAsync(int id);
        Task<IEnumerable<PersonDTO>> SearchPersonsAsync(string searchTerm, int pageNumber, int pageSize);
        Task<List<ConnectionReportDTO>> GetConnectionReportAsync();
    }
}

