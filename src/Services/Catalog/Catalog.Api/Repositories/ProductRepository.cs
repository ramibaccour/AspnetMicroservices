using Catalog.Api.Data;
using Catalog.Api.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private ICatalogContext _catalogContext;
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }
        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name.ToLower() == name.ToLower()).ToListAsync();
        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);

            //try
            //{
            //    _catalogContext.Products.InsertOneAsync(product).Wait();
            //}
            //catch (AggregateException aggEx)
            //{
            //    aggEx.Handle(x =>
            //    {
            //        var mwx = x as MongoWriteException;
            //        if (mwx != null && mwx.WriteError.Category == ServerErrorCategory.DuplicateKey)
            //        {
            //            // mwx.WriteError.Message contains the duplicate key error message
            //            return true;
            //        }
            //        return false;
            //    });
            //}

        }
        public async Task<bool> UpdateProduct(Product product)
        {
            var result = await _catalogContext.Products.
                ReplaceOneAsync(filter: productToFilter => productToFilter.Id == product.Id, replacement: product);
            if( result.IsAcknowledged && result.ModifiedCount>0)
                return true;
            else
                return false;
        }
        public async Task<bool> DeleteProduct(string Id)
        {
            var result = await _catalogContext.Products.DeleteOneAsync(filter: p => p.Id == Id);
            if (result.IsAcknowledged && result.DeletedCount > 0)
                return true;
            else
                return false;
        }
    }
}
