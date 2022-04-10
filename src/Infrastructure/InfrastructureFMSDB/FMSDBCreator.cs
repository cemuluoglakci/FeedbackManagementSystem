using Microsoft.EntityFrameworkCore;

namespace InfrastructureFMSDB
{
    class FMSDBCreator : DesignTimeDBPattern<FMSDataContext>
    {
        protected override FMSDataContext CreateNewInstance(DbContextOptions<FMSDataContext> options)
        {
            return new FMSDataContext(options);
        }
    }

}
