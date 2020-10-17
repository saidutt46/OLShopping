using DCommerce.Repository.Shared;
using Microsoft.EntityFrameworkCore;

namespace DCommerce.Repository.DatabaseContext
{
    public class DCommerceContextFactory : DesignTimeDbContextFactoryBase<DCommerceContext>
    {
        protected override DCommerceContext CreateNewInstance(DbContextOptions<DCommerceContext> options)
        {
            return new DCommerceContext(options);
        }
    }
}
