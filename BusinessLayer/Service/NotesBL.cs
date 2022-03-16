using BusinessLayer.Interface;
using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class NotesBL : INotesBL
    {
        private readonly INotesRL NotesRL;
        public NotesBL(INotesRL NotesRL)
        {
            this.NotesRL = NotesRL;
        }

        public bool ChangeColor(long NotesId, long userId, ChangeColour notesModel)
        {
            try
            {
                return NotesRL.ChangeColor(NotesId, userId, notesModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity CreateNote(NotesModel Notes, long userId)
        {
                try
                {
                    return NotesRL.CreateNote(Notes,userId);
                }
                catch (Exception)
                {

                    throw;
                }
        }

        public bool Delete(long NotesId)
        {
            try
            {
                return NotesRL.Delete(NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsArchieve(long NotesId, long userId)
        {
            try
            {
                return NotesRL.IsArchieve(NotesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool IsPin(long NotesId, long userId)
        {
            try
            {
                return NotesRL.IsPin(NotesId,userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsTrash(long NotesId, long userId)
        {
            try
            {
                return NotesRL.IsTrash(NotesId, userId);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public NotesEntity Retrieve(long NotesId)
        {
            try
            {
                return NotesRL.Retrieve(NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UpdateNote(UpdateModel updateNote, long NotesId)
        {
            try
            {
                return NotesRL.UpdateNote(updateNote, NotesId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public NotesEntity UploadImage(long NotesId, long userId, IFormFile image)
        {
            try
            {
                return NotesRL.UploadImage(NotesId, userId,image);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
