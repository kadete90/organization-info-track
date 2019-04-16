using System.ComponentModel.DataAnnotations;

namespace InfoTrack.App.Models
{
    public class SearchModel
    {
        [Required]
        public string keyword { get; set; }

        //[Url] // this requires protocol
        [Required]
        public string findUri { get; set; }
    }
}
