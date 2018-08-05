using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class UpdateEmployeeModel
    {
        public T_Department Deparment { get; set; }
        public IList<T_Department> Departments { get; set; }
        public IList<T_Employee> Managers { get; set; }
        public T_Employee Employee { get; set; }
    }
}