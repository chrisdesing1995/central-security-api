using CentralSecurity.Domain.Types;

namespace CentralSecurity.Domain.Queries.Interfaces
{
    public interface IUserQueries
    {
        Task<IEnumerable<UserType>> GetAllUserAsync();
        Task<UserType> GetUserByIdAsync(Guid userId);
    }
}
