using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Core.Model;
using backend.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Infrastructure
{
    public class EntityFrameworkApplicationRepository : IEntityFrameworkApplicationRepository
    {
        private readonly PlungeDbContext _plungeDbContext;

        public EntityFrameworkApplicationRepository(PlungeDbContext plungeDbContext)
        {
            _plungeDbContext = plungeDbContext;
        }

        #region Product
        public IEnumerable<Product> GetProducts(){
            return _plungeDbContext._Products;
        }
        public Product GetProductByID(int productId){
            return _plungeDbContext._Products.Find(productId);
        }
        public void InsertProduct(Product product){
            _plungeDbContext._Products.Add(product);
        }
        public void DeleteProduct(int productId){
            Product product = _plungeDbContext._Products.Find(productId);
            _plungeDbContext._Products.Remove(product);
        }
        public void UpdateProduct(Product product){
            _plungeDbContext.Entry(product).State = EntityState.Modified;
        }
        public void ProductSave(){
            _plungeDbContext.SaveChanges();
        }
        #endregion

        #region Category
        public IEnumerable<Category> GetCategories(){
            return _plungeDbContext._Categories;
        }
        public Category GetCategoryByID(int categoryId){
            return _plungeDbContext._Categories.Find(categoryId);
        }
        public void InsertCategory(Category category){
            _plungeDbContext._Categories.Add(category);
        }
        public void DeleteCategory(int categoryId){
            Category category = _plungeDbContext._Categories.Find(categoryId);
            _plungeDbContext._Categories.Remove(category);
        }
        public void UpdateCategory(Category category){
            _plungeDbContext.Entry(category).State = EntityState.Modified;
        }
        public void CategorySave(){
            _plungeDbContext.SaveChanges();
        }
        #endregion

        #region Users
        public IEnumerable<AppUser> GetUsers(){
            return _plungeDbContext._Users;
        }
        public AppUser GetUserByID(Guid userId){
            return _plungeDbContext._Users.Find(userId);
        }
        public void InsertUser(AppUser user){
            _plungeDbContext._Users.Add(user);
        }
        public void DeleteUser(Guid userId){
            AppUser user = _plungeDbContext._Users.Find(userId);
            _plungeDbContext._Users.Remove(user);
        }
        public void UpdateUser(AppUser user){
            _plungeDbContext.Entry(user).State = EntityState.Modified;
        }
        public void UserSave(){
            _plungeDbContext.SaveChanges();
        }

        #endregion

        #region Roles
        public IEnumerable<AppUserRole> GetRoles(){
            return _plungeDbContext._Roles;
        }
        public AppUserRole GetUserRoleByID(int roleId){
            return _plungeDbContext._Roles.Find(roleId);
        }
        public void InsertUserRole(AppUserRole role){
            _plungeDbContext._Roles.Add(role);
        }
        public void DeleteUserRole(int roleId){
            AppUserRole role = _plungeDbContext._Roles.Find(roleId);
            _plungeDbContext._Roles.Remove(role);
        }
        public void UpdateUserRole(AppUserRole role){
            _plungeDbContext.Entry(role).State = EntityState.Modified;
        }
        public void UserRoleSave(){
            _plungeDbContext.SaveChanges();
        }

        #endregion

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _plungeDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
