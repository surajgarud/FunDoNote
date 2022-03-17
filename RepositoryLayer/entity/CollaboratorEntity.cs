using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.VisualStudio.Services.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.entity
{
    public class CollaboratorEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CollabId { get; set; }
        public string CollabEmail { get; set; }

        [ForeignKey("user")]
        public long Id { get; set; }
        public virtual userEntity user { get; set; }

        [ForeignKey("notes")]
        public long NotesId { get; set; }

        public virtual NotesEntity notes { get; set; }
    }
}
