using PropertyAPI.DTO;
using PropertyAPI.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PropertyAPI.Services
{
    public interface IPropertyService
    {
        Task<IEnumerable<PropertyContent>> GetProperties();
        Task<Response> SaveRecord(PropertyContent newRecord);
    }
}
