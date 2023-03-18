using System;
using BulkyBook.Models;
using Npgsql;
using System.Data;
using Dapper;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
	public class CoverTypeRepository : ICoverTypeRepository
	{
        private readonly IDbConnection _db;

        public CoverTypeRepository(NpgsqlConnection db)
        {
            _db = db;
        }

        public void Add(CoverType obj)
        {
            _db.Execute("INSERT INTO cover_type(name) VALUES(@Name)", obj);
        }

        public IEnumerable<CoverType> GetAll()
        {
            return _db.Query<CoverType>("SELECT id, name FROM cover_type ORDER BY id DESC").ToList();
        }

        public CoverType GetFirstOrDefault(Func<CoverType, bool> filter)
        {
            return _db.Query<CoverType>("SELECT id, name FROM cover_type").Where(filter).FirstOrDefault();
        }

        public void Remove(CoverType obj)
        {
            _db.Execute("DELETE FROM cover_type WHERE id = @id", obj);
        }

        public void RemoveRange(IEnumerable<CoverType> entity)
        {
            throw new NotImplementedException();
        }

        public void Update(CoverType obj)
        {
            _db.Execute("UPDATE cover_type SET name = @Name WHERE id = @id", obj);
        }
    }
}

