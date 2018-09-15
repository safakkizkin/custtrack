using CustTrack.Models.EntityFramework;
using System.Collections.Generic;

namespace CustTrack.Models
{
    public class AppointmentModel
    {
        public List<T_Customer> _Customers { get; set; }
        public T_Appointment _Appointment { get; set; }
        public List<T_AppointmentColor> _AppointmentTypes { get; set; }
    }
}