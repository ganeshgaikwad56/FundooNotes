using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommanLayer.Users
{
    public class NotePostModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        
        public string Colour { get; set; }
    }
}
