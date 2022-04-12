using InfrastructureFMSDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMSUnitTests.Arrange
{
    public class TestBase : IDisposable
    {
        protected readonly FMSDataContext _context;

        public TestBase()
        {
            _context = FMSDBContextFactory.Create();
        }

        public void Dispose()
        {
            FMSDBContextFactory.Destroy(_context);
        }
    }
}
