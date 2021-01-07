using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models
{
    public class Song
    {
        public int Id { get; set; }
        [Display(Name = "Song Name")]
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        [Display(Name = "Drop Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)] 
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        [Display(Name = "Album Cover")] 
        public string ImagePath { get; set; }
        [Display(Name = "Cost")] 
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }

        public override string ToString()
        {
            return $"{Title} by {Artist} on {Album}";
        }
    }
}