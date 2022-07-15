using BusinessLayer.Interfaces;
using CommanLayer.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        FundooContextDB fundooContext;
        INoteBL NoteBL;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NoteController(INoteBL NoteBL, FundooContextDB fundoos, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.NoteBL = NoteBL;
            this.fundooContext = fundoos;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("AddNote")]

        public async Task<ActionResult> AddNote(NotePostModel notePostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);

                await this.NoteBL.AddNote(UserId, notePostModel);
                return this.Ok(new { success = true, message = "Note Added Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpPut("ChangeColour/{noteID}/{colour}")]
        public async Task<ActionResult> ChangeColour(int noteID, string colour)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.NoteBL.ChangeColour(UserId, noteID, colour);
                return this.Ok(new { success = true, message = "Colour change successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpDelete("DeleteNote/{noteID}")]
        public async Task<ActionResult> DeleteNote(int noteID)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.NoteBL.DeleteNote(UserId, noteID);
                return this.Ok(new { success = true, message = "Note Deleted Successfully" });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("ArchieveNote/{noteID}")]
        public async Task<ActionResult> ArchieveNote(int noteID)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.NoteBL.ArchieveNote(UserId, noteID);
                return this.Ok(new { success = true, message = "Archieve Note Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("PinNote/{noteID}")]
        public async Task<ActionResult> PinNote(int noteID)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                await this.NoteBL.PinNote(UserId, noteID);
                return this.Ok(new { success = true, message = "Pin Note Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("TrashNote/{noteID}")]
        public async Task<ActionResult> TrashNote(int noteID)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.NoteBL.TrashNote(UserId, noteID);
                return this.Ok(new { success = true, message = "Trash Note Success" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("ReminderNote/{noteID}/{dateTime}")]
        public async Task<ActionResult> ReminderNote(int noteID, DateTime dateTime)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This noteID is doesn't exist" });
                }
                await this.NoteBL.ReminderNote(UserId, noteID, dateTime);
                return this.Ok(new { success = true, message = "Reminder set Successfully" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllNotes")]
        public async Task<ActionResult> GetAllNotes()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                List<Note> notes = await this.NoteBL.GetAllNotes(UserId);
                return this.Ok(new { success = true, message = "These Nots are:", data = notes });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("UpdateNote/{noteID}")]
        public async Task<ActionResult> UpdateNote(int noteID, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int UserId = Int32.Parse(userid.Value);
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == UserId && x.NoteId == noteID);
                if (note == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to update" });
                }
                await this.NoteBL.UpdateNote(UserId, noteID, noteUpdateModel);
                return this.Ok(new { success = true, message = "Note Updateed Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("GetAllNotesRedis")]
        public async Task<ActionResult> GetAllNote()
        {
            try
            {
                string serializeNoteList;
                string key = "Ganesh";
                var noteList = new List<Note>();
                var redisNoteList = await distributedCache.GetAsync(key);
                if (redisNoteList != null)
                {
                    serializeNoteList = Encoding.UTF8.GetString(redisNoteList);
                    noteList = JsonConvert.DeserializeObject<List<Note>>(serializeNoteList);
                }
                else
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                    int userID = Int32.Parse(userid.Value);
                    noteList = await this.NoteBL.GetAllNotes(userID);
                    serializeNoteList = JsonConvert.SerializeObject(noteList);
                    redisNoteList = Encoding.UTF8.GetBytes(serializeNoteList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    await distributedCache.SetAsync(key, redisNoteList, option);
                }
                return this.Ok(new { success = true, message = "Get note successful!!!", data = noteList });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
