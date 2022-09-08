using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPlanningApp.Areas.User.Models
{
    public class BoardList
    {
        public BoardList()
        {
            Cards = new List<Card>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
        public int BoardId { get; set; }
        [ForeignKey("BoardId")]
        public Board Board { get; set; }
    }
}
