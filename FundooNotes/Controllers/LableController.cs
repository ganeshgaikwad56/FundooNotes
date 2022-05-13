using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LableController : ControllerBase
    {
        FundooContextDB fundooContext;
        ILableBL LableBL;
        public LableController(ILableBL LableB, FundooContextDB fundoos)
        {
            this.LableBL=LableB;
            this.fundooContext = fundoos;
        }

        [Authorize]
        [HttpPost("LableName/{noteID}")]
        public async Task<ActionResult> AddLable(int noteID,string LableName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                //int noteId = Int32.Parse(userid.Value);

                await this.LableBL.AddLable(userID, noteID,LableName );
                return this.Ok(new { success = true, message = "Lable Added Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLable/{LableId}")]
        public async Task<ActionResult> DeleteLable(int LableId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                var lable = fundooContext.Lables.FirstOrDefault(x => x.LableId == LableId && x.UserID == userID);
                if (lable == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This LableID is doesn't exist" });
                }
                await this.LableBL.DeleteLable(LableId, userID);
                return this.Ok(new { success = true, message = "Lable Deleted Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
