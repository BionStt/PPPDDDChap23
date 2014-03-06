using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneCall
    {
        public DateTime startTime {get; set;} 
        public int minutes {get; set;}
        public PhoneNumber numberDialled { get; set; }
    }
}
