using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface ILableRL
    {
        Task AddLable(int userID, int noteID, string LableName);
        Task DeleteLable(int LableId, int userID);
    }
}
