using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Core.Model;

namespace backend.Core.Interfaces
{
    public interface IEntityFrameworkApplicationRepository
    {
        #region Product
        IEnumerable<Product> GetProducts();
        Product GetProductByID(int productId);
        void InsertProduct(Product product);
        void DeleteProduct(int productId);
        void UpdateProduct(Product product);
        void ProductSave();
        #endregion

        #region Category
        IEnumerable<Category> GetCategories();
        Category GetCategoryByID(int categoryId);
        void InsertCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
        void CategorySave();
        #endregion

        #region Users
        IEnumerable<AppUser> GetUsers();
        AppUser GetUserByID(Guid userId);
        void InsertUser(AppUser user);
        void DeleteUser(Guid userId);
        void UpdateUser(AppUser user);
        void UserSave();

        #endregion

        #region Roles
        IEnumerable<AppUserRole> GetRoles();
        AppUserRole GetUserRoleByID(int roleId);
        void InsertUserRole(AppUserRole role);
        void DeleteUserRole(int roleId);
        void UpdateUserRole(AppUserRole role);
        void UserRoleSave();

        #endregion
    }
}
