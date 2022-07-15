using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userId, int noteId, string labelName);
        Task CreatLabel(int userId, string labelName);
        Task<RepositoryLayer.Entities.Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);

        Task<List<RepositoryLayer.Entities.Label>> GetLabelByuserId(int userId);
        Task<List<RepositoryLayer.Entities.Label>> GetlabelByNoteId(int NoteId);
    }
}
