using Catalog.Api.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public interface IProductRepository
    {
        // task pour dire que c tune tache asychrone
        // IEnumerable c comme liste mais en lecture seul
        // conclusion :
        // GetProducts est une fonction asychnore (Task) qui retourne une liste de type Product
        // non mobifiable (IEnumerable)
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(string Id);
        Task<IEnumerable<Product>> GetProductsByName(string Name);
        Task CreateProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(string Id);
    }
}
