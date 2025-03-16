using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonManagementAPI.DTOs;
using PersonManagementAPI.Models;
using PersonManagementAPI.Repositories;

namespace PersonManagementAPI.Services
{
    public class PersonService : IPersonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PersonDTO> AddPersonAsync(PersonDTO personDto)
        {
            bool exists = await _unitOfWork.Persons.GetAll()
                                                   .AnyAsync(p => p.PersonalNumber == personDto.PersonalNumber);

            if (exists)
            {
                throw new ArgumentException("The personalNumber already exists!"); 
            }
            var person = _mapper.Map<Person>(personDto);
            await _unitOfWork.Persons.AddAsync(person);

            await _unitOfWork.CommitAsync();
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<PersonDTO> UpdatePersonAsync(int id, PersonDTO personDto)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException("Person not found");

            _mapper.Map(personDto, person);
            _unitOfWork.Persons.Update(person);

            await _unitOfWork.CommitAsync();
            return _mapper.Map<PersonDTO>(person);
        }

        public async Task<bool> UploadProfilePictureAsync(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("No file uploaded.");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(fileExtension))
                throw new ArgumentException("Invalid file type. Only JPG, JPEG, and PNG are allowed.");

            var fileName = Guid.NewGuid().ToString() + fileExtension;
            var uploadsFolder = Path.Combine("Uploads");
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to save the file.", ex);
            }

            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException("Person not found");

            await _unitOfWork.Persons.UpdateProfilePictureAsync(id, filePath);
            await _unitOfWork.CommitAsync();

            return true;
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException("Person not found");

            await _unitOfWork.Persons.DeleteAsync(id);

            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<PersonDTO> GetPersonByIdAsync(int id)
        {
            var person = await _unitOfWork.Persons.GetByIdAsync(id);
            if (person == null)
                throw new KeyNotFoundException("Person not found");

            var reverseConnections = await _unitOfWork.ConnectedPersons.GetAll()
                .Where(cp => cp.ConnectedPersonId == id)
                .Select(cp => new ConnectedPersonDTO
                {
                    ConnectedPersonId = cp.PersonId, 
                    ConnectionType = cp.ConnectionType
                })
                .ToListAsync();

            var directConnections = person.ConnectedPersons.Select(cp => new ConnectedPersonDTO
            {
                ConnectedPersonId = cp.ConnectedPersonId,
                ConnectionType = cp.ConnectionType
            }).ToList();

            var personDto = _mapper.Map<PersonDTO>(person);
            personDto.ConnectedPersons = directConnections.Concat(reverseConnections).ToList();

            return personDto;
        }


        public async Task<IEnumerable<PersonDTO>> SearchPersonsAsync(string searchTerm, int pageNumber, int pageSize)
        {
            var persons = await _unitOfWork.Persons.GetAll()
                .Where(p => p.FirstName.Contains(searchTerm) || p.LastName.Contains(searchTerm))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PersonDTO>>(persons);
        }
        public async Task<List<ConnectionReportDTO>> GetConnectionReportAsync()
        {
            return await _unitOfWork.ConnectedPersons.GetConnectionReportAsync();
        }
    }
}
