using BusinessLayer.Interface;
using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
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
                return NotesRL.IsPin(NotesId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsTrash(long NotesId)
        {
            try
            {
                return NotesRL.IsTrash(NotesId);
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
                return NotesRL.IsPin(NotesId);
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
    }
}
