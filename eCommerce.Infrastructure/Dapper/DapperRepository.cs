using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using eCommerce.Domain.RepositoryContracts;
namespace eCommerce.Infrastructure.Dapper
{
    public class DapperRepository : IDapperRepository
    {
        public readonly IDbConnection _dbConnection;
        public DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<T?> GetTAsync<T>(string sql, object? parameters = null)
        {
            return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        }
        //public async Task<T> GetTAsync<T>(string query, object parameters)
        //{
        //    return await _dbConnection.QuerySingleOrDefaultAsync<T>(query, parameters);
        //}
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null)
        {            
            return await _dbConnection.QueryAsync<T>(sql, parameters);
        }

        public async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            return await _dbConnection.ExecuteAsync(sql, parameters);
        }

    }
}
