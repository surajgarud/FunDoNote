using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FunDoContext funDoContext;
        private IConfiguration _config;

        //Constructor
        public NotesRL(FunDoContext fundooContext, IConfiguration configuration)
        {
            this.funDoContext = fundooContext;
            this._config = configuration;

        }
        //Method to Notes Details.
        public NotesEntity CreateNote(NotesModel Notes, long userId)
        {
            try
            {
                NotesEntity newNotes = new NotesEntity();
                newNotes.Title = Notes.Title;
                newNotes.Description = Notes.Description;
                newNotes.colour = Notes.colour;
                newNotes.Image = Notes.Image;
                newNotes.IsArchieve = Notes.IsArchieve;
                newNotes.IsTrash = Notes.IsTrash;
                newNotes.IsPin = Notes.IsPin;
                newNotes.CreatedAt = Notes.CreatedAt;
                newNotes.ModifyAt = Notes.ModifyAt;
                newNotes.Id = userId;
                funDoContext.Notes.Add(newNotes);
                int result = funDoContext.SaveChanges();
                if (result > 0)
                    return newNotes;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NotesEntity UpdateNote(UpdateModel updateNote, long noteId)
        {

            try
            {
                var note = funDoContext.Notes.Where(u => u.NotesId == noteId).FirstOrDefault();
                if (note != null)
                {
                    note.Description = updateNote.Description;
                    note.colour = updateNote.colour;
                    note.Id = noteId;
                    funDoContext.Notes.Update(note);
                    int result = funDoContext.SaveChanges();
                    return note;
                }

                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NotesEntity IsPin(long NotesId)
        {
            try
            {
                var note = this.funDoContext.Notes.Where(n => n.NotesId == NotesId).FirstOrDefault();
                if (note != null)
                {

                    return note;
                }
                else
                {
                    return null;
                }
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
                var note = this.funDoContext.Notes.Where(n => n.NotesId == NotesId).FirstOrDefault();
                if (note != null)
                {
                    this.funDoContext.Notes.Remove(note);

                    this.funDoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
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
                var notes = funDoContext.Notes.FirstOrDefault(e => e.NotesId == NotesId && e.Id == userId);

                if (notes != null)
                {
                    if (notes.IsPin == true)
                    {
                        notes.IsPin = false;
                    }
                    else if (notes.IsPin == false)
                    {
                        notes.IsPin = true;
                    }
                    notes.ModifyAt = DateTime.Now;
                }
                int changes = funDoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
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
                var notes = funDoContext.Notes.FirstOrDefault(e => e.NotesId == NotesId && e.Id == userId);

                if (notes != null)
                {
                    if (notes.IsArchieve == true)
                    {
                        notes.IsArchieve = false;
                    }
                    else if (notes.IsArchieve == false)
                    {
                        notes.IsArchieve = true;
                    }
                    notes.ModifyAt = DateTime.Now;
                }
                int changes = funDoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
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
                var notes = funDoContext.Notes.FirstOrDefault(e => e.NotesId == NotesId);

                if (notes != null)
                {
                    if (notes.IsTrash == true)
                    {
                        notes.IsTrash = false;
                    }
                    else if (notes.IsTrash == false)
                    {
                        notes.IsTrash = true;
                    }
                    notes.ModifyAt = DateTime.Now;
                }
                int changes = funDoContext.SaveChanges();

                if (changes > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
