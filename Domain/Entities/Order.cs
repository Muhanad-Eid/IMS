using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }  
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
