using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class UpdateDepartmentModel
    {
        public IList<T_Employee> _Employees { get; set; }
        public T_Department _Department { get; set; }
        public IList<T_Authority> _Authorities { get; set; }
    }
}