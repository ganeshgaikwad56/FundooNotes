using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LableBL : ILableBL
    {
        ILableRL lableRL;
        public LableBL(ILableRL lableRL)
        {
            this.lableRL = lableRL;
        }

        public async Task AddLable(int userID, int noteID, string LableName)
        {
            try
            {
                await lableRL.AddLable(userID, noteID, LableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLable(int LableId, int userID)
        {
            try
            {
                await lableRL.DeleteLable(LableId, userID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
