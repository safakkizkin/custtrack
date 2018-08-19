using CustTrack.Models.EntityFramework;

namespace CustTrack.Models
{
    public class CustomerModel
    {
        public T_Customer _Cust { get; set; }
        public T_Employee _Emp { get; set; }
    }
}