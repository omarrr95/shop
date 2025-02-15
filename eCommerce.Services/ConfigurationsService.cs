using eCommerce.Data;
using eCommerce.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Services
{
    public class ConfigurationsService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public ConfigurationsService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public List<Configuration> GetAllConfigurations()
        {
            

            return _eCommerceContext.Configurations.ToList();
        }

        public List<Configuration> GetConfigurationsByType(int configurationType)
        {
            

            return _eCommerceContext.Configurations.Where(x => x.ConfigurationType == configurationType).ToList();
        }

        public Configuration GetConfigurationByKey(string key)
        {
            

            return _eCommerceContext.Configurations.FirstOrDefault(x => x.Key == key);
        }

        public bool UpdateConfiguration(Configuration configuration)
        {
            

            _eCommerceContext.Entry(configuration).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }

        public bool UpdateConfigurationValue(string key, string value)
        {
            

            var configuration = _eCommerceContext.Configurations.Find(key);

            configuration.Value = value;

            _eCommerceContext.Entry(configuration).State = EntityState.Modified;

            return _eCommerceContext.SaveChanges() > 0;
        }

        public List<Configuration> SearchConfigurations(int? configurationType, string searchTerm, int? pageNo, int recordSize, out int count)
        {
            

            var configurations = _eCommerceContext.Configurations.AsQueryable();

            if (configurationType.HasValue && configurationType.Value > 0)
            {
                configurations = configurations.Where(x => x.ConfigurationType == configurationType.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                configurations = configurations.Where(x => x.Key.ToLower().Contains(searchTerm.ToLower()));
            }

            count = configurations.Count();

            pageNo = pageNo ?? 1;
            var skipCount = (pageNo.Value - 1) * recordSize;

            return configurations.OrderBy(x => x.Key).Skip(skipCount).Take(recordSize).ToList();
        }
    }
}
