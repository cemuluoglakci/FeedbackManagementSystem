using InfrastructureFMSDB;
using System;

namespace ApplicationFMSUnitTests.Arrange
{
    public class CommandTestBase : IDisposable
    {
        protected readonly FMSDataContext _context;

        public CommandTestBase()
        {
            _context = FMSDBContextFactory.Create();
        }

        public void Dispose()
        {
            FMSDBContextFactory.Destroy(_context);
        }
    }
}
