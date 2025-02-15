using eCommerce.Data;
using eCommerce.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class SharedService
    {
        private readonly eCommerceContext _eCommerceContext;
        public SharedService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }

        public int SavePicture(Picture picture)
        {
            

            _eCommerceContext.Pictures.Add(picture);

            _eCommerceContext.SaveChanges();

            return picture.ID;
        }

        public bool SaveNewsletterSubscription(NewsletterSubscription newsletterSubscription)
        {
            

            //check for an existing subscription.
            var existingSubscription = _eCommerceContext.NewsletterSubscriptions.FirstOrDefault(x => x.EmailAddress == newsletterSubscription.EmailAddress);

            if(existingSubscription == null)
            {
                _eCommerceContext.NewsletterSubscriptions.Add(newsletterSubscription);
            }

            return _eCommerceContext.SaveChanges() > 0;
        }


        public List<NewsletterSubscription> SearchNewsletterSubscription(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            

            var newsletterSubscriptions = _eCommerceContext.NewsletterSubscriptions.Where(x => !x.IsDeleted).AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                newsletterSubscriptions = newsletterSubscriptions.Where(x => x.EmailAddress.Contains(searchTerm));
            }

            count = newsletterSubscriptions.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return newsletterSubscriptions.OrderBy(x => x.ID).Skip(skipCount).Take(recordSize).ToList();
        }
    }
}
