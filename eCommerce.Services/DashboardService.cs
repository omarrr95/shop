using eCommerce.Data;

namespace eCommerce.Services
{
    public class DashboardService
    {
        #region Define as Singleton
        private readonly eCommerceContext _eCommerceContext;
        public DashboardService(eCommerceContext eCommerceContext)
        {
            _eCommerceContext = eCommerceContext;
        }
        #endregion

        public int GetUserCount()
        {
            

            return _eCommerceContext.Users.Count();
        }
        public int GetRolesCount()
        {
            

            return _eCommerceContext.Roles.Count();
        }
    }
}
