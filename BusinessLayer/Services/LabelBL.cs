using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class LabelBL : ILabelBL
    {
        ILabelRL labelRL;
        public LabelBL(ILabelRL labelRL)
        {
            this.labelRL = labelRL;
        }

        public async Task AddLabel(int userId, int noteId, string labelName)
        {
            try
            {
                await labelRL.AddLabel(userId, noteId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreatLabel(int userId, string labelName)
        {
            try
            {
                await labelRL.CreatLabel(userId, labelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteLabel(int LabelId, int userId)
        {
            try
           {
                await labelRL.DeleteLabel(LabelId, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> GetlabelByNoteId(int NoteId)
        {
            try
            {
               return await labelRL.GetlabelByNoteId(NoteId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Label>> GetLabelByuserId(int userId)
        {
            try
            {
                return await labelRL.GetLabelByuserId(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Label> UpdateLabel(int userId, int LabelId, string LabelName)
        {
            try
            {
                return await labelRL.UpdateLabel(userId, LabelId, LabelName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public async Task DeleteLable(int LableId, int userID)
        //{
        //    try
        //    {
        //        await lableRL.DeleteLable(LableId, userID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        // }
    }
}
