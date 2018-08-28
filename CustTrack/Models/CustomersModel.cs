using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class CustomersModel
    {
        public IList<T_Customer> _Custs { get; set; }
        public IList<T_Employee> _Emps  { get; set; }
    }
}