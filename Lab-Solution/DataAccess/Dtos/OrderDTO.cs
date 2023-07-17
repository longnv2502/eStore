using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Dtos
{
    public class OrderDTO
    {
        public DateTime? RequiredDate { get; set; }
        public ICollection<OrderDetailDTO> OrderDetailDTOs { get; set; } = null!;

    }
}
