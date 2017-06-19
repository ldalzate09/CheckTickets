using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckTickets.Models
{
    public class Ticket
    {
        
        public int TicketId { get; set; }
        public string TicketCode { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public override int GetHashCode()
        {
            return TicketId;
        }
    }
}
