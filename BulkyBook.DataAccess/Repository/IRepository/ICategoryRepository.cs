using System;
using System.Linq.Expressions;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository
{
	public interface ICategoryRepository
	{
		void Update(Category obj);
        //Category GetFirstOrDefault(Func<Category, bool> filter);
        Category GetFirstOrDefault(Category obj);
        IEnumerable<Category> GetAll();
        void Add(Category entity);
        void Remove(Category entity);
        void RemoveRange(IEnumerable<Category> entity);
    }
}

