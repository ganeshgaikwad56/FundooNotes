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
    public class LableRL : ILableRL
    {
        //Instances of fundoocontext
        FundooContextDB fundooContext;
        public IConfiguration Configuration { get; }

        public LableRL(FundooContextDB fundooContext, IConfiguration Configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = Configuration;

        }

        public async Task AddLable(int userID, int noteID, string LableName)
        {
            try
            {
                Lable lable = new Lable();
                lable.UserID = userID;
                lable.NoteId = noteID;
                lable.LableName = LableName;

                fundooContext.Lables.Add(lable);
                await fundooContext.SaveChangesAsync();
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
                var result = fundooContext.Lables.FirstOrDefault(x => x.LableId == LableId && x.UserID == userID);
                if (result != null)
                {
                    fundooContext.Lables.Remove(result);
                    await fundooContext.SaveChangesAsync();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
