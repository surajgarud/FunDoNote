﻿using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDo_notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        private readonly ICollaboratorBL collabBL;
        public CollabController(ICollaboratorBL collabBL)
        {
            this.collabBL = collabBL;
        }
        [HttpPost("AddCollab")]
        public IActionResult AddCollab(string email, long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(a => a.Type == "Id").Value);
                CollaboratorModel collaborator = new CollaboratorModel();
                collaborator.Id = userId;
                collaborator.NotesId = noteId;
                collaborator.collabEmail = email;
                var result = this.collabBL.AddCollaborator(collaborator);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collaborator Added  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collaborator Add Failed ! Try Again" });

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpDelete("Remove")]
        public IActionResult DeleteCollab(long collabId)
        {
            try
            {

                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.DeleteCollab(userId, collabId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = " Collab Removed  successfully ", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Collab Remove Failed ! Try Again" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("{noteId}/Get")]
        public List<CollaboratorEntity> GetByNoteId(long noteId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.collabBL.GetByNoteId(noteId, userId);
                if (result != null)
                {
                    return result;
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
