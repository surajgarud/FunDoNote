using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface INotesRL
    {
        public NotesEntity CreateNote(NotesModel Notes, long userId);
        public NotesEntity UpdateNote(UpdateModel updateNote, long NotesId);
        public NotesEntity IsPin(long NotesId);
        public bool Delete(long NotesId);
        public bool IsPin(long NotesId, long userId);
        public bool IsArchieve(long NotesId, long userId);
        public bool IsTrash(long NotesId);
    }
}
