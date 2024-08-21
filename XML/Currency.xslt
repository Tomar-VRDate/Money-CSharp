<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="text" indent="no"/>
    <!-- Template for Static Currency-->
    <xsl:template name="static-Currency">
        <xsl:param name="currencyName"/>
        <xsl:param name="isoCurrencyCode"/>
        <xsl:param name="isoCurrencySymbol"/>
        <xsl:param name="isFund"/>
        <xsl:if test="not($currencyName = 'No universal currency')">
            <xsl:text disable-output-escaping="yes">
            /// &lt;summary&gt;</xsl:text>
            ///     Currency # <xsl:value-of select="$isoCurrencySymbol"/><xsl:text disable-output-escaping="yes"> - </xsl:text><xsl:value-of select="$currencyName" disable-output-escaping="yes"/> <xsl:if test="$isFund = 'true'"> isFund = <xsl:value-of select="$isFund"/></xsl:if>
            <xsl:text disable-output-escaping="yes">
            /// &lt;/summary&gt;</xsl:text>
            public static readonly Currency <xsl:value-of select="$isoCurrencyCode"/>;
         </xsl:if>
    </xsl:template> 
    <!-- Template for Static CurrencyByIsoCurrencySymbol Dictionary-->
    <xsl:template name="CurrencyByIsoCurrencySymbol-Dictionary">
        <xsl:param name="currencyName"/>
        <xsl:param name="isoCurrencyCode"/>
        <xsl:param name="isoCurrencySymbol"/>
        <xsl:param name="isFund"/>
        <xsl:if test="not($currencyName = 'No universal currency')">
            <xsl:text disable-output-escaping="yes">            
            </xsl:text>            
            <xsl:value-of select="$isoCurrencyCode"/> = new Currency("<xsl:value-of select="$currencyName"/>", "<xsl:value-of select="$isoCurrencyCode"/>", <xsl:value-of select="$isoCurrencySymbol"/>, <xsl:if test="$isFund = 'true'"><xsl:value-of select="$isFund"/></xsl:if><xsl:if test="not($isFund = 'true')">false</xsl:if>);
            CurrencyByIsoCurrencySymbol[<xsl:value-of select="$isoCurrencySymbol"/>] = <xsl:value-of select="$isoCurrencyCode"/>; // <xsl:value-of select="$currencyName"/>
        </xsl:if>
    </xsl:template>
    <!-- Template for Currency struct -->
    <xsl:template match="/Currencies"><xsl:text disable-output-escaping="yes">
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

// ReSharper disable InconsistentNaming

namespace System
{
    /// &lt;summary&gt;
    ///     Represents a system of money within which &lt;see cref="Money" /&gt;
    ///     amounts can be compared and arithmetic can be performed.
    ///     Updated on </xsl:text><xsl:value-of select="Updated"/><xsl:text disable-output-escaping="yes">
    ///     from https://www.six-group.com/dam/download/financial-information/data-center/iso-currrency/lists/list-one.xml
    /// &lt;/summary&gt;
    [Serializable]
    public struct Currency : IEquatable&lt;Currency&gt;, IFormatProvider
    {
        public static readonly Dictionary&lt;int, CurrencyInfo&gt; CurrencyInfoByIsoCurrencySymbol
                = new Dictionary&lt;int, CurrencyInfo&gt;();
            
        public static readonly Dictionary&lt;string, Currency&gt; CurrencyByIsoCurrencyCode
            = new Dictionary&lt;string, Currency&gt;(StringComparer.InvariantCultureIgnoreCase);
            
        public static readonly Dictionary&lt;string, int&gt; IsoCurrencySymbolByIsoCurrencyCode
            = new Dictionary&lt;string, int&gt;(StringComparer.InvariantCultureIgnoreCase);
        
        public static readonly Dictionary&lt;string, List&lt;int>> IsoCurrencySymbolsByCurrencySymbol
            = new Dictionary&lt;string, List&lt;int>>(StringComparer.InvariantCultureIgnoreCase);
        
        public static readonly Dictionary&lt;int, int&gt; IsoCurrencySymbolByLocalCultureId
            = new Dictionary&lt;int, int&gt;();
        
        public static readonly Dictionary&lt;int, List&lt;int>> LocalCultureIdsByIsoCurrencySymbol
            = new Dictionary&lt;int, List&lt;int>>();
        
        public static readonly Dictionary&lt;string, List&lt;int>> LocalCultureIdsByIsoCurrencyCode =
            new Dictionary&lt;string, List&lt;int>>();
        
        public static readonly Dictionary&lt;string, string&gt; CurrencySymbolByIsoCurrencySymbol =
            new Dictionary&lt;string, string&gt;();
        
        public static readonly Dictionary&lt;string, RegionInfo&gt; RegionInfoByIsoCurrencySymbol =
            new Dictionary&lt;string, RegionInfo&gt;();
        
        public static readonly Dictionary&lt;int, RegionInfo&gt; RegionInfoByLocalCultureId =
            new Dictionary&lt;int, RegionInfo&gt;();
        
        #region Static Currency Fields

        </xsl:text>
        <xsl:for-each select="Currency">
            <xsl:call-template name="static-Currency">
                <xsl:with-param name="currencyName" select="CurrencyName"/>
                <xsl:with-param name="isoCurrencyCode" select="IsoCurrencyCode"/>
                <xsl:with-param name="isoCurrencySymbol" select="IsoCurrencySymbol"/>
                <xsl:with-param name="isFund" select="IsFund"/>
            </xsl:call-template>
        </xsl:for-each>
        <xsl:text disable-output-escaping="yes">            
        public static readonly Currency None = XXX;

        #endregion

        static Currency()
        {
            var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (var culture in cultures)
            {
                var localCultureId = culture.LCID;
                var regionInfo = new RegionInfo(localCultureId);
                var isoCurrencySymbol = regionInfo.ISOCurrencySymbol;

                if (!LocalCultureIdsByIsoCurrencyCode.ContainsKey(isoCurrencySymbol))
                {
                    LocalCultureIdsByIsoCurrencyCode[isoCurrencySymbol] = new List&lt;int&gt;();
                }

                LocalCultureIdsByIsoCurrencyCode[isoCurrencySymbol].Add(localCultureId);

                RegionInfoByIsoCurrencySymbol[isoCurrencySymbol] = regionInfo;
                RegionInfoByLocalCultureId[localCultureId] = regionInfo;
                CurrencySymbolByIsoCurrencySymbol[isoCurrencySymbol] = regionInfo.CurrencySymbol;
            }
            
            #region set Static Currency Fields &amp; CurrencyByIsoCurrencySymbol
            
            // ... currency entries will be generated here using CurrencyByIsoCurrencySymbol-Dictionary template...
            </xsl:text>
            <xsl:for-each select="Currency">
                <xsl:call-template name="CurrencyByIsoCurrencySymbol-Dictionary">
                    <xsl:with-param name="currencyName" select="CurrencyName"/>
                    <xsl:with-param name="isoCurrencyCode" select="IsoCurrencyCode"/>
                    <xsl:with-param name="isoCurrencySymbol" select="IsoCurrencySymbol"/>
                    <xsl:with-param name="isFund" select="IsFund"/>
                </xsl:call-template>
            </xsl:for-each>
            <xsl:text disable-output-escaping="yes">
            
            #endregion

            foreach (var currency in CurrencyByIsoCurrencySymbol.Values)
            {
                var isoCurrencyCode = currency.IsoCurrencySymbol;
                var currencySymbol = currency.CurrencySymbol;

                CurrencyByIsoCurrencyCode[isoCurrencyCode] = currency;

                if (LocalCultureIdsByIsoCurrencyCode.TryGetValue(isoCurrencyCode, out var localCultureIds))
                {
                    foreach (var localCultureId in localCultureIds)
                        IsoCurrencySymbolByLocalCultureId[localCultureId] = currency.IsoCurrencyCode;

                    LocalCultureIdsByIsoCurrencySymbol[currency.IsoCurrencyCode] = localCultureIds;
                }

                IsoCurrencySymbolByIsoCurrencyCode[isoCurrencyCode] = currency.IsoCurrencyCode;

                if (currencySymbol == null) continue;

                if (!IsoCurrencySymbolsByCurrencySymbol.TryGetValue(currencySymbol, out var isoCurrencySymbols))
                {
                    isoCurrencySymbols = new List&lt;int&gt;();
                    IsoCurrencySymbolsByCurrencySymbol[currencySymbol] = isoCurrencySymbols;
                }

                isoCurrencySymbols.Add(currency.IsoCurrencyCode);
            }
        }

        private static Currency GetCurrencyByIsoCurrencySymbol(int isoCurrencySymbol)
        {
            if (isoCurrencySymbol == 0)
            {
                return None;
            }

            if (!CurrencyByIsoCurrencySymbol.TryGetValue(isoCurrencySymbol, out var currency))
                throw new InvalidOperationException("Unknown currency: " + isoCurrencySymbol +
                                                    " numeric ISO 4217 currency Symbol.");

            return currency;
        }

        private static Currency GetCurrencyByIsoCurrencyCode(string isoCurrencyCode)
        {
            if (isoCurrencyCode == null)
            {
                return None;
            }

            if (!CurrencyByIsoCurrencyCode.TryGetValue(isoCurrencyCode, out var currency))
                throw new InvalidOperationException("Unknown currency: " + isoCurrencyCode +
                                                    " ISO 4217 currency 3 letters code.");

            return currency;
        }

        private static NumberFormatInfo GetNumberFormatInfo(int isoCurrencyCode)
        {
            if (!LocalCultureIdsByIsoCurrencySymbol.TryGetValue(isoCurrencyCode, out var localCultureIds))
                return CultureInfo.CurrentCulture.NumberFormat;

            var localCultureId = localCultureIds.Contains(CultureInfo.CurrentCulture.LCID)
                ? CultureInfo.CurrentCulture.LCID
                : localCultureIds[0];

            return new CultureInfo(localCultureId).NumberFormat;
        }

        public Currency(string isoCurrencyCodeOrCurrencySymbol)
        {
            if (!TryParse(isoCurrencyCodeOrCurrencySymbol, out var currency))
                throw new ArgumentException("Unknown iso currency code or currency symbol: " +
                                            isoCurrencyCodeOrCurrencySymbol);

            this = currency;
        }

        /// &lt;summary&gt;
        ///      Try Parse
        ///      by Iso Currency &lt;see cref="Currency" /&gt; Code (e.i. USD, GBP)
        ///      then by Currency &lt;see cref="Currency" /&gt;. Symbol (e.i. $, £)
        /// &lt;/summary&gt;
        /// &lt;returns&gt;
        ///     The out &lt;see cref="Currency" /&gt; which corresponds
        ///     to the current Iso Currency Code Or Currency Symbol.
        /// &lt;/returns&gt;
        public static bool TryParse(string isoCurrencyCodeOrCurrencySymbol, out Currency currency)
        {
            currency = XXX;
            isoCurrencyCodeOrCurrencySymbol = isoCurrencyCodeOrCurrencySymbol.Trim();
            switch (isoCurrencyCodeOrCurrencySymbol.Length)
            {
                case 0:
                    return false;
                default:
                    return TryParseByIsoCurrencyCode(isoCurrencyCodeOrCurrencySymbol, out currency)
                           || TryParseByCurrencySymbol(isoCurrencyCodeOrCurrencySymbol, out currency);
            }
        }

        /// &lt;summary&gt;
        ///     Try Parse by Iso Currency &lt;see cref="Currency" /&gt; Code (e.i. USD, GBP)
        /// &lt;/summary&gt;
        /// &lt;returns&gt;
        ///     The out &lt;see cref="Currency" /&gt; which corresponds
        ///     to the current Iso Currency Code.
        /// &lt;/returns&gt;
        public static bool TryParseByIsoCurrencyCode(string isoCurrencyCode, out Currency currency)
        {
            if (!IsoCurrencySymbolByIsoCurrencyCode.TryGetValue(isoCurrencyCode,
                    out var isoCurrencySymbol))
            {
                currency = XXX;
                return false;
            }

            currency = GetCurrencyByIsoCurrencySymbol(isoCurrencySymbol);
            return true;
        }

        /// &lt;summary&gt;
        ///      Try Parse by Currency &lt;see cref="Currency" /&gt; Symbol (e.i. $, £)
        /// &lt;/summary&gt;
        /// &lt;returns&gt;
        ///     The out &lt;see cref="Currency" /&gt; which corresponds
        ///     to the current Currency Symbol.
        /// &lt;/returns&gt;
        public static bool TryParseByCurrencySymbol(string currencySymbol, out Currency currency)
        {
            if (!IsoCurrencySymbolsByCurrencySymbol.TryGetValue(currencySymbol,
                    out var isoCurrencySymbols))
            {
                currency = XXX;
                return false;
            }

            if (IsoCurrencySymbolByLocalCultureId.TryGetValue(Thread.CurrentThread.CurrentCulture.LCID,
                    out var isoCurrencySymbol)
                &amp;&amp; !isoCurrencySymbols.Contains(isoCurrencySymbol))
            {
                isoCurrencySymbol = isoCurrencySymbols[0];
            }

            currency = GetCurrencyByIsoCurrencySymbol(isoCurrencySymbol);
            return true;
        }

        public readonly string CurrencyName;
        public readonly string CurrencySymbol;
        public readonly string IsoCurrencySymbol;
        public readonly int IsoCurrencyCode;
        public readonly RegionInfo RegionInfo;
        public readonly NumberFormatInfo NumberFormatInfo;
        public readonly bool IsFund;

        public Currency(string currencyName,
            string isoCurrencySymbol,
            ushort isoCurrencyCode,
            bool isFund)
        {
            CurrencyName = currencyName;
            IsoCurrencyCode = isoCurrencyCode;
            IsoCurrencySymbol = isoCurrencySymbol;
            IsFund = isFund;
            NumberFormatInfo = GetNumberFormatInfo(isoCurrencyCode);
            RegionInfo = RegionInfoByIsoCurrencySymbol.GetValueOrDefault(isoCurrencySymbol);
            if (RegionInfo != null)
            {
                CurrencyName = RegionInfo.CurrencyEnglishName;
                CurrencySymbol = RegionInfo.CurrencySymbol;
                IsoCurrencySymbol = RegionInfo.ISOCurrencySymbol;
            }
            else
            {
                CurrencySymbol = CurrencySymbolByIsoCurrencySymbol.GetValueOrDefault(isoCurrencySymbol);
            }
        }

        /// &lt;summary&gt;
        ///     Creates a &lt;see cref="Currency" /&gt; instance from the
        ///     &lt;see cref="CultureInfo.CurrentCulture" /&gt;.
        /// &lt;/summary&gt;
        /// &lt;returns&gt;
        ///     The &lt;see cref="Currency" /&gt; which corresponds
        ///     to the current culture.
        /// &lt;/returns&gt;
        public static Currency FromCurrentCulture()
        {
            return FromCultureInfo(CultureInfo.CurrentCulture);
        }

        /// &lt;summary&gt;
        ///     Creates a &lt;see cref="Currency" /&gt; instance from the
        ///     given &lt;see cref="CultureInfo" /&gt;.
        /// &lt;/summary&gt;
        /// &lt;param name="cultureInfo"&gt;
        ///     The &lt;see cref="CultureInfo" /&gt; from which to create the currency.
        /// &lt;/param&gt;
        /// &lt;returns&gt;
        ///     The &lt;see cref="Currency" /&gt; which corresponds
        ///     to &lt;paramref name="cultureInfo" /&gt;.
        /// &lt;/returns&gt;
        public static Currency FromCultureInfo(CultureInfo cultureInfo)
        {
            if (IsoCurrencySymbolByLocalCultureId.TryGetValue(cultureInfo.LCID, out var localCultureId)
                &amp;&amp; CurrencyByIsoCurrencySymbol.TryGetValue(localCultureId, out var currency))
                return currency;

            throw new InvalidOperationException("Unknown culture: " + cultureInfo);
        }

        /// &lt;summary&gt;
        ///     Creates a &lt;see cref="Currency" /&gt; instance from its
        ///     3-letter ISO 4217 code.
        /// &lt;/summary&gt;
        /// &lt;param name="code"&gt;The ISO 4217 3-letter currency code.&lt;/param&gt;
        /// &lt;returns&gt;
        ///     The &lt;see cref="Currency" /&gt; which corresponds
        ///     to the ISO 4217 3-letter &lt;paramref name="code" /&gt;.
        /// &lt;/returns&gt;
        public static Currency FromIso3LetterCode(string code)
        {
            return new Currency(code);
        }

        /// &lt;summary&gt;
        ///     Compares two currency values for equality.
        /// &lt;/summary&gt;
        /// &lt;param name="left"&gt;The left side to compare.&lt;/param&gt;
        /// &lt;param name="right"&gt;The right side to compare.&lt;/param&gt;
        /// &lt;returns&gt;
        ///     &lt;see langword="true" /&gt; if they are equal; &lt;see langword="false" /&gt; otherwise.
        /// &lt;/returns&gt;
        public static bool operator ==(Currency left, Currency right)
        {
            return left.Equals(right);
        }

        /// &lt;summary&gt;
        ///     Compares two currency values for inequality.
        /// &lt;/summary&gt;
        /// &lt;param name="left"&gt;The left side to compare.&lt;/param&gt;
        /// &lt;param name="right"&gt;The right side to compare.&lt;/param&gt;
        /// &lt;returns&gt;
        ///     &lt;see langword="true" /&gt; if they are not equal; &lt;see langword="false" /&gt; otherwise.
        /// &lt;/returns&gt;
        public static bool operator !=(Currency left, Currency right)
        {
            return !left.Equals(right);
        }

        public override int GetHashCode()
        {
            return 609502847 ^ IsoCurrencyCode.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Currency other)) return false;

            return Equals(other);
        }

        public override string ToString()
        {
            return RegionInfo == null
                ? $"{CurrencyName} #{IsoCurrencyCode} {IsoCurrencySymbol} {CurrencySymbol}"
                : $"{CurrencyName} #{IsoCurrencyCode} {IsoCurrencySymbol} {CurrencySymbol} {RegionInfo}";
        }

        #region IEquatable&lt;Currency&gt; Members

        public bool Equals(Currency other)
        {
            return IsoCurrencyCode == other.IsoCurrencyCode;
        }

        #endregion

        #region IFormatProvider Members

        public object GetFormat(Type formatType)
        {
            return formatType != typeof(NumberFormatInfo) ? null : GetNumberFormatInfo(this.IsoCurrencyCode);
        }

        #endregion
    }

    internal static class DictionaryExtensions
    {
        public static string GetValueOrDefault(this IDictionary&lt;string, string&gt; table, string key)
        {
            string value;

            return !table.TryGetValue(key, out value) ? null : value;
        }

        public static RegionInfo GetValueOrDefault(this IDictionary&lt;string, RegionInfo&gt; table, string key)
        {
            RegionInfo value;

            return !table.TryGetValue(key, out value) ? null : value;
        }
    }
}</xsl:text>
    </xsl:template>
</xsl:stylesheet>