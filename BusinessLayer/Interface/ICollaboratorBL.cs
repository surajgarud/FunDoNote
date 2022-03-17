using CommonLayer.Model;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICollaboratorBL
    {
        public CollaboratorEntity AddCollaborator(CollaboratorModel collabModel);
        public CollaboratorEntity DeleteCollab(long userId, long collabId);
        public List<CollaboratorEntity> GetByNoteId(long noteId, long userId);
    }
}
