using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class PromosService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public PromosService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public List<Promo> SearchPromos(string searchTerm, int? pageNo, int recordSize, out int count)
        {
            

            var promos = _eCommerceContext.Promos
                                .Where(x => !x.IsDeleted)
                                .AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                promos = promos.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            count = promos.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return promos.OrderByDescending(x => x.Name).Skip(skipCount).Take(recordSize).ToList();
        }

        public Promo GetPromoByID(int ID)
        {
            

            return _eCommerceContext.Promos.FirstOrDefault(x=> !x.IsDeleted && x.ID == ID);
        }
        public Promo GetPromoByCode(string code)
        {
            

            return _eCommerceContext.Promos.FirstOrDefault(x => !x.IsDeleted && x.Code == code);
        }

        public bool SavePromo(Promo Promo)
        {
            

            _eCommerceContext.Promos.Add(Promo);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdatePromo(Promo promo)
        {
            

            _eCommerceContext.Entry(promo).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }
        
        public bool DeletePromo(int ID)
        {
            

            var promos = _eCommerceContext.Promos.Find(ID);

            promos.IsDeleted = true;

            _eCommerceContext.Entry(promos).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }
    }
}
