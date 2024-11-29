using SANA.Shop.DAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SANA.Shop.BLL.Services
{
    public interface IService<T> where T : class
    {
        IEnumerable<T> Get();
    }

    public class EmployeeService : IService<ProductViewModel>
    {
        DataAccess ds;
        public EmployeeService(DataAccess d)
        {
            ds = d;
        }
        public IEnumerable<ProductViewModel> Get()
        {
            return ds.Get();
        }
    }
}
