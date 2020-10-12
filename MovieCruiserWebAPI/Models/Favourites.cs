using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class Favourites
    {
        [Key]
        public int FavouriteId { get; set; }

        [ForeignKey("Movie")]
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual MovieList Movie { get; set; }
    }
}
