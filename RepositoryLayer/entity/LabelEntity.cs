using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long LabelId { get; set; }
        public string LabelName { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }

        public virtual userEntity user { get; set; }

        [ForeignKey("notes")]
        public long NotesId { get; set; }

        public virtual NotesEntity notes { get; set; }
    }
}
