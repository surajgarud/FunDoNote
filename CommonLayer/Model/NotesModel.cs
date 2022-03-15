using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NotesModel
    {
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
    }
}
