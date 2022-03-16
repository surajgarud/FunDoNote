using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Services.Account;
using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using Octokit;
using System.Text;
using Account = CloudinaryDotNet.Account;

namespace RepositoryLayer.Service
{
    public class NotesRL : INotesRL
    {
        private readonly FunDoContext funDoContext;
        //
        private readonly IConfiguration _config;

        //Constructor
        public NotesRL(FunDoContext fundooContext, IConfiguration _config)
        {
            this.funDoContext = fundooContext;
            this._config = _config;

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

        public NotesEntity UpdateNote(UpdateModel updateNote, long NotesId)
        {

            try
            {
                var note = funDoContext.Notes.Where(u => u.NotesId == NotesId).FirstOrDefault();
                if (note != null)
                {
                    note.Description = updateNote.Description;
                    note.Title = updateNote.Title;
                    note.Reminder = updateNote.Reminder;
                    note.ModifyAt = updateNote.ModifyAt;
                    note.Id = NotesId;
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
                var notes = funDoContext.Notes.FirstOrDefault(e => e.NotesId == NotesId);

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
        public bool IsTrash(long NotesId, long userId)
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

        public NotesEntity Retrieve(long NotesId)
        {
            try
            {
                // Fetch details with the given noteId.
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

                {
                    throw;
                }
            }
        }
        public NotesEntity UploadImage(long NotesId, long userId, IFormFile image)
        {
            try
            {
                // Fetch All the details with the given noteId and userId
                var note = this.funDoContext.Notes.FirstOrDefault(n => n.NotesId == NotesId && n.Id == userId);
                if (note != null)
                {
                    Account acc = new Account(_config["Cloudinary:CloudName"], _config["Cloudinary:ApiKey"], _config["Cloudinary:ApiSecret"]);
                    Cloudinary cloud = new Cloudinary(acc);
                    var imagePath = image.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(image.FileName, imagePath),
                    };
                    var uploadResult = cloud.Upload(uploadParams);
                    note.Image = image.FileName;
                    this.funDoContext.Notes.Update(note);
                    int upload = this.funDoContext.SaveChanges();
                    if (upload > 0)
                    {
                        return note;
                    }
                    else
                    {
                        return null;
                    }
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
        public bool ChangeColor(long NotesId, long userId, ChangeColour notesModel)
        {
            try
            {
                var result = funDoContext.Notes.FirstOrDefault(e => e.NotesId == NotesId && e.Id == userId);

                if (result != null)
                {
                    result.colour = notesModel.colour;
                    result.ModifyAt = DateTime.Now;
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
