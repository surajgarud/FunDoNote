using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CollaboratorBL : ICollaboratorBL
    {
            private readonly ICollaboratorRL CollabRL;
            public CollaboratorBL(ICollaboratorRL CollabRL)
            {
                this.CollabRL = CollabRL;
            }

        public CollaboratorEntity AddCollaborator(CollaboratorModel collabModel)
        {
            try
            {
                return this.CollabRL.AddCollaborator(collabModel);
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
                return this.CollabRL.DeleteCollab(userId, collabId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<CollaboratorEntity> GetByNoteId(long noteId, long userId)
        {
            try
            {
                return this.CollabRL.GetByNoteId(noteId, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
