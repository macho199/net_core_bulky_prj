using System;
using BulkyBook.Utility;
using Npgsql;
using System.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Linq.Expressions;
using Dapper;

namespace BulkyBook.DataAccess.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
        private readonly IDbConnection _db;

        public CategoryRepository(NpgsqlConnection db)
        {
            _db = db;
        }

        public void Add(Category entity)
        {
            _db.Execute("INSERT INTO category(name, display_order, created_date_time) VALUES(@Name, @DisplayOrder, now())", entity);
        }

        public IEnumerable<Category> GetAll()
        {
            return _db.Query<Category>("SELECT id, name, display_order, created_date_time FROM category ORDER BY id DESC").ToList();
        }

        public Category GetFirstOrDefault(Func<Category, bool> filter)
        {
            return _db.Query<Category>("SELECT id, name, display_order, created_date_time FROM category").Where(filter).FirstOrDefault();
        }

        public void Remove(Category entity)
        {
            _db.Execute("DELETE FROM category WHERE id = @id", entity);
        }

        public void RemoveRange(IEnumerable<Category> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Category obj)
        {
            _db.Execute("UPDATE category SET name = @Name, display_order = @DisplayOrder WHERE id = @id", obj);
        }
    }
}

