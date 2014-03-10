using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class PhoneNumber
    {
        public PhoneNumber(string phoneNumber)
        {
            Number = phoneNumber;
        }

        public string Number { get; set; }
    }
}
