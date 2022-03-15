using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.entity
{
    public class NotesEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long NotesId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public string colour { get; set; }
        public string Image { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPin { get; set; }
        public bool IsArchieve { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ModifyAt { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }
        public userEntity user { get; set; }
    }
}
