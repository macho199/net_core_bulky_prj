using System;
using BulkyBook.DataAccess.Repository.IRepository;
using Npgsql;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }

        public UnitOfWork(NpgsqlConnection db)
        {
            Category = new CategoryRepository(db);
            CoverType = new CoverTypeRepository(db);
        }
    }
}

