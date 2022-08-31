using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPlanningApp.Areas.User.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [Display(Name ="Background Colour")]
        public string BackgroundColour { get; set; }
        public List<BoardList> Lists { get; set; }
    }
}
