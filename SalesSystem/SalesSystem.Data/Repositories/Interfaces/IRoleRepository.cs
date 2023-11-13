using SalesSystem.Model;

namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetRoles();
    }
}
