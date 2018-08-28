using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class CustomerModel
    {
        public T_Customer _Cust { get; set; }
        public T_City _City { get; set; }
        public IList<T_City> _Cities { get; set; }
        public T_District _District { get; set; }
        public IList<T_District> _Districts { get; set; }
    }
}