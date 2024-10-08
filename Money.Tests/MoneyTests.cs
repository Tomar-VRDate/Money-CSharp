﻿using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Xunit;

namespace System.Tests
{
    public class MoneyTests
    {
        [Fact]
        public void MoneyHasValueEquality()
        {
            var money1 = new Money(101.5M);
            var money2 = new Money(101.5M);

            Assert.Equal(money1, money2);
            Assert.NotSame(money1, money2);
        }

        [Fact]
        public void MoneyImplicitlyConvertsFromPrimitiveNumbers()
        {
            Money money;

            byte byteValue = 50;
            money = byteValue;
            Assert.Equal(new Money(50), money);

            sbyte sByteValue = 75;
            money = sByteValue;
            Assert.Equal(new Money(75), money);

            short int16Value = 100;
            money = int16Value;
            Assert.Equal(new Money(100), money);

            var int32Value = 200;
            money = int32Value;
            Assert.Equal(new Money(200), money);

            long int64Value = 300;
            money = int64Value;
            Assert.Equal(new Money(300), money);

            ushort uInt16Value = 400;
            money = uInt16Value;
            Assert.Equal(new Money(400), money);

            uint uInt32Value = 500;
            money = uInt32Value;
            Assert.Equal(new Money(500), money);

            ulong uInt64Value = 600;
            money = uInt64Value;
            Assert.Equal(new Money(600), money);

            float singleValue = 700;
            money = singleValue;
            Assert.Equal(new Money(700), money);

            double doubleValue = 800;
            money = doubleValue;
            Assert.Equal(new Money(800), money);

            decimal decimalValue = 900;
            money = decimalValue;
            Assert.Equal(new Money(900), money);
        }

        [Fact]
        public void MoneyWholeAmountAdditionIsCorrect()
        {
            // whole number
            Money money1 = 101M;
            Money money2 = 99M;

            Assert.Equal(new Money(200), money1 + money2);
        }

        [Fact]
        public void MoneyFractionalAmountAdditionIsCorrect()
        {
            // fractions
            Money money1 = 100.00M;
            Money money2 = 0.01M;

            Assert.Equal(new Money(100.01M), money1 + money2);
        }

        [Fact]
        public void MoneyFractionalAmountWithOverflowAdditionIsCorrect()
        {
            // overflow
            Money money1 = 100.999M;
            Money money2 = 0.9M;

            Assert.Equal(new Money(101.899M), money1 + money2);
        }

        [Fact]
        public void MoneyNegativeAmountAdditionIsCorrect()
        {
            // negative
            Money money1 = 100.999M;
            Money money2 = -0.9M;

            Assert.Equal(new Money(100.099M), money1 + money2);
        }

        [Fact]
        public void MoneyNegativeAmountWithOverflowAdditionIsCorrect()
        {
            // negative overflow
            Money money1 = -100.999M;
            Money money2 = -0.9M;

            Assert.Equal(new Money(-101.899M), money1 + money2);
        }

        [Fact]
        public void MoneyWholeAmountSubtractionIsCorrect()
        {
            // whole number
            Money money1 = 101M;
            Money money2 = 99M;

            Assert.Equal(new Money(2), money1 - money2);
        }

        [Fact]
        public void MoneyFractionalAmountSubtractionIsCorrect()
        {
            // fractions
            Money money1 = 100.00M;
            Money money2 = 0.01M;

            Assert.Equal(new Money(99.99M), money1 - money2);
        }

        [Fact]
        public void MoneyFractionalAmountWithOverflowSubtractionIsCorrect()
        {
            // overflow
            Money money1 = 100.5M;
            Money money2 = 0.9M;

            Assert.Equal(new Money(99.6M), money1 - money2);
        }

        [Fact]
        public void MoneyNegativeAmountSubtractionIsCorrect()
        {
            // negative
            Money money1 = 100.999M;
            Money money2 = -0.9M;

            Assert.Equal(new Money(101.899M), money1 - money2);
        }

        [Fact]
        public void MoneyNegativeAmountWithOverflowSubtractionIsCorrect()
        {
            // negative overflow
            Money money1 = -100.999M;
            Money money2 = -0.9M;

            Assert.Equal(new Money(-100.099M), money1 - money2);
        }

        [Fact]
        public void MoneyScalarWholeMultiplicationIsCorrect()
        {
            Money money = 100.125;

            Assert.Equal(new Money(500.625M), money * 5);
        }

        [Fact]
        public void MoneyScalarFractionalMultiplicationIsCorrect()
        {
            Money money = 100.125;

            Assert.Equal(new Money(50.0625M), money * 0.5M);
        }

        [Fact]
        public void MoneyScalarNegativeWholeMultiplicationIsCorrect()
        {
            Money money = -100.125;

            Assert.Equal(new Money(-500.625M), money * 5);
        }

        [Fact]
        public void MoneyScalarNegativeFractionalMultiplicationIsCorrect()
        {
            Money money = -100.125;

            Assert.Equal(new Money(-50.0625M), money * 0.5M);
        }

        [Fact]
        public void MoneyScalarWholeDivisionIsCorrect()
        {
            Money money = 100.125;

            Assert.Equal(new Money(50.0625M), money / 2);
        }

        [Fact]
        public void MoneyScalarFractionalDivisionIsCorrect()
        {
            Money money = 100.125;

            Assert.Equal(new Money(200.25M), money / 0.5M);
        }

        [Fact]
        public void MoneyScalarNegativeWholeDivisionIsCorrect()
        {
            Money money = -100.125;

            Assert.Equal(new Money(-50.0625M), money / 2);
        }

        [Fact]
        public void MoneyScalarNegativeFractionalDivisionIsCorrect()
        {
            Money money = -100.125;

            Assert.Equal(new Money(-200.25M), money / 0.5M);
        }

        [Fact]
        public void MoneyEqualOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.True(money1 == money2);

            money2 = 101.125;
            Assert.False(money1 == money2);

            money2 = 100.25;
            Assert.False(money1 == money2);
        }

        [Fact]
        public void MoneyNotEqualOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.False(money1 != money2);

            money2 = 101.125;
            Assert.True(money1 != money2);

            money2 = 100.25;
            Assert.True(money1 != money2);
        }

        [Fact]
        public void MoneyGreaterThanEqualOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.True(money1 >= money2);

            money2 = 101.125;
            Assert.True(money2 >= money1);
            Assert.False(money1 >= money2);

            money2 = 100.25;
            Assert.True(money2 >= money1);
            Assert.False(money1 >= money2);
        }

        [Fact]
        public void MoneyLessThanEqualOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.True(money1 <= money2);

            money2 = 101.125;
            Assert.False(money2 <= money1);
            Assert.True(money1 <= money2);

            money2 = 100.25;
            Assert.False(money2 <= money1);
            Assert.True(money1 <= money2);
        }

        [Fact]
        public void MoneyGreaterThanOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.False(money1 > money2);

            money2 = 101.125;
            Assert.True(money2 > money1);
            Assert.False(money1 > money2);

            money2 = 100.25;
            Assert.True(money2 > money1);
            Assert.False(money1 > money2);
        }

        [Fact]
        public void MoneyLessThanOperatorIsCorrect()
        {
            Money money1 = 100.125;
            Money money2 = 100.125;

            Assert.False(money1 < money2);

            money2 = 101.125;
            Assert.False(money2 < money1);
            Assert.True(money1 < money2);

            money2 = 100.25;
            Assert.False(money2 < money1);
            Assert.True(money1 < money2);
        }

        [Fact]
        public void MoneyPrintsCorrectly()
        {
            var previousCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            var money = new Money(100.125M, Currency.USD);
            var formattedMoney = money.ToString();
            Assert.Equal("$100.13", formattedMoney);
            Thread.CurrentThread.CurrentCulture = previousCulture;
        }

        [Fact]
        public void MoneyOperationsInvolvingDifferentCurrencyAllFail()
        {
            var money1 = new Money(101.5M, Currency.AUD);
            var money2 = new Money(98.5M, Currency.CAD);
            Money m;
            bool b;

            Assert.Throws<InvalidOperationException>(() => { m = money1 + money2; });
            Assert.Throws<InvalidOperationException>(() => { m = money1 - money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 == money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 != money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 > money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 < money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 >= money2; });
            Assert.Throws<InvalidOperationException>(() => { b = money1 <= money2; });
        }

        [Fact]
        public void MoneyTryParseByIsoCurrencySymbolsIsCorrect()
        {
            foreach (var expectedCurrency in Currency.CurrencyByIsoCurrencySymbol.Values)
            {
                AssertTryParse($"{expectedCurrency.IsoCurrencySymbol} 123.45", new Money(123.45M, expectedCurrency));
            }
        }

        [Fact]
        public void MoneyTryParseByIsoCurrencySymbolsWithoutSpaceIsCorrect()
        {
            foreach (var expectedCurrency in Currency.CurrencyByIsoCurrencySymbol.Values)
            {
                AssertTryParse($"{expectedCurrency.IsoCurrencySymbol}123.45", new Money(123.45M, expectedCurrency));
            }
        }

        [Fact]
        public void MoneyTryParseBySomeCurrencySymbolIsCorrect()
        {
            AssertTryParse($"{Currency.USD.CurrencySymbol} 123.45", new Money(123.45M, Currency.USD));

            AssertTryParse($"{Currency.EUR.CurrencySymbol} 123.45", new Money(123.45M, Currency.EUR));

            AssertTryParse($"{Currency.JPY.CurrencySymbol} 123.45", new Money(123.45M, Currency.JPY));

            AssertTryParse($"{Currency.GBP.CurrencySymbol} 123.45", new Money(123.45M, Currency.GBP));

            AssertTryParse($"{Currency.CHF.CurrencySymbol} 123.45", new Money(123.45M, Currency.CHF));

            AssertTryParse($"{Currency.ZAR.CurrencySymbol} 123.45", new Money(123.45M, Currency.ZAR));

            AssertTryParse($"{Currency.KWD.CurrencySymbol} 123.45", new Money(123.45M, Currency.KWD));

            AssertTryParse($"{Currency.ILS.CurrencySymbol} 123.45", new Money(123.45M, Currency.ILS));
        }


        [Fact]
        public void MoneyTryParseByCurrencySymbolIsCorrect()
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

                    AssertTryParse($"{expectedCurrency.CurrencySymbol} 123.45",
                        new Money(123.45M, expectedCurrency));
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

        [Fact]
        public void MoneyTryParseByCurrencySymbolWithoutSpaceIsCorrect()
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

                    AssertTryParse($"{expectedCurrency.CurrencySymbol}123.45",
                        new Money(123.45M, expectedCurrency));
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

        private static void AssertTryParse(string @string, Money expected)
        {
            bool result;
            Money actual;
            result = Money.TryParse(@string, out actual);
            Assert.True(result, @string);
            Assert.Equal(expected, actual);
        }
    }
}