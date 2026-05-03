using consumirAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace consumirAPI.Service
{
    public interface IApiService
    {
        Task<Produto> GetById(int? Id);
        Task<IEnumerable<Produto>> GetAll();
        Task<Produto> Create(Produto produto);
        Task<bool> Update(int? Id, Produto produto);
        Task<bool> Delete(int? Id);
    }
}
