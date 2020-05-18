using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStrore2020.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Display(Name = "Customer Name")]
        public string Name { get; set; }

        public int Age { get; set; }

        [Display(Name = "Favortie Genre")]
        public string FavoriteGenre { get; set; }

        [Display(Name = "Favortie Song")]
        public string FavoriteSong { get; set; }
        
        public string ImagePath { get; set; }
    }
}
