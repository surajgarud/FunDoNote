using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CollaboratorRL : ICollaboratorRL
    {
        private readonly FunDoContext funDoContext;

        public CollaboratorRL(FunDoContext funDoContext)
        {
            this.funDoContext = funDoContext;
        }
        public CollaboratorEntity AddCollaborator(CollaboratorModel collabModel)
        {
            try
            {
                CollaboratorEntity collaboration = new CollaboratorEntity();
                var user = funDoContext.User.Where(e => e.Email == collabModel.collabEmail).FirstOrDefault();

                var notes = funDoContext.Notes.Where(e => e.NotesId == collabModel.NotesId && e.Id == collabModel.Id).FirstOrDefault();
                if (notes != null && user != null)
                {
                    collaboration.NotesId = collabModel.NotesId;
                    collaboration.CollabEmail = collabModel.collabEmail;
                    collaboration.Id = collabModel.Id;
                    funDoContext.CollabTable.Add(collaboration);
                    var result = funDoContext.SaveChanges();
                    return collaboration;
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
        public CollaboratorEntity DeleteCollab(long userId, long collabId)
        {
            try
            {
                var data = this.funDoContext.CollabTable.FirstOrDefault(d => d.Id == userId && d.CollabId == collabId);
                if (data != null)
                {
                    this.funDoContext.CollabTable.Remove(data);
                    this.funDoContext.SaveChanges();
                    return data;
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
        public List<CollaboratorEntity> GetByNoteId(long NotesId, long userId)
        {
            try
            {
                var data = this.funDoContext.CollabTable.Where(c => c.NotesId == NotesId && c.Id == userId).ToList();
                if (data != null)
                {
                    return data;
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
    }
}
