using System;
using System.Data;
using System.Runtime.InteropServices;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Dapper;

namespace BulkyBook.DataAccess.Repository
{
	public class ProductRepository : IProductRepository
	{
        private readonly IDbConnection _db;
		public ProductRepository(IDbConnection db)
		{
            _db = db;
		}

        public void Add(Product entity)
        {
            _db.Execute("INSERT INTO product(title, description, isbn, author, list_price, price, price50, price100, image_url, category_id, cover_type_id) VALUES(@Title, @Description, @ISBN, @Author, @ListPrice, @Price, @Price50, @Price100, @ImageUrl, @CategoryId, @CoverTypeId)", entity);
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Query<Product>("SELECT p.*, c.name AS category_name, ct.name as cover_type_name FROM product p LEFT JOIN category c ON p.category_id = c.id LEFT JOIN cover_type ct ON p.cover_type_id = ct.id").ToList();
        }

        public Product GetFirstOrDefault(Product product)
        {
            return _db.Query<Product>("SELECT * FROM product" + product.ToWhereString(), product).FirstOrDefault();
        }

        public void Remove(Product entity)
        {
            _db.Execute("DELETE FROM product" + entity.ToWhereString(), entity);
        }

        public void RemoveRange(IEnumerable<Product> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product obj)
        {
            _db.Execute("UPDATE product SET title = @Title, description = @Description, isbn = @ISBN, author = @author, list_price = @ListPrice, price = @Price, price50 = @Price50, price100 = @Price100, image_url = @ImageUrl, category_id = @CategoryId, cover_type_id = @CoverTypeId WHERE id = @id", obj);
        }
    }
}

