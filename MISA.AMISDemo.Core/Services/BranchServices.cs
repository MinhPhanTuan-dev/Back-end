using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class BranchServices : BaseServices<Branch>, IBranchServices
    {
        public BranchServices(IBranchRepository branchRepository) : base(branchRepository)
        {
        }
    }
}

