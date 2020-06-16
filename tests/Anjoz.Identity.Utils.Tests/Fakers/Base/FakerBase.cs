using Bogus;

namespace Anjoz.Identity.Utils.Tests.Fakers.Base
{
    public class FakerBase<TFaker> : Faker<TFaker>
        where TFaker : class, new()
    {
        private const string DefaultLocale = "pt_BR";

        protected FakerBase()
        {
            Locale = DefaultLocale;
        }
    }
}