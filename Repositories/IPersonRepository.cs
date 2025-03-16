using PersonManagementAPI.Models;

namespace PersonManagementAPI.Repositories
{
    public interface IPersonRepository
    {
        Task AddAsync(Person person);
        void Update(Person person);
        Task DeleteAsync(int id);
        Task<Person?> GetByIdAsync(int id);
        IQueryable<Person> GetAll(); 
        Task UpdateProfilePictureAsync(int personId, string imagePath);
    }
}

