using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewMe.DataAccess;
using ReviewMe.DataAccess.Repository;
using ReviewMe.Model;

namespace ReviewMe.Bal
{
    public class CommentBal
    {
        private readonly Repository<Comment> _commentRepository = new Repository<Comment>(new EntityContext());

        public List<Comment> GetAllComments()
        {
            try
            {
                List<Comment> commentList = _commentRepository.GetAll();
                return commentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Comment GetCommentById(long id)
        {
            try 
            { 
                Comment comment = _commentRepository.GetById(id);
                return comment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddComment(Comment comment)
        {
            try
            {
                var model = _commentRepository.Add(comment);
                if (model != null)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Comment SaveOrUpdateComment(Comment comment)
        {
            try
            {
                Comment entity = _commentRepository.SaveOrUpdate(comment);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteComment(long id)
        {
            try
            {
                var response = _commentRepository.Delete(id);
                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
