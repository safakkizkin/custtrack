using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class UpdateEmployeeModel
    {
        public T_Department _Department { get; set; }
        public IList<T_Department> _Departments { get; set; }
        public T_Authority _Authority { get; set; }
        public IList<T_Authority> _Authorities { get; set; }
        public IList<T_Employee> _Managers { get; set; }
        public T_Employee _Employee { get; set; }
    }
}