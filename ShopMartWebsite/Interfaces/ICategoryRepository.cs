using ShopMartWebsite.Entities;
using ShopMartWebsite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopMartWebsite.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetCategoryById(int id);
        bool SaveCategory(Category product);
        bool UpdateCategory(Category product);
        bool DeleteCategory(Category category);
        IEnumerable<Category> GetAllCategory();
        IEnumerable<Category> SearchCategories(string searchTerm, int page, int recordSize);
        int SearchCategoriesCount(string searchterm);
    }
}
