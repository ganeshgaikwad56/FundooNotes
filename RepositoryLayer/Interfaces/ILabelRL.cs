using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILabelRL
    {
        Task AddLabel(int userId, int noteId,string labelName);
        Task CreatLabel(int userId,string labelName);
        Task<Entities.Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);

        Task<List<Entities.Label>> GetLabelByuserId(int userId);
        Task<List<Entities.Label>> GetlabelByNoteId(int NoteId);
    }
}
