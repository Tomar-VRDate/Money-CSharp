using System.Collections.Generic;
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

        [Fact]
        public void CurrencyTryParseByIsoCurrencySymbolsIsCorrect()
        {
            foreach (var expectedCurrency in Currency.CurrencyByIsoCurrencySymbol.Values)
            {
                AssertTryParse($"{expectedCurrency.IsoCurrencySymbol}", expectedCurrency);
            }
        }

        [Fact]
        public void CurrencyTryParseBySomeCurrencySymbolIsCorrect()
        {
            AssertTryParse(Currency.USD.CurrencySymbol, Currency.USD);

            AssertTryParse(Currency.EUR.CurrencySymbol, Currency.EUR);

            AssertTryParse(Currency.JPY.CurrencySymbol, Currency.JPY);

            AssertTryParse(Currency.GBP.CurrencySymbol, Currency.GBP);

            AssertTryParse(Currency.CHF.CurrencySymbol, Currency.CHF);

            AssertTryParse(Currency.ZAR.CurrencySymbol, Currency.ZAR);
            
            AssertTryParse(Currency.KWD.CurrencySymbol, Currency.KWD);

            AssertTryParse(Currency.ILS.CurrencySymbol, Currency.ILS);
        }

        [Fact]
        public void CurrencyTryParseByCurrencySymbolIsCorrect()
        {
            int withoutCurrencySymbol = 0;
            List<Exception> exceptions = new List<Exception>();
            foreach (var expectedCurrency in Currency.CurrencyByIsoCurrencySymbol.Values)
            {
                try
                {
                    if (expectedCurrency.CurrencySymbol == null)
                    {
                        withoutCurrencySymbol++;
                        continue;
                    }

                    AssertTryParse($"{expectedCurrency.CurrencySymbol}", expectedCurrency);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            var exceptionsCount = exceptions.Count;
            var currencyCount = Currency.CurrencyByIsoCurrencySymbol.Values.Count;
            var present = 100 - (float)exceptionsCount / currencyCount * 100;
            Console.WriteLine(
                $"{exceptionsCount}/{currencyCount} {present}% parsed {withoutCurrencySymbol} without CurrencySymbols {exceptionsCount} CurrencySymbols are shared with other IsoCurrencySymbols");
            Assert.True(currencyCount > exceptionsCount);
            Assert.Equal(25, exceptionsCount);
            foreach (var exception in exceptions)
            {
                Console.WriteLine(exception);
            }
        }

        private static void AssertTryParse(string @string, Currency expected)
        {
            bool result;
            Currency actual;
            result = Currency.TryParse(@string, out actual);
            Assert.True(result, @string);
            Assert.Equal(expected, actual);
        }
    }
}