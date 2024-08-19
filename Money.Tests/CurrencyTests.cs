using System.Globalization;
using Xunit;

namespace System.Tests
{
    public class CurrencyTests
    {
        [Fact]
        public void CurrencyFromCurrentCultureEqualsCurrentCultureCurrency()
        {
            // NOTE: I think this test could fail in certain cultures...
            var currency1 = new Currency(new RegionInfo(CultureInfo.CurrentCulture.LCID).ISOCurrencySymbol);
            var currency2 = Currency.FromCurrentCulture();

            Assert.Equal(currency1.CurrencyName, currency2.CurrencyName);
            Assert.Equal(currency1.CurrencySymbol, currency2.CurrencySymbol);
            Assert.Equal(currency1.IsoCurrencySymbol, currency2.IsoCurrencySymbol);
            Assert.Equal(currency1.IsoCurrencyCode, currency2.IsoCurrencyCode);
        }

        [Fact]
        public void CurrencyFromSpecificCultureInfoIsCorrect()
        {
            var currency = Currency.FromCultureInfo(new CultureInfo(1052));

            Assert.Equal(8, currency.IsoCurrencyCode);
        }

        [Fact]
        public void CurrencyFromSpecificIsoCodeIsCorrect()
        {
            var currency = Currency.FromIso3LetterCode("EUR");

            Assert.Equal(978, currency.IsoCurrencyCode);
        }

        [Fact]
        public void CurrencyHasValueEquality()
        {
            var currency1 = new Currency("USD");
            var currency2 = new Currency("USD");
            object boxedCurrency2 = currency2;

            Assert.True(currency1.Equals(currency2));
            Assert.True(currency1.Equals(boxedCurrency2));
        }
    }
}