using AutoMapper;
using Xunit;
using Profile = Anjoz.Identity.Application.AutoMapper.Profiles.Base.Profile;

namespace Anjoz.Identity.Unit.Tests.AutoMapper.Tests
{
    public class AutoMapperUnitTest
    {
        [Fact]
        public void Shoul_Validate_All_Mappings()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(typeof(Profile).Assembly));
            
            configuration.AssertConfigurationIsValid();
        }
    }
}