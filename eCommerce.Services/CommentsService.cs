using eCommerce.Data;
using eCommerce.Entities;
using eCommerce.Entities.CustomEntities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class CommentsService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public CommentsService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public bool AddComment(Comment comment)
        {
            

            _eCommerceContext.Comments.Add(comment);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public List<Comment> SearchComments(int? entityID, int? recordID, string userID, string searchTerm, int? pageNo, int recordSize, out int count)
        {
            

            var comments = _eCommerceContext.Comments
                                  .Where(x => !x.IsDeleted)
                                  .AsQueryable();

            if (entityID.HasValue && entityID.Value > 0)
            {
                comments = comments.Where(x => x.EntityID == entityID.Value);
            }

            if (recordID.HasValue && recordID.Value > 0)
            {
                comments = comments.Where(x => x.RecordID == recordID.Value);
            }

            if (!string.IsNullOrEmpty(userID))
            {
                comments = comments.Where(x => x.UserID == userID);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                comments = comments.Where(x => x.Text.ToLower().Contains(searchTerm.ToLower()));
            }

            count = comments.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return comments.Include("User")
                           .Include("User.Picture")
                           .OrderByDescending(x => x.TimeStamp)
                           .Skip(skipCount)
                           .Take(recordSize)
                           .ToList();
        }
        
        public Comment GetCommentByID(int ID)
        {
            

            var comment = _eCommerceContext.Comments.FirstOrDefault(x => x.ID == ID);

            return comment != null && !comment.IsDeleted ? comment : null;
        }

        public ProductRating GetProductRating(int productID)
        {
            

            var productRating = new ProductRating();

            var productComments = _eCommerceContext.Comments.Where(x => !x.IsDeleted && x.EntityID == (int)EntityEnums.Product && x.RecordID == productID);

            productRating.TotalRatings = productComments.Count();
            productRating.AverageRating = productRating.TotalRatings > 0 ? (int)productComments.Average(x => x.Rating) : 0;

            return productRating;
        }

        public bool DeleteComment(Comment comment)
        {
            

            //var comment = _eCommerceContext.Comments.Find(ID);

            comment.IsDeleted = true;

            _eCommerceContext.Entry(comment).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }
    }
}
