using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    //Bcause I know exactly what is the class inside that category, that is Category
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
      //  void Save();
    }
}
