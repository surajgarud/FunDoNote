using BusinessLayer.Interface;
using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunDo_notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INotesBL NotesBL;
        public NotesController(INotesBL NotesBL)
        {
            this.NotesBL = NotesBL;
        }
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(NotesModel Notes)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = NotesBL.CreateNote(Notes, userId);

                if (result != null)
                    return this.Ok(new { success = true, message = "Create Note Successful",data = result });
                else
                    return this.BadRequest(new { success = false, message = " Note Creation UnSuccessful", data = result });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPut("UpdateNote")]
        public IActionResult UpdateNote(UpdateModel update, long NotesId)
        {
            try
            {
                var result = NotesBL.UpdateNote(update, NotesId);
                if (result != null)
                    return this.Ok(new { success = true, message = "Update Notes Successful" });
                else
                    return this.BadRequest(new { success = false, message = "Update Notes UnSuccessful" });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet("{id}/Get")]
        public NotesEntity Retrieve(long NotesId)
        {
            try
            {
                var result = this.NotesBL.Retrieve(NotesId);
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
        [HttpDelete("Delete")]
        public IActionResult Delete(long NotesId)
        {
            try
            {
                var notes = this.NotesBL.Delete(NotesId);
                if (!notes)
                {
                    return this.BadRequest(new { Success = false, message = "failed to Delete the note" });
                }
                else
                {
                    return this.Ok(new { Success = true, message = " Note is Deleted successfully ", data = notes });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Pinned")]
        public IActionResult IsPin(long NotesId, long userID)
        {
            bool result = NotesBL.IsPin(NotesId, userID);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Archieve")]
        public IActionResult IsArchieve(long NotesId, long userID)
        {
            bool result = NotesBL.IsArchieve(NotesId, userID);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("Trash")]
        public IActionResult IsTrash(long NotesId)
        {
            bool result = NotesBL.IsTrash(NotesId);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
