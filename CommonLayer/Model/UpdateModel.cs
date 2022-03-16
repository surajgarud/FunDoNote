using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UpdateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Reminder { get; set; }
        public DateTime? ModifyAt { get; set; }

    }
}
