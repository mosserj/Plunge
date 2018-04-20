using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Core.Model;

namespace backend.Core.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> Get();

        Product GetById(int id);

        void Remove(int id);

        void Update(Product product);

        void Add(Product product);

        void SaveChanges();
    }
}
