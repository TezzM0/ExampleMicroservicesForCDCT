using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace CdcTestsShared
{
    public static class AutoFixtureProvider
    {
        public static Fixture CreateAutoFixture()
        {
            var fixture = new Fixture();
            fixture.Customize(new AutoNSubstituteCustomization());
            return fixture;
        }
    }
}
