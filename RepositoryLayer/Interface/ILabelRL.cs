using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ILabelRL
    {
        public LabelEntity AddLabelName(string labelName, long noteId, long userId);
        public LabelEntity UpdateLabel(string labelName, long NotesId, long userId);
        public List<LabelEntity> GetByLabelId(long noteId);
        public bool RemoveLabel(long labelId, long userId);
    }
}
