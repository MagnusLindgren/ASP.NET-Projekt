using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt.Model
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Place { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int SpotsAvailable { get; set; }

        [InverseProperty("HostedEvents")]
        public User Organizer { get; set; }
        [InverseProperty("JoinedEvents")]
        public List<User> Attendees { get; set; }
    }
}
