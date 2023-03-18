using System;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository
{
	public interface ICoverTypeRepository
	{
        void Update(CoverType obj);
        CoverType GetFirstOrDefault(Func<CoverType, bool> filter);
        IEnumerable<CoverType> GetAll();
        void Add(CoverType entity);
        void Remove(CoverType entity);
        void RemoveRange(IEnumerable<CoverType> entity);
    }
}

