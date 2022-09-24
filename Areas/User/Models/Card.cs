using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPlanningApp.Areas.User.Models
{
    public class Card
    {
        public Card()
        {
            Labels = new List<Label>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public string CoverColour { get; set; }
        public List<Label> Labels { get; set; }

    }
}
