using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPlanningApp.Areas.User.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string CoverColour { get; set; }
        public List<Label> Label { get; set; }
    }
}
