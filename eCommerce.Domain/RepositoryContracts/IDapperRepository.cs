using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domain.RepositoryContracts
{
    public interface IDapperRepository
    {
        Task<T?> GetTAsync<T>(string sql, object? parameters = null);
       
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object? parameters = null);
        Task<int> ExecuteAsync(string sql, object? parameters = null);
    }
}
