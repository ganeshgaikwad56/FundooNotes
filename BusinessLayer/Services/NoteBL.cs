using BusinessLayer.Interfaces;
using CommanLayer.Users;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public async Task AddNote(int userID, NotePostModel notePostModel)
        {
            try
            {
                await noteRL.AddNote(userID, notePostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ArchieveNote(int userID, int noteID)
        {
            try
            {
                await noteRL.ArchieveNote(userID, noteID);
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
                await noteRL.ChangeColour(userID, noteID, colour);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteNote(int userID, int noteID)
        {
            try
            {
                await noteRL.DeleteNote(userID, noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Note>> GetAllNotes(int userID)
        {
            try
            {
               return await noteRL.GetAllNotes(userID);
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
                await noteRL.PinNote(userID, noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task ReminderNote(int userID, int noteID, DateTime dateTime)
        {
            try
            {
                await noteRL.ReminderNote(userID, noteID, dateTime);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task TrashNote(int userID, int noteID)
        {
            try
            {
                await noteRL.TrashNote(userID, noteID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Note> UpdateNote(int userID, int noteID, NoteUpdateModel noteUpdateModel)
        {
            try
            {
                return await noteRL.UpdateNote(userID, noteID, noteUpdateModel);
            }
           
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
