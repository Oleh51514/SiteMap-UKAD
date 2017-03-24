using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitemap.Common
{
    public interface IEmployeeRepository
    {
        int Save();
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        public int Save()
        {
            return 5;
        }
    }
}
