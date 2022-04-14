using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Models;
using AutoMapper;
using InfrastructureFMSDB;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace ApplicationFMSUnitTests.Arrange
{
    public class QueryTestBase : IDisposable
    {
        public FMSDataContext Context { get; private set; }
        public IMapper Mapper { get; private set; }
        public IOptions<JwtSetting> JwtSettingOptions;

        public QueryTestBase()
        {
            Context = FMSDBContextFactory.Create();
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            Mapper = configurationProvider.CreateMapper();
            JwtSettingOptions = Options.Create(new JwtSetting() { Secret = "PXgDHjN7DkRWP3ZiSFh0K9N0u2hLI6ZhP1nJZMkBbi6hwUmmsnkqUP8S1hcgYgrAfqmMgXdkvyL3VaWzFEL9qXFNGCB5gL9By7FRevf65U0b6PxIzUa3hlFVRPkukCgu" });
            //new JwtSetting 
        }

        public void Dispose()
        {
            FMSDBContextFactory.Destroy(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestBase> { }
    }
}
