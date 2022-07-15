using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.FundooContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    public class LableController : ControllerBase
    {
        FundooContextDB fundooContext;
        ILabelBL labelBL;
        public LableController(ILabelBL LabelB, FundooContextDB fundoos)
        {
            this.labelBL=LabelB;
            this.fundooContext = fundoos;
        }

        [Authorize]
        [HttpPost("LableName/{noteID}")]
        public async Task<ActionResult> AddLabel(int noteID,string LableName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                //int noteId = Int32.Parse(userid.Value);

                await this.labelBL.AddLabel(userID, noteID,LableName );
                return this.Ok(new { success = true, message = "Lable Added Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost("CreatLabel/{labelName}")]
        public async Task<ActionResult> CreatLabel(string labelName)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                //int noteId = Int32.Parse(userid.Value);

                await this.labelBL.CreatLabel(userId, labelName);
                return this.Ok(new { success = true, message = "Labele created Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Authorize]
        [HttpDelete("DeleteLable/{LabelId}")]
        public async Task<ActionResult> DeleteLable(int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                var lable = fundooContext.Labels.FirstOrDefault(x => x.LableId == LabelId && x.UserID==userID);
                if (lable == null)
                {
                    return this.BadRequest(new { success = false, message = "Sorry! This LableID is doesn't exist" });
                }
                await this.labelBL.DeleteLabel(LabelId, userID);
                return this.Ok(new { success = true, message = "Lable Deleted Successfully " });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPut("UpdateLabel/{LabelId}/{LabelName}")]
        public async Task<ActionResult> UpdateLabel(string LabelName, int LabelId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                var result = await this.labelBL.UpdateLabel(userId, LabelId, LabelName);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, message = "Updation of Label failed" });
                }
                return this.Ok(new { success = true, message = $"Label updated successfully", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetLabelByuserId/{userID}")]
        public async Task<ActionResult> GetLabelByuserId()
        {
            try
            {
                List<RepositoryLayer.Entities.Label> list = new List<RepositoryLayer.Entities.Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                list = await this.labelBL.GetLabelByuserId(userID);
                if (list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetlabelByNoteId/{NoteId}")]
        public async Task<ActionResult> GetLabelByNoteId(int NoteId)
        {
            try
            {
                List<RepositoryLayer.Entities.Label> list = new List<RepositoryLayer.Entities.Label>();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserID", StringComparison.InvariantCultureIgnoreCase));
                int userID = Int32.Parse(userid.Value);
                list = await this.labelBL.GetLabelByuserId(NoteId);
                if (list == null)
                {
                    return this.BadRequest(new { success = false, message = "Failed to get label" });
                }
                return this.Ok(new { success = true, message = $"Label get successfully", data = list });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
