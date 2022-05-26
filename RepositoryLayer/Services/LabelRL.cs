using Microsoft.EntityFrameworkCore;
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
    public class LabelRL : ILabelRL
    {
        FundooContextDB fundooContext;
        public IConfiguration Configuration { get; }
        public string LableName { get; private set; }

        public LabelRL(FundooContextDB fundooContext, IConfiguration Configuration)
        {
            this.fundooContext = fundooContext;
            this.Configuration = Configuration;

        }
        public async Task AddLabel(int userId, int noteId, string lableName)
        {
            try
            {
                var user = fundooContext.User.FirstOrDefault(u => u.UserID == userId);
                var note = fundooContext.Notes.FirstOrDefault(b => b.NoteId == noteId);
                Label label = new Label
                {
                    User = user,
                    Note = note
                };
                label.LableName = lableName;
                fundooContext.Labels.Add(label);
                await fundooContext.SaveChangesAsync();
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

                var result = fundooContext.Labels.FirstOrDefault(u => u.UserID == userId && u.LableId == LabelId);
                if (result != null)
                {

                    result.LableName = LabelName;

                    await fundooContext.SaveChangesAsync();
                }
                //return await fundooDBContext.Label.Where(u => u.UserId == userId && u.LabelId == LabelId).Include(u=>u.User).

                return await fundooContext.Labels
                .Where(u => u.UserID == userId && u.LableId == LabelId)
                .Include(u => u.User)
                .FirstOrDefaultAsync();
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
                var result = fundooContext.Labels.FirstOrDefault(u => u.LableId == LabelId && u.UserID == userId);
                fundooContext.Labels.Remove(result);
                await fundooContext.SaveChangesAsync();
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
                List<Label> reuslt = await fundooContext.Labels.Where(u => u.UserID == userId).ToListAsync();
                return reuslt;
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
                List<Label> reuslt = await fundooContext.Labels.Where(u => u.NoteId == NoteId).Include(u => u.User).ToListAsync();
                return reuslt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
