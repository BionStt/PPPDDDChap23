using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPPDDDChap23.EventSourcing.Application.Model.PayAsYouGo
{
    public class FreeCallAllowance
    {
        // Your allowances will expire after 30 days.  
        // Calls to standard UK mobiles and landlines (01, 02, 03) within the UK.

        public Allowance Allowance { get; set; }

        public void Subtract(int minutes)
        { 
        
        }

        public int MinutesWhichCanCover(PhoneCall phoneCall)
        {
            return 10;
        }
    }
}
