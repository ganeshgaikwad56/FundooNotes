using System;
using RepositoryLayer.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colour { get; set; }

        public bool IsPin { get; set; }
        public bool IsTrash { get; set; }
        public bool IsReminder { get; set; }
        public bool IsArchieve { get; set; }

        public DateTime RegisterDate { get; set; }
        public DateTime ModifyDate { get; set; }
        public DateTime RemindereDate { get; set; }

        public int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual IList<Lable> Lables { get; set; }



    }
}
