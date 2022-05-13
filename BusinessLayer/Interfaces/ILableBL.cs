using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ILableBL
    {
        Task AddLable(int userID, int noteID, string LableName);
        Task DeleteLable(int LableId, int userID);
    }
}
