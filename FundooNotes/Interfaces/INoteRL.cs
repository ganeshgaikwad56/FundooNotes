using CommanLayer.Users;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface INoteRL
    {
        Task AddNote( int userID, NotePostModel notePostModel);
        Task DeleteNote( int userID, int noteID);
        Task ChangeColour(int userID, int noteID, string colour); 
        Task ArchieveNote(int userID, int noteID);
        Task PinNote(int userID, int noteID);
        Task TrashNote(int userID, int noteID);
        Task ReminderNote(int userID, int noteID, DateTime dateTime);
        Task<Note> UpdateNote(int userID, int noteID, NoteUpdateModel noteUpdateModel);
        Task<List<Note>> GetAllNotes(int userID);

    }
}
