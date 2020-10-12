using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCruiserWebAPI.Models
{
    public class MovieList
    {
        [Key]
        public int MovieId { get; set; }

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string Name { get; set; }

        public int Price { get; set; }

        public bool IsAvailable { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Column(TypeName = "Date")]
        public DateTime LaunchDate { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
