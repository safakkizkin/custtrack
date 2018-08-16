namespace CustTrack.Models.CustomerModel
{
    using CustTrack.Models.T_Employee;
    using CustTrack.Models.T_Customer;

    public class CustomerModel
    {
        public T_Customer cust { get; set; }
        public T_Employee emp { get; set; }
    }
}