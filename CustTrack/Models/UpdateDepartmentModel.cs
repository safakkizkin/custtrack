using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class UpdateDepartmentModel
    {
        public IList<T_Employee> EmployeeModel { get; set; }
        public IList<T_Employee> ManagerModel { get; set; }
        public T_Department Department { get; set; }
    }
}