using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class EmployeesModel
    {
        public IList<T_Department> Departmans { get; set; }
        public IList<T_Employee> Employees { get; set; }
        public IList<T_Authority> Authorities { get; set; }
    }
}