using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class EmployeesModel
    {
        public IList<T_Department> _Departmans { get; set; }
        public IList<T_Employee> _Employees { get; set; }
        public IList<T_Authority> _Authorities { get; set; }
    }
}