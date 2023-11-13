using SalesSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
    } 
}
