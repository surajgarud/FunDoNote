using RepositoryLayer.context;
using RepositoryLayer.entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Service
{
    public class LabelRL : ILabelRL
    {
        private readonly FunDoContext funDoContext;

        public LabelRL(FunDoContext fundooContext)
        {
            this.funDoContext = fundooContext;
        }
        public LabelEntity AddLabelName(string labelName, long noteId, long userId)
        {
            try
            {
                LabelEntity labelEntity = new LabelEntity
                {
                    LabelName = labelName,
                    Id = userId,
                    NotesId = noteId
                };
                this.funDoContext.Label.Add(labelEntity);
                int result = this.funDoContext.SaveChanges();
                if (result > 0)
                {
                    return labelEntity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public LabelEntity UpdateLabel(string labelName, long NotesId, long userId)
        {
           
            try
            {
                var label = funDoContext.Label.Where(u => u.NotesId == NotesId).FirstOrDefault();
                if (label != null)
                {
                    label.LabelName = labelName;
                    this.funDoContext.Label.Update(label);
                    this.funDoContext.SaveChanges();
                    return label;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<LabelEntity> GetByLabelId(long noteId)
        {
            try
            {
                var data = this.funDoContext.Label.Where(c => c.NotesId == noteId).ToList();
                if (data != null)
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool RemoveLabel(long labelId, long userId)
        {
            try
            {
                var labelDetails = this.funDoContext.Label.FirstOrDefault(l => l.LabelId == labelId && l.Id == userId);
                if (labelDetails != null)
                {
                    this.funDoContext.Label.Remove(labelDetails);

                    this.funDoContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
