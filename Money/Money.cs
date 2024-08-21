using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace System
{
    /// <summary>
    ///     Represents a decimal amount of a specific <see cref="Currency" />.
    /// </summary>
    [Serializable]
    [DebuggerDisplay("{getDebugView()}")]
    public struct Money : IEquatable<Money>,
        IComparable<Money>,
        IFormattable,
        IConvertible,
        IComparable
    {
        /// <summary>
        ///     A zero value of money, regardless of currency.
        /// </summary>
        public static readonly Money Zero = new Money(0);

        /// <summary>
        ///     A source of randomness for stochastic rounding.
        /// </summary>
        [ThreadStatic] private static Random _rng;

        /// <summary>
        ///     The amount by which <see cref="_decimalFraction" /> has been scaled up to be a whole number.
        /// </summary>
        private const decimal FractionScale = 1E9M;

        /// <summary>
        ///     The <see cref="System.Currency" /> this amount represents money in.
        /// </summary>
        private readonly Currency? _currency;

        /// <summary>
        ///     The whole units of currency.
        /// </summary>
        private readonly long _units;

        /// <summary>
        ///     The fractional units of currency.
        /// </summary>
        private readonly int _decimalFraction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Money" /> struct equal to <paramref name="value" />.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        public Money(decimal value)
        {
            checkValue(value);

            _units = (long)value;
            _decimalFraction = (int)decimal.Round((value - _units) * FractionScale);

            if (_decimalFraction >= FractionScale)
            {
                _units += 1;
                _decimalFraction = _decimalFraction - (int)FractionScale;
            }

            _currency = Currency.FromCurrentCulture();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Money" /> struct equal to <paramref name="value" />
        ///     in <paramref name="currency" />.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <param name="currency">
        ///     The currency.
        /// </param>
        public Money(decimal value, Currency currency)
            : this(value)
        {
            _currency = currency;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Money" /> struct.
        /// </summary>
        /// <param name="units">
        ///     The units.
        /// </param>
        /// <param name="fraction">
        ///     The fraction.
        /// </param>
        /// <param name="currency">
        ///     The currency.
        /// </param>
        private Money(long units, int fraction, Currency currency)
        {
            _units = units;
            _decimalFraction = fraction;
            _currency = currency;
        }

        /// <summary>
        ///     Gets the <see cref="System.Currency" /> which this money value is specified in.
        /// </summary>
        public Currency Currency => _currency.GetValueOrDefault(Currency.FromCurrentCulture());

        /// <summary>
        ///     Implicitly converts a <see cref="Byte" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(byte value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="SByte" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        [CLSCompliant(false)]
        public static implicit operator Money(sbyte value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Single" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(float value)
        {
            return new Money((decimal)value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Double" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(double value)
        {
            return new Money((decimal)value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Decimal" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(decimal value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Money" /> value to <see cref="Decimal" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Decimal" /> value which this <see cref="Money" /> value is equivalent to.
        /// </returns>
        public static implicit operator decimal(Money value)
        {
            return value.computeValue();
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int16" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(short value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int32" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(int value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="Int64" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        public static implicit operator Money(long value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt16" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        [CLSCompliant(false)]
        public static implicit operator Money(ushort value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt32" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        [CLSCompliant(false)]
        public static implicit operator Money(uint value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     Implicitly converts a <see cref="UInt64" /> value to <see cref="Money" /> with no <see cref="Currency" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     A <see cref="Money" /> value with no <see cref="Currency" /> specified.
        /// </returns>
        [CLSCompliant(false)]
        public static implicit operator Money(ulong value)
        {
            return new Money(value);
        }

        /// <summary>
        ///     A negation operator for a <see cref="Money" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The additive inverse (negation) of the given <paramref name="value" />.
        /// </returns>
        public static Money operator -(Money value)
        {
            return new Money(-value._units, -value._decimalFraction, value.Currency);
        }

        /// <summary>
        ///     An identity operator for a <see cref="Money" /> value.
        /// </summary>
        /// <param name="value">
        ///     The value.
        /// </param>
        /// <returns>
        ///     The given <paramref name="value" />.
        /// </returns>
        public static Money operator +(Money value)
        {
            return value;
        }

        /// <summary>
        ///     An addition operator for two <see cref="Money" /> values.
        /// </summary>
        /// <param name="left">
        ///     The left operand.
        /// </param>
        /// <param name="right">
        ///     The right operand.
        /// </param>
        /// <returns>
        ///     The sum of <paramref name="left" /> and <paramref name="right" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the currencies of <paramref name="left" /> and <paramref name="right" /> are not equal.
        /// </exception>
        public static Money operator +(Money left, Money right)
        {
            if (left.Currency != right.Currency) throw differentCurrencies(left.Currency, right.Currency);

            var fractionSum = left._decimalFraction + right._decimalFraction;

            var overflow = 0L;
            var fractionSign = Math.Sign(fractionSum);
            var absFractionSum = Math.Abs(fractionSum);

            if (absFractionSum >= FractionScale)
            {
                overflow = fractionSign;
                absFractionSum -= (int)FractionScale;
                fractionSum = fractionSign * absFractionSum;
            }

            var newUnits = left._units + right._units + overflow;

            if (fractionSign < 0 && Math.Sign(newUnits) > 0)
            {
                newUnits -= 1;
                fractionSum = (int)FractionScale - absFractionSum;
            }

            return new Money(newUnits,
                fractionSum,
                left.Currency);
        }


        /// <summary>
        ///     A subtraction operator for two <see cref="Money" /> values.
        /// </summary>
        /// <param name="left">
        ///     The left operand.
        /// </param>
        /// <param name="right">
        ///     The right operand.
        /// </param>
        /// <returns>
        ///     The difference of <paramref name="left" /> and <paramref name="right" />.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Thrown if the currencies of <paramref name="left" /> and <paramref name="right" /> are not equal.
        /// </exception>
        public static Money operator -(Money left, Money right)
        {
            if (left.Currency != right.Currency) throw differentCurrencies(left.Currency, right.Currency);

            return left + -right;
        }

        /// <summary>
        ///     A product operator for a <see cref="Money" /> value and a <see cref="Decimal" /> value.
        /// </summary>
        /// <param name="left">
        ///     The left operand.
        /// </param>
        /// <param name="right">
        ///     The right operand.
        /// </param>
        /// <returns>
        ///     The product of <paramref name="left" /> and <paramref name="right" />.
        /// </returns>
        public static Money operator *(Money left, decimal right)
        {
            return (decimal)left * right;
        }

        public static Money operator /(Money left, decimal right)
        {
            return (decimal)left / right;
        }

        public static bool operator ==(Money left, Money right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Money left, Money right)
        {
            return !left.Equals(right);
        }

        public static bool operator >(Money left, Money right)
        {
            return left.CompareTo(right) > 0;
        }

        public static bool operator <(Money left, Money right)
        {
            return left.CompareTo(right) < 0;
        }

        public static bool operator >=(Money left, Money right)
        {
            return left.CompareTo(right) >= 0;
        }

        public static bool operator <=(Money left, Money right)
        {
            return left.CompareTo(right) <= 0;
        }

        public static bool TryParse(string s, out Money money)
        {
            money = Zero;

            if (s == null) return false;

            s = s.Trim();

            if (s == string.Empty) return false;

            Currency? currency = null;
            Currency currencyValue;

            if (s.Length > 1 && s.Contains(" "))
            {
                var strings = s.Split(' ');
                var currencyString = strings[0].Trim();
                var valueString = strings[1].Trim();
                if (Currency.TryParse(currencyString, out currencyValue))
                {
                    s = valueString;
                    currency = currencyValue;
                }
            }
            else
            {
                // Regex pattern to capture the currency symbol or ISO code and the amount
                // Here’s what this pattern does:
                // ^ asserts the start of the line.
                // ([^\d]+) is a capturing group that matches one or more characters that are not digits.
                // (\d+(?:\.\d+)?) is a capturing group that matches one or more digits, optionally followed by a period . and one or more digits (the decimal part).
                // $ asserts the end of the line.
                // The ?: inside the second group makes it a non-capturing group, meaning it won’t be captured for back-referencing. This is useful if you only want to capture the whole number part as a single group.
                string pattern = @"^([^\d]+)(\d+(?:\.\d+)?)$";
                var match = Regex.Match(s, pattern);

                if (match.Success)
                {
                    var currencyString =match.Groups[1].Value.Trim();
                    var valueString = match.Groups[2].Value.Trim();
                    if (Currency.TryParse(currencyString, out currencyValue))
                    {
                        s = valueString;
                        currency = currencyValue;
                    }
                }
                else
                {
                    // Check for currency symbol (e.g. $, £)
                    if (!Currency.TryParse(s.Substring(0, 1), out currencyValue))
                    {
                        // Check for currency ISO code (e.g. USD, GBP)
                        if (s.Length > 2 && Currency.TryParse(s.Substring(0, 3), out currencyValue))
                        {
                            s = s.Substring(3);
                            currency = currencyValue;
                        }
                    }
                    else
                    {
                        s = s.Substring(1);
                        currency = currencyValue;
                    }    
                }
            }

            decimal value;

            if (!decimal.TryParse(s, out value)) return false;

            money = currency != null ? new Money(value, currency.Value) : new Money(value);

            return true;
        }

        public Money Round(RoundingPlaces places, MidpointRoundingRule rounding = MidpointRoundingRule.ToEven)
        {
            Money remainder;
            return Round(places, rounding, out remainder);
        }

        public Money Round(RoundingPlaces places, MidpointRoundingRule rounding, out Money remainder)
        {
            long unit;

            var placesExponent = getExponentFromPlaces(places);
            var fraction = roundFraction(placesExponent, rounding, out unit);
            var units = _units + unit;
            remainder = new Money(0, _decimalFraction - fraction, Currency);

            return new Money(units, fraction, Currency);
        }

        private int roundFraction(int exponent, MidpointRoundingRule rounding, out long unit)
        {
            var denominator = FractionScale / (decimal)Math.Pow(10, exponent);
            var fraction = _decimalFraction / denominator;

            switch (rounding)
            {
                case MidpointRoundingRule.ToEven:
                    fraction = Math.Round(fraction, MidpointRounding.ToEven);
                    break;
                case MidpointRoundingRule.AwayFromZero:
                {
                    var sign = Math.Sign(fraction);
                    fraction = Math.Abs(fraction); // make positive
                    fraction = Math.Floor(fraction + 0.5M); // round UP
                    fraction *= sign; // reapply sign
                    break;
                }
                case MidpointRoundingRule.TowardZero:
                {
                    var sign = Math.Sign(fraction);
                    fraction = Math.Abs(fraction); // make positive
                    fraction = Math.Floor(fraction + 0.5M); // round DOWN
                    fraction *= sign; // reapply sign
                    break;
                }
                case MidpointRoundingRule.Up:
                    fraction = Math.Floor(fraction + 0.5M);
                    break;
                case MidpointRoundingRule.Down:
                    fraction = Math.Ceiling(fraction - 0.5M);
                    break;
                case MidpointRoundingRule.Stochastic:
                    if (_rng == null) _rng = new MersenneTwister();

                    var coinFlip = _rng.NextDouble();

                    if (coinFlip >= 0.5) goto case MidpointRoundingRule.Up;

                    goto case MidpointRoundingRule.Down;
                default:
                    throw new ArgumentOutOfRangeException("rounding");
            }

            fraction *= denominator;

            if (fraction >= FractionScale)
            {
                unit = 1;
                fraction = fraction - (int)FractionScale;
            }
            else
            {
                unit = 0;
            }

            return (int)fraction;
        }

        private static int getExponentFromPlaces(RoundingPlaces places)
        {
            switch (places)
            {
                case RoundingPlaces.Zero:
                    return 0;
                case RoundingPlaces.One:
                    return 1;
                case RoundingPlaces.Two:
                    return 2;
                case RoundingPlaces.Three:
                    return 3;
                case RoundingPlaces.Four:
                    return 4;
                case RoundingPlaces.Five:
                    return 5;
                case RoundingPlaces.Six:
                    return 6;
                case RoundingPlaces.Seven:
                    return 7;
                case RoundingPlaces.Eight:
                    return 8;
                case RoundingPlaces.Nine:
                    return 9;
                default:
                    throw new ArgumentOutOfRangeException("places");
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (397 * _units.GetHashCode()) ^ _currency.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Money)) return false;

            var other = (Money)obj;
            return Equals(other);
        }

        public override string ToString()
        {
            return computeValue().ToString("C", (IFormatProvider)_currency ?? NumberFormatInfo.CurrentInfo);
        }

        public string ToString(string format)
        {
            return computeValue().ToString(format, (IFormatProvider)_currency ?? NumberFormatInfo.CurrentInfo);
        }

        #region Implementation of IEquatable<Money>

        public bool Equals(Money other)
        {
            checkCurrencies(other);

            return _units == other._units &&
                   _decimalFraction == other._decimalFraction;
        }

        #endregion

        #region Implementation of IComparable<Money>

        public int CompareTo(Money other)
        {
            checkCurrencies(other);

            var unitCompare = _units.CompareTo(other._units);

            return unitCompare == 0
                ? _decimalFraction.CompareTo(other._decimalFraction)
                : unitCompare;
        }

        #endregion

        #region Implementation of IFormattable

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return computeValue().ToString(format, formatProvider);
        }

        #endregion

        #region Implementation of IComparable

        int IComparable.CompareTo(object obj)
        {
            if (obj is Money) return CompareTo((Money)obj);

            throw new InvalidOperationException("Object is not a " + GetType() + " instance.");
        }

        #endregion

        #region Implementation of IConvertible

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        public bool ToBoolean(IFormatProvider provider)
        {
            return _units == 0 && _decimalFraction == 0;
        }

        public char ToChar(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        [CLSCompliant(false)]
        public sbyte ToSByte(IFormatProvider provider)
        {
            return (sbyte)computeValue();
        }

        public byte ToByte(IFormatProvider provider)
        {
            return (byte)computeValue();
        }

        public short ToInt16(IFormatProvider provider)
        {
            return (short)computeValue();
        }

        [CLSCompliant(false)]
        public ushort ToUInt16(IFormatProvider provider)
        {
            return (ushort)computeValue();
        }

        public int ToInt32(IFormatProvider provider)
        {
            return (int)computeValue();
        }

        [CLSCompliant(false)]
        public uint ToUInt32(IFormatProvider provider)
        {
            return (uint)computeValue();
        }

        public long ToInt64(IFormatProvider provider)
        {
            return (long)computeValue();
        }

        [CLSCompliant(false)]
        public ulong ToUInt64(IFormatProvider provider)
        {
            return (ulong)computeValue();
        }

        public float ToSingle(IFormatProvider provider)
        {
            return (float)computeValue();
        }

        public double ToDouble(IFormatProvider provider)
        {
            return (double)computeValue();
        }

        public decimal ToDecimal(IFormatProvider provider)
        {
            return computeValue();
        }

        public DateTime ToDateTime(IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        public string ToString(IFormatProvider provider)
        {
            return ((decimal)this).ToString(provider);
        }

        public object ToType(Type conversionType, IFormatProvider provider)
        {
            throw new NotSupportedException();
        }

        #endregion

        private decimal computeValue()
        {
            return _units + _decimalFraction / FractionScale;
        }

        private static InvalidOperationException differentCurrencies(Currency leftCurrency, Currency rightCurrency)
        {
            return new InvalidOperationException("Money values are in different " +
                                                 "currencies. " +
                                                 leftCurrency +
                                                 " != " +
                                                 rightCurrency +
                                                 " Convert to the same " +
                                                 "currency before performing " +
                                                 "operations on the values.");
        }

        private static void checkValue(decimal value)
        {
            if (value < long.MinValue || value > long.MaxValue)
                throw new ArgumentOutOfRangeException("value",
                    value,
                    "Money value must be between " +
                    long.MinValue + " and " +
                    long.MaxValue);
        }

        private void checkCurrencies(Money other)
        {
            if (other.Currency != Currency) throw differentCurrencies(Currency, other.Currency);
        }

        private string getDebugView()
        {
            return ToString() +
                   string.Format(" ({0} {1})",
                       ToDecimal(CultureInfo.CurrentCulture),
                       Currency == Currency.None ? "<Unspecified Currency>" : Currency.ToString());
        }
    }
}