using System;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Lable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LableId { get; set; }
        public string LableName { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }

        public int? NoteId { get; set; }      
        public virtual Note Note { get; set; }

    }
}
