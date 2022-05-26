using CommanLayer.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.FundooContext;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {
        //Instances of fundoocontext
        FundooContextDB fundooContext;
        public IConfiguration Configuration { get; }

        public NoteRL(FundooContextDB fundooContext, IConfiguration Configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = Configuration;

        }
        public async Task AddNote(int userID, NotePostModel notePostModel)
        {
            try
            {
                Note note = new Note();
                note.UserID = userID;

                note.Title = notePostModel.Title;
                note.Description = notePostModel.Description;
                note.Colour = notePostModel.Colour;
                note.IsPin = false;
                note.IsArchieve = false;
                note.IsReminder = false;
                note.IsTrash = false;
                note.RegisterDate = DateTime.Now;
                note.ModifyDate = DateTime.Now;               
                
                fundooContext.Notes.Add(note);
                await fundooContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ChangeColour(int userID, int noteID, string colour)
        {
            try
            {
                var note=fundooContext.Notes.FirstOrDefault(x =>x.UserID==userID && x.NoteId==noteID);
                if (note!=null)
                {
                    note.Colour = colour;
                    await fundooContext.SaveChangesAsync();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task ArchieveNote(int userID, int noteID)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserID == userID && u.NoteId == noteID);
                if(note!=null)
                {
                    if(note.IsArchieve == true)
                    {
                        note.IsArchieve = false;
                    }
                    if(note.IsArchieve == false)
                    {
                        note.IsArchieve = true;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Note> UpdateNote(int userID, int noteID, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserID == userID && u.NoteId == noteID);
                if (note != null)
                {
                    note.Title = noteUpdateModel.Title;
                    note.Description = noteUpdateModel.Description;
                    note.Colour = noteUpdateModel.Colour;
                    note.IsArchieve = noteUpdateModel.IsArchieve;
                    note.IsPin = noteUpdateModel.IsPin;
                    note.IsTrash = noteUpdateModel.IsTrash;
                    note.IsReminder = noteUpdateModel.IsReminder;

                    await fundooContext.SaveChangesAsync();
                    
                }
                return await fundooContext.Notes
                        .Where(u => u.UserID == userID && u.NoteId == noteID)
                        .Include(b => b.User)
                        .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task PinNote(int userID, int noteID)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserID == userID && u.NoteId == noteID);
                if (note != null)
                {
                    if (note.IsPin == true)
                    {
                        note.IsPin = false;
                    }
                    if (note.IsPin == false)
                    {
                        note.IsPin = true;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task TrashNote(int userID, int noteID)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserID == userID && u.NoteId == noteID);
                if (note != null)
                {
                    if (note.IsTrash == true)
                    {
                        note.IsTrash = false;
                    }
                    if (note.IsTrash == false)
                    {
                        note.IsTrash = true;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteNote(int userID, int noteID)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(x => x.UserID == userID && x.NoteId == noteID);
                if (note != null)
                {
                    fundooContext.Notes.Remove(note);
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task ReminderNote(int userID, int noteID, DateTime dateTime)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(u => u.UserID == userID && u.NoteId == noteID);
                if (note != null)
                {
                    
                    if(note.IsReminder == true)
                    {
                        note.RemindereDate = dateTime;
                    }
                    await fundooContext.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Note>> GetAllNotes(int userID)
        {
            try
            {
                //List<Note> list = new List<Note>();
               return await fundooContext.Notes.Where(u => u.UserID == userID).Include(u => u.User).ToListAsync();
               // return list;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
