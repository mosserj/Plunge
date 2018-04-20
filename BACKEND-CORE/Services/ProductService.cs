using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Core.Interfaces;
using backend.Core.Model;

namespace backend.Services
{
    public class ProductService : IProductService
    {
        private readonly IEntityFrameworkApplicationRepository _entityDB;

        public ProductService(IEntityFrameworkApplicationRepository entityDB)
        {
            _entityDB = entityDB;
        }

        public IEnumerable<Product> Get()
        {
            return _entityDB.GetProducts();
        }

        public Product GetById(int id)
        {
            return _entityDB.GetProductByID(id);
        }

        public void Remove(int id){
            _entityDB.DeleteProduct(id);
        }
        public void Update(Product product){
            _entityDB.UpdateProduct(product);
        }

        public void Add(Product product){
            _entityDB.InsertProduct(product);
        }

        public void SaveChanges(){
            _entityDB.ProductSave();
        }
    }
}
