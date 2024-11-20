using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class DepartmentServices : BaseServices<Department>, IDepartmentServices
    {
        public DepartmentServices(IDepartmentRepository DepartmentRepository) : base(DepartmentRepository)
        {
        }

    }
}

