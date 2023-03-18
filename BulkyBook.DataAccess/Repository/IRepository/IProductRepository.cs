using System;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository
{
	public interface IProductRepository
	{
        void Update(Product obj);
        //Product Get(Product obj);
        //Product GetFirstOrDefault(Func<Product, bool> filter);
        Product GetFirstOrDefault(Product product);
        IEnumerable<Product> GetAll();
        void Add(Product entity);
        void Remove(Product entity);
        void RemoveRange(IEnumerable<Product> entity);
    }
}

