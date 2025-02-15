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
    public class LanguagesService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public LanguagesService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public List<Language> GetLanguages(bool enabledLanguagesOnly = false, bool resourceLanguagesOnly = true, int? pageNo = 1, int? recordSize = 0)
        {
            

            var languages = _eCommerceContext.Languages
                                    .Where(x => !x.IsDeleted)
                                    .AsQueryable();

            if(enabledLanguagesOnly)
            {
                languages = languages.Where(x => x.IsEnabled);
            }

            if(resourceLanguagesOnly)
            {
                var languageIDsWithResources = _eCommerceContext.LanguageResources.Select(x => x.LanguageID).Distinct();

                languages = languages.Where(x=>languageIDsWithResources.Contains(x.ID));
            }

            languages = languages.OrderBy(x => x.Name);

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                languages = languages.Skip(skip)
                                       .Take(recordSize.Value);
            }

            return languages.ToList();
        }

        public List<Language> SearchLanguages(string searchTerm, out int count, bool enabledLanguagesOnly = false, int? pageNo = 1, int? recordSize = 0)
        {
            

            var languages = _eCommerceContext.Languages
                                    .Where(x => !x.IsDeleted)
                                    .AsQueryable();

            if(!string.IsNullOrEmpty(searchTerm))
            {
                languages = languages.Where(x => x.Name.Contains(searchTerm));
            }

            if (enabledLanguagesOnly)
            {
                languages = languages.Where(x => x.IsEnabled);
            }

            count = languages.Count();

            languages = languages.OrderBy(x => x.ID);

            if (recordSize.HasValue && recordSize.Value > 0)
            {
                pageNo = pageNo ?? 1;
                var skip = (pageNo.Value - 1) * recordSize.Value;

                languages = languages.Skip(skip)
                                       .Take(recordSize.Value);
            }

            return languages.ToList();
        }

        public Language GetDefaultLanguage()
        {
            

            return _eCommerceContext.Languages.FirstOrDefault(x => x.IsDefault && !x.IsDeleted);
        }

        public Language GetLanguageByID(int ID)
        {
            

            return _eCommerceContext.Languages.FirstOrDefault(x => x.ID == ID && !x.IsDeleted);
        }

        public Language GetLanguageByShortCode(string shortCode)
        {
            

            return _eCommerceContext.Languages.FirstOrDefault(x => x.ShortCode == shortCode && !x.IsDeleted);
        }

        public bool AddLanguage(Language language, bool makeDefault = false)
        {
            

            if (makeDefault)
            {
                var existingDefaultLanguages = _eCommerceContext.Languages.Where(x => x.IsDefault).ToList();

                existingDefaultLanguages.ForEach(x => x.IsDefault = false);
            }

            _eCommerceContext.Languages.Add(language);

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateLanguage(Language language, bool makeDefault = false)
        {
            

            if (makeDefault)
            {
                var existingDefaultLanguages = _eCommerceContext.Languages.Where(x => x.IsDefault).ToList();

                existingDefaultLanguages.ForEach(x => x.IsDefault = false);
            }

            _eCommerceContext.Entry(language).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool LanguageHasResources(Language language)
        {
            

            var languageResource = _eCommerceContext.LanguageResources.FirstOrDefault(x => x.LanguageID == language.ID);

            return languageResource != null;
        }

        public List<int> LanguagesWithResources()
        {
            

            var languageIDs = _eCommerceContext.LanguageResources.Select(x => x.LanguageID).Distinct().ToList();

            return languageIDs;
        }

        public List<LanguageResource> GetAllLanguageResources()
        {
            

            return _eCommerceContext.LanguageResources.ToList();
        }

        public List<LanguageResource> GetLanguageResources(int languageID)
        {
            

            return _eCommerceContext.LanguageResources.Where(x => x.LanguageID == languageID).ToList();
        }

        public List<LanguageResource> GetLanguagesResources(List<int> languageIDs)
        {
            

            return _eCommerceContext.LanguageResources.Where(x => languageIDs.Contains(x.LanguageID)).ToList();
        }

        public bool ImportLanguageResources(int languageID, List<LanguageResource> resources)
        {
            

            var existingResources = _eCommerceContext.LanguageResources.Where(x=>x.LanguageID == languageID);

            if(existingResources.Count() > 0)
            {
                _eCommerceContext.LanguageResources.RemoveRange(existingResources);
            }

            _eCommerceContext.LanguageResources.AddRange(resources);

            return _eCommerceContext.SaveChanges() > 0;
        }
    }
}
