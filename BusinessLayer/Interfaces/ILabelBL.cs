using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILabelBL
    {
        Task AddLabel(int userID, int noteID, string LabelName);
        Task<RepositoryLayer.Entities.Label> UpdateLabel(int userId, int LabelId, string LabelName);
        Task DeleteLabel(int LabelId, int userId);

        Task<List<RepositoryLayer.Entities.Label>> GetLabelByuserId(int userId);
        Task<List<RepositoryLayer.Entities.Label>> GetlabelByNoteId(int NoteId);
    }
}
