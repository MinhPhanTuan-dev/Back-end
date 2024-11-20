using MISA.AMISDemo.Core.Interfaces;
using MISA.AMISDemo.Core.Entities;

namespace MISA.AMISDemo.Core.Services
{
    public class BankServices : BaseServices<Bank>,IBankServices
    {
        public BankServices(IBankRepository bankRepository) :base(bankRepository)
        {
        }
    }
}

