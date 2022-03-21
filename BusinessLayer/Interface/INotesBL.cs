using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.entity;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface INotesBL
    {
        public NotesEntity CreateNote(NotesModel Notes, long userId);
        public NotesEntity UpdateNote(UpdateModel updateNote, long NotesId);
        public NotesEntity Retrieve(long NotesId);
        public List<NotesEntity> GetAllNotes();
        public bool Delete(long NotesId);
        public bool IsPin(long NotesId, long userId);
        public bool IsArchieve(long NotesId, long userId);
        public bool IsTrash(long NotesId, long userId);
        public NotesEntity UploadImage(long NotesId, long userId, IFormFile image);
        public bool ChangeColor(long NotesId, long userId, ChangeColour notesModel);
    }
}
