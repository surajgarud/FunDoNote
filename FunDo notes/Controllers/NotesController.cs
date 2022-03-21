using BusinessLayer.Interface;
using CommonLayer.Model;
using DocumentFormat.OpenXml.ExtendedProperties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunDo_notes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly INotesBL NotesBL;
        public NotesController(INotesBL NotesBL, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.NotesBL = NotesBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(NotesModel Notes)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = NotesBL.CreateNote(Notes, userId);

                if (result != null)
                    return this.Ok(new { success = true, message = "Create Note Successful", data = result });
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
        [HttpGet("GetAllNotes")]
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                var result = this.NotesBL.GetAllNotes();
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
        [HttpGet("{id}/Get")]
        public NotesEntity Retrieve(long NotesId)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = NotesBL.Retrieve(NotesId);
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
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "NotesList";
            string serializedNotesList;
            var NotesList = new List<NotesEntity>();
            var redisNotesList = await distributedCache.GetAsync(cacheKey);
            if (redisNotesList != null)
            {
                serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
            }
            else
            {
                //long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                NotesList = (List<NotesEntity>) this.NotesBL.GetAllNotes();
                serializedNotesList = JsonConvert.SerializeObject(NotesList);
                redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisNotesList, options);
            }
            return Ok(NotesList);
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
        [HttpPut("IsPinned")]
        public IActionResult IsPin(long NotesId)
        {
            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                bool result = this.NotesBL.IsPin(NotesId, userId);
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Pinned Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Pinned Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("IsArchieve")]
        public IActionResult IsArchieve(long NotesId)
        {

            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                bool result = this.NotesBL.IsArchieve(NotesId, userId);
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Archieve Is Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Archieve Is Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("IsTrash")]
        public IActionResult IsTrash(long NotesId)
        {

            try
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                bool result = this.NotesBL.IsArchieve(NotesId, userId);
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Trash Is Successful" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Trash Is Unsuccessful" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost("UploadImage")]
        public IActionResult UploadImage(long NotesId, IFormFile image)
        {
            try
            {
                // Authorise user by userId
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                var result = this.NotesBL.UploadImage(NotesId, userId, image);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Image Uploaded Successfully", data = result });


                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Image Upload Failed ! Try Again " });
                }
            }
            catch (Exception)
            {
                throw;
            }

        }
        [HttpPut("Colour")]
        public IActionResult ChangeColor(long noteId, ChangeColour notesModel)
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            bool result = NotesBL.ChangeColor(noteId, userId, notesModel);

            try
            {
                if (result == true)
                {
                    return Ok(new { Success = true, message = "Color changed Successfully !!" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Color not changed !!" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
