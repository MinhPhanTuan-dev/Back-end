using Microsoft.AspNetCore.Http;
using MISA.AMISDemo.Core.DTOs;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Interfaces
{
    public interface IBaseServices<Entity> where Entity : class
    {
       MISAServicesResult InsertService(Entity entity);
       List<Entity> GetAsync();
        object? CheckFileImport(IFormFile formFile);
    }
}
