using System.Collections.Generic;
using System.Globalization;
using System.Threading;

// ReSharper disable InconsistentNaming

namespace System
{
    /// <summary>
    ///     Represents a system of money within which <see cref="Money" />
    ///     amounts can be compared and arithmetic can be performed.
    ///     Updated on 2024-08-21
    ///     from https://www.six-group.com/dam/download/financial-information/data-center/iso-currrency/lists/list-one.xml
    /// </summary>
    [Serializable]
    public struct Currency : IEquatable<Currency>, IFormatProvider
    {
        public static readonly Dictionary<int, Currency> CurrencyByIsoCurrencySymbol
            = new Dictionary<int, Currency>();

        public static readonly Dictionary<string, Currency> CurrencyByIsoCurrencyCode
            = new Dictionary<string, Currency>(StringComparer.InvariantCultureIgnoreCase);

        public static readonly Dictionary<string, int> IsoCurrencySymbolByIsoCurrencyCode
            = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

        public static readonly Dictionary<string, List<int>> IsoCurrencySymbolsByCurrencySymbol
            = new Dictionary<string, List<int>>(StringComparer.InvariantCultureIgnoreCase);

        public static readonly Dictionary<int, int> IsoCurrencySymbolByLocalCultureId
            = new Dictionary<int, int>();

        public static readonly Dictionary<int, List<int>> LocalCultureIdsByIsoCurrencySymbol
            = new Dictionary<int, List<int>>();

        public static readonly Dictionary<string, List<int>> LocalCultureIdsByIsoCurrencyCode =
            new Dictionary<string, List<int>>();

        public static readonly Dictionary<string, string> CurrencySymbolByIsoCurrencySymbol =
            new Dictionary<string, string>();

        public static readonly Dictionary<string, RegionInfo> RegionInfoByIsoCurrencySymbol =
            new Dictionary<string, RegionInfo>();

        public static readonly Dictionary<int, RegionInfo> RegionInfoByLocalCultureId =
            new Dictionary<int, RegionInfo>();

        #region Static Currency Fields

        /// <summary>
        ///     Currency # 008 - Lek
        /// </summary>
        public static readonly Currency ALL;

        /// <summary>
        ///     Currency # 012 - Algerian Dinar
        /// </summary>
        public static readonly Currency DZD;

        /// <summary>
        ///     Currency # 032 - Argentine Peso
        /// </summary>
        public static readonly Currency ARS;

        /// <summary>
        ///     Currency # 036 - Australian Dollar
        /// </summary>
        public static readonly Currency AUD;

        /// <summary>
        ///     Currency # 044 - Bahamian Dollar
        /// </summary>
        public static readonly Currency BSD;

        /// <summary>
        ///     Currency # 048 - Bahraini Dinar
        /// </summary>
        public static readonly Currency BHD;

        /// <summary>
        ///     Currency # 050 - Taka
        /// </summary>
        public static readonly Currency BDT;

        /// <summary>
        ///     Currency # 051 - Armenian Dram
        /// </summary>
        public static readonly Currency AMD;

        /// <summary>
        ///     Currency # 052 - Barbados Dollar
        /// </summary>
        public static readonly Currency BBD;

        /// <summary>
        ///     Currency # 060 - Bermudian Dollar
        /// </summary>
        public static readonly Currency BMD;

        /// <summary>
        ///     Currency # 064 - Ngultrum
        /// </summary>
        public static readonly Currency BTN;

        /// <summary>
        ///     Currency # 068 - Boliviano
        /// </summary>
        public static readonly Currency BOB;

        /// <summary>
        ///     Currency # 072 - Pula
        /// </summary>
        public static readonly Currency BWP;

        /// <summary>
        ///     Currency # 084 - Belize Dollar
        /// </summary>
        public static readonly Currency BZD;

        /// <summary>
        ///     Currency # 090 - Solomon Islands Dollar
        /// </summary>
        public static readonly Currency SBD;

        /// <summary>
        ///     Currency # 096 - Brunei Dollar
        /// </summary>
        public static readonly Currency BND;

        /// <summary>
        ///     Currency # 104 - Kyat
        /// </summary>
        public static readonly Currency MMK;

        /// <summary>
        ///     Currency # 108 - Burundi Franc
        /// </summary>
        public static readonly Currency BIF;

        /// <summary>
        ///     Currency # 116 - Riel
        /// </summary>
        public static readonly Currency KHR;

        /// <summary>
        ///     Currency # 124 - Canadian Dollar
        /// </summary>
        public static readonly Currency CAD;

        /// <summary>
        ///     Currency # 132 - Cabo Verde Escudo
        /// </summary>
        public static readonly Currency CVE;

        /// <summary>
        ///     Currency # 136 - Cayman Islands Dollar
        /// </summary>
        public static readonly Currency KYD;

        /// <summary>
        ///     Currency # 144 - Sri Lanka Rupee
        /// </summary>
        public static readonly Currency LKR;

        /// <summary>
        ///     Currency # 152 - Chilean Peso
        /// </summary>
        public static readonly Currency CLP;

        /// <summary>
        ///     Currency # 156 - Yuan Renminbi
        /// </summary>
        public static readonly Currency CNY;

        /// <summary>
        ///     Currency # 170 - Colombian Peso
        /// </summary>
        public static readonly Currency COP;

        /// <summary>
        ///     Currency # 174 - Comorian Franc
        /// </summary>
        public static readonly Currency KMF;

        /// <summary>
        ///     Currency # 188 - Costa Rican Colon
        /// </summary>
        public static readonly Currency CRC;

        /// <summary>
        ///     Currency # 192 - Cuban Peso
        /// </summary>
        public static readonly Currency CUP;

        /// <summary>
        ///     Currency # 203 - Czech Koruna
        /// </summary>
        public static readonly Currency CZK;

        /// <summary>
        ///     Currency # 208 - Danish Krone
        /// </summary>
        public static readonly Currency DKK;

        /// <summary>
        ///     Currency # 214 - Dominican Peso
        /// </summary>
        public static readonly Currency DOP;

        /// <summary>
        ///     Currency # 222 - El Salvador Colon
        /// </summary>
        public static readonly Currency SVC;

        /// <summary>
        ///     Currency # 230 - Ethiopian Birr
        /// </summary>
        public static readonly Currency ETB;

        /// <summary>
        ///     Currency # 232 - Nakfa
        /// </summary>
        public static readonly Currency ERN;

        /// <summary>
        ///     Currency # 238 - Falkland Islands Pound
        /// </summary>
        public static readonly Currency FKP;

        /// <summary>
        ///     Currency # 242 - Fiji Dollar
        /// </summary>
        public static readonly Currency FJD;

        /// <summary>
        ///     Currency # 262 - Djibouti Franc
        /// </summary>
        public static readonly Currency DJF;

        /// <summary>
        ///     Currency # 270 - Dalasi
        /// </summary>
        public static readonly Currency GMD;

        /// <summary>
        ///     Currency # 292 - Gibraltar Pound
        /// </summary>
        public static readonly Currency GIP;

        /// <summary>
        ///     Currency # 320 - Quetzal
        /// </summary>
        public static readonly Currency GTQ;

        /// <summary>
        ///     Currency # 324 - Guinean Franc
        /// </summary>
        public static readonly Currency GNF;

        /// <summary>
        ///     Currency # 328 - Guyana Dollar
        /// </summary>
        public static readonly Currency GYD;

        /// <summary>
        ///     Currency # 332 - Gourde
        /// </summary>
        public static readonly Currency HTG;

        /// <summary>
        ///     Currency # 340 - Lempira
        /// </summary>
        public static readonly Currency HNL;

        /// <summary>
        ///     Currency # 344 - Hong Kong Dollar
        /// </summary>
        public static readonly Currency HKD;

        /// <summary>
        ///     Currency # 348 - Forint
        /// </summary>
        public static readonly Currency HUF;

        /// <summary>
        ///     Currency # 352 - Iceland Krona
        /// </summary>
        public static readonly Currency ISK;

        /// <summary>
        ///     Currency # 356 - Indian Rupee
        /// </summary>
        public static readonly Currency INR;

        /// <summary>
        ///     Currency # 360 - Rupiah
        /// </summary>
        public static readonly Currency IDR;

        /// <summary>
        ///     Currency # 364 - Iranian Rial
        /// </summary>
        public static readonly Currency IRR;

        /// <summary>
        ///     Currency # 368 - Iraqi Dinar
        /// </summary>
        public static readonly Currency IQD;

        /// <summary>
        ///     Currency # 376 - New Israeli Sheqel
        /// </summary>
        public static readonly Currency ILS;

        /// <summary>
        ///     Currency # 388 - Jamaican Dollar
        /// </summary>
        public static readonly Currency JMD;

        /// <summary>
        ///     Currency # 392 - Yen
        /// </summary>
        public static readonly Currency JPY;

        /// <summary>
        ///     Currency # 398 - Tenge
        /// </summary>
        public static readonly Currency KZT;

        /// <summary>
        ///     Currency # 400 - Jordanian Dinar
        /// </summary>
        public static readonly Currency JOD;

        /// <summary>
        ///     Currency # 404 - Kenyan Shilling
        /// </summary>
        public static readonly Currency KES;

        /// <summary>
        ///     Currency # 408 - North Korean Won
        /// </summary>
        public static readonly Currency KPW;

        /// <summary>
        ///     Currency # 410 - Won
        /// </summary>
        public static readonly Currency KRW;

        /// <summary>
        ///     Currency # 414 - Kuwaiti Dinar
        /// </summary>
        public static readonly Currency KWD;

        /// <summary>
        ///     Currency # 417 - Som
        /// </summary>
        public static readonly Currency KGS;

        /// <summary>
        ///     Currency # 418 - Lao Kip
        /// </summary>
        public static readonly Currency LAK;

        /// <summary>
        ///     Currency # 422 - Lebanese Pound
        /// </summary>
        public static readonly Currency LBP;

        /// <summary>
        ///     Currency # 426 - Loti
        /// </summary>
        public static readonly Currency LSL;

        /// <summary>
        ///     Currency # 430 - Liberian Dollar
        /// </summary>
        public static readonly Currency LRD;

        /// <summary>
        ///     Currency # 434 - Libyan Dinar
        /// </summary>
        public static readonly Currency LYD;

        /// <summary>
        ///     Currency # 446 - Pataca
        /// </summary>
        public static readonly Currency MOP;

        /// <summary>
        ///     Currency # 454 - Malawi Kwacha
        /// </summary>
        public static readonly Currency MWK;

        /// <summary>
        ///     Currency # 458 - Malaysian Ringgit
        /// </summary>
        public static readonly Currency MYR;

        /// <summary>
        ///     Currency # 462 - Rufiyaa
        /// </summary>
        public static readonly Currency MVR;

        /// <summary>
        ///     Currency # 480 - Mauritius Rupee
        /// </summary>
        public static readonly Currency MUR;

        /// <summary>
        ///     Currency # 484 - Mexican Peso
        /// </summary>
        public static readonly Currency MXN;

        /// <summary>
        ///     Currency # 496 - Tugrik
        /// </summary>
        public static readonly Currency MNT;

        /// <summary>
        ///     Currency # 498 - Moldovan Leu
        /// </summary>
        public static readonly Currency MDL;

        /// <summary>
        ///     Currency # 504 - Moroccan Dirham
        /// </summary>
        public static readonly Currency MAD;

        /// <summary>
        ///     Currency # 512 - Rial Omani
        /// </summary>
        public static readonly Currency OMR;

        /// <summary>
        ///     Currency # 516 - Namibia Dollar
        /// </summary>
        public static readonly Currency NAD;

        /// <summary>
        ///     Currency # 524 - Nepalese Rupee
        /// </summary>
        public static readonly Currency NPR;

        /// <summary>
        ///     Currency # 532 - Netherlands Antillean Guilder
        /// </summary>
        public static readonly Currency ANG;

        /// <summary>
        ///     Currency # 533 - Aruban Florin
        /// </summary>
        public static readonly Currency AWG;

        /// <summary>
        ///     Currency # 548 - Vatu
        /// </summary>
        public static readonly Currency VUV;

        /// <summary>
        ///     Currency # 554 - New Zealand Dollar
        /// </summary>
        public static readonly Currency NZD;

        /// <summary>
        ///     Currency # 558 - Cordoba Oro
        /// </summary>
        public static readonly Currency NIO;

        /// <summary>
        ///     Currency # 566 - Naira
        /// </summary>
        public static readonly Currency NGN;

        /// <summary>
        ///     Currency # 578 - Norwegian Krone
        /// </summary>
        public static readonly Currency NOK;

        /// <summary>
        ///     Currency # 586 - Pakistan Rupee
        /// </summary>
        public static readonly Currency PKR;

        /// <summary>
        ///     Currency # 590 - Balboa
        /// </summary>
        public static readonly Currency PAB;

        /// <summary>
        ///     Currency # 598 - Kina
        /// </summary>
        public static readonly Currency PGK;

        /// <summary>
        ///     Currency # 600 - Guarani
        /// </summary>
        public static readonly Currency PYG;

        /// <summary>
        ///     Currency # 604 - Sol
        /// </summary>
        public static readonly Currency PEN;

        /// <summary>
        ///     Currency # 608 - Philippine Peso
        /// </summary>
        public static readonly Currency PHP;

        /// <summary>
        ///     Currency # 634 - Qatari Rial
        /// </summary>
        public static readonly Currency QAR;

        /// <summary>
        ///     Currency # 643 - Russian Ruble
        /// </summary>
        public static readonly Currency RUB;

        /// <summary>
        ///     Currency # 646 - Rwanda Franc
        /// </summary>
        public static readonly Currency RWF;

        /// <summary>
        ///     Currency # 654 - Saint Helena Pound
        /// </summary>
        public static readonly Currency SHP;

        /// <summary>
        ///     Currency # 682 - Saudi Riyal
        /// </summary>
        public static readonly Currency SAR;

        /// <summary>
        ///     Currency # 690 - Seychelles Rupee
        /// </summary>
        public static readonly Currency SCR;

        /// <summary>
        ///     Currency # 702 - Singapore Dollar
        /// </summary>
        public static readonly Currency SGD;

        /// <summary>
        ///     Currency # 704 - Dong
        /// </summary>
        public static readonly Currency VND;

        /// <summary>
        ///     Currency # 706 - Somali Shilling
        /// </summary>
        public static readonly Currency SOS;

        /// <summary>
        ///     Currency # 710 - Rand
        /// </summary>
        public static readonly Currency ZAR;

        /// <summary>
        ///     Currency # 728 - South Sudanese Pound
        /// </summary>
        public static readonly Currency SSP;

        /// <summary>
        ///     Currency # 748 - Lilangeni
        /// </summary>
        public static readonly Currency SZL;

        /// <summary>
        ///     Currency # 752 - Swedish Krona
        /// </summary>
        public static readonly Currency SEK;

        /// <summary>
        ///     Currency # 756 - Swiss Franc
        /// </summary>
        public static readonly Currency CHF;

        /// <summary>
        ///     Currency # 760 - Syrian Pound
        /// </summary>
        public static readonly Currency SYP;

        /// <summary>
        ///     Currency # 764 - Baht
        /// </summary>
        public static readonly Currency THB;

        /// <summary>
        ///     Currency # 776 - Pa’anga
        /// </summary>
        public static readonly Currency TOP;

        /// <summary>
        ///     Currency # 780 - Trinidad and Tobago Dollar
        /// </summary>
        public static readonly Currency TTD;

        /// <summary>
        ///     Currency # 784 - UAE Dirham
        /// </summary>
        public static readonly Currency AED;

        /// <summary>
        ///     Currency # 788 - Tunisian Dinar
        /// </summary>
        public static readonly Currency TND;

        /// <summary>
        ///     Currency # 800 - Uganda Shilling
        /// </summary>
        public static readonly Currency UGX;

        /// <summary>
        ///     Currency # 807 - Denar
        /// </summary>
        public static readonly Currency MKD;

        /// <summary>
        ///     Currency # 818 - Egyptian Pound
        /// </summary>
        public static readonly Currency EGP;

        /// <summary>
        ///     Currency # 826 - Pound Sterling
        /// </summary>
        public static readonly Currency GBP;

        /// <summary>
        ///     Currency # 834 - Tanzanian Shilling
        /// </summary>
        public static readonly Currency TZS;

        /// <summary>
        ///     Currency # 840 - US Dollar
        /// </summary>
        public static readonly Currency USD;

        /// <summary>
        ///     Currency # 858 - Peso Uruguayo
        /// </summary>
        public static readonly Currency UYU;

        /// <summary>
        ///     Currency # 860 - Uzbekistan Sum
        /// </summary>
        public static readonly Currency UZS;

        /// <summary>
        ///     Currency # 882 - Tala
        /// </summary>
        public static readonly Currency WST;

        /// <summary>
        ///     Currency # 886 - Yemeni Rial
        /// </summary>
        public static readonly Currency YER;

        /// <summary>
        ///     Currency # 901 - New Taiwan Dollar
        /// </summary>
        public static readonly Currency TWD;

        /// <summary>
        ///     Currency # 924 - Zimbabwe Gold
        /// </summary>
        public static readonly Currency ZWG;

        /// <summary>
        ///     Currency # 925 - Leone
        /// </summary>
        public static readonly Currency SLE;

        /// <summary>
        ///     Currency # 926 - Bolívar Soberano
        /// </summary>
        public static readonly Currency VED;

        /// <summary>
        ///     Currency # 927 - Unidad Previsional
        /// </summary>
        public static readonly Currency UYW;

        /// <summary>
        ///     Currency # 928 - Bolívar Soberano
        /// </summary>
        public static readonly Currency VES;

        /// <summary>
        ///     Currency # 929 - Ouguiya
        /// </summary>
        public static readonly Currency MRU;

        /// <summary>
        ///     Currency # 930 - Dobra
        /// </summary>
        public static readonly Currency STN;

        /// <summary>
        ///     Currency # 931 - Peso Convertible
        /// </summary>
        public static readonly Currency CUC;

        /// <summary>
        ///     Currency # 932 - Zimbabwe Dollar
        /// </summary>
        public static readonly Currency ZWL;

        /// <summary>
        ///     Currency # 933 - Belarusian Ruble
        /// </summary>
        public static readonly Currency BYN;

        /// <summary>
        ///     Currency # 934 - Turkmenistan New Manat
        /// </summary>
        public static readonly Currency TMT;

        /// <summary>
        ///     Currency # 936 - Ghana Cedi
        /// </summary>
        public static readonly Currency GHS;

        /// <summary>
        ///     Currency # 938 - Sudanese Pound
        /// </summary>
        public static readonly Currency SDG;

        /// <summary>
        ///     Currency # 940 - Uruguay Peso en Unidades Indexadas (UI) isFund = true
        /// </summary>
        public static readonly Currency UYI;

        /// <summary>
        ///     Currency # 941 - Serbian Dinar
        /// </summary>
        public static readonly Currency RSD;

        /// <summary>
        ///     Currency # 943 - Mozambique Metical
        /// </summary>
        public static readonly Currency MZN;

        /// <summary>
        ///     Currency # 944 - Azerbaijan Manat
        /// </summary>
        public static readonly Currency AZN;

        /// <summary>
        ///     Currency # 946 - Romanian Leu
        /// </summary>
        public static readonly Currency RON;

        /// <summary>
        ///     Currency # 947 - WIR Euro isFund = true
        /// </summary>
        public static readonly Currency CHE;

        /// <summary>
        ///     Currency # 948 - WIR Franc isFund = true
        /// </summary>
        public static readonly Currency CHW;

        /// <summary>
        ///     Currency # 949 - Turkish Lira
        /// </summary>
        public static readonly Currency TRY;

        /// <summary>
        ///     Currency # 950 - CFA Franc BEAC
        /// </summary>
        public static readonly Currency XAF;

        /// <summary>
        ///     Currency # 951 - East Caribbean Dollar
        /// </summary>
        public static readonly Currency XCD;

        /// <summary>
        ///     Currency # 952 - CFA Franc BCEAO
        /// </summary>
        public static readonly Currency XOF;

        /// <summary>
        ///     Currency # 953 - CFP Franc
        /// </summary>
        public static readonly Currency XPF;

        /// <summary>
        ///     Currency # 955 - Bond Markets Unit European Composite Unit (EURCO)
        /// </summary>
        public static readonly Currency XBA;

        /// <summary>
        ///     Currency # 956 - Bond Markets Unit European Monetary Unit (E.M.U.-6)
        /// </summary>
        public static readonly Currency XBB;

        /// <summary>
        ///     Currency # 957 - Bond Markets Unit European Unit of Account 9 (E.U.A.-9)
        /// </summary>
        public static readonly Currency XBC;

        /// <summary>
        ///     Currency # 958 - Bond Markets Unit European Unit of Account 17 (E.U.A.-17)
        /// </summary>
        public static readonly Currency XBD;

        /// <summary>
        ///     Currency # 959 - Gold
        /// </summary>
        public static readonly Currency XAU;

        /// <summary>
        ///     Currency # 960 - SDR (Special Drawing Right)
        /// </summary>
        public static readonly Currency XDR;

        /// <summary>
        ///     Currency # 961 - Silver
        /// </summary>
        public static readonly Currency XAG;

        /// <summary>
        ///     Currency # 962 - Platinum
        /// </summary>
        public static readonly Currency XPT;

        /// <summary>
        ///     Currency # 963 - Codes specifically reserved for testing purposes
        /// </summary>
        public static readonly Currency XTS;

        /// <summary>
        ///     Currency # 964 - Palladium
        /// </summary>
        public static readonly Currency XPD;

        /// <summary>
        ///     Currency # 965 - ADB Unit of Account
        /// </summary>
        public static readonly Currency XUA;

        /// <summary>
        ///     Currency # 967 - Zambian Kwacha
        /// </summary>
        public static readonly Currency ZMW;

        /// <summary>
        ///     Currency # 968 - Surinam Dollar
        /// </summary>
        public static readonly Currency SRD;

        /// <summary>
        ///     Currency # 969 - Malagasy Ariary
        /// </summary>
        public static readonly Currency MGA;

        /// <summary>
        ///     Currency # 970 - Unidad de Valor Real isFund = true
        /// </summary>
        public static readonly Currency COU;

        /// <summary>
        ///     Currency # 971 - Afghani
        /// </summary>
        public static readonly Currency AFN;

        /// <summary>
        ///     Currency # 972 - Somoni
        /// </summary>
        public static readonly Currency TJS;

        /// <summary>
        ///     Currency # 973 - Kwanza
        /// </summary>
        public static readonly Currency AOA;

        /// <summary>
        ///     Currency # 975 - Bulgarian Lev
        /// </summary>
        public static readonly Currency BGN;

        /// <summary>
        ///     Currency # 976 - Congolese Franc
        /// </summary>
        public static readonly Currency CDF;

        /// <summary>
        ///     Currency # 977 - Convertible Mark
        /// </summary>
        public static readonly Currency BAM;

        /// <summary>
        ///     Currency # 978 - Euro
        /// </summary>
        public static readonly Currency EUR;

        /// <summary>
        ///     Currency # 979 - Mexican Unidad de Inversion (UDI) isFund = true
        /// </summary>
        public static readonly Currency MXV;

        /// <summary>
        ///     Currency # 980 - Hryvnia
        /// </summary>
        public static readonly Currency UAH;

        /// <summary>
        ///     Currency # 981 - Lari
        /// </summary>
        public static readonly Currency GEL;

        /// <summary>
        ///     Currency # 984 - Mvdol isFund = true
        /// </summary>
        public static readonly Currency BOV;

        /// <summary>
        ///     Currency # 985 - Zloty
        /// </summary>
        public static readonly Currency PLN;

        /// <summary>
        ///     Currency # 986 - Brazilian Real
        /// </summary>
        public static readonly Currency BRL;

        /// <summary>
        ///     Currency # 990 - Unidad de Fomento isFund = true
        /// </summary>
        public static readonly Currency CLF;

        /// <summary>
        ///     Currency # 994 - Sucre
        /// </summary>
        public static readonly Currency XSU;

        /// <summary>
        ///     Currency # 997 - US Dollar (Next day) isFund = true
        /// </summary>
        public static readonly Currency USN;

        /// <summary>
        ///     Currency # 998 - US Dollar (Same day) isFund = true
        /// </summary>
        public static readonly Currency USS;

        /// <summary>
        ///     Currency # 999 - The codes assigned for transactions where no currency is involved
        /// </summary>
        public static readonly Currency XXX;

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
                    LocalCultureIdsByIsoCurrencyCode[isoCurrencySymbol] = new List<int>();
                }

                LocalCultureIdsByIsoCurrencyCode[isoCurrencySymbol].Add(localCultureId);

                RegionInfoByIsoCurrencySymbol[isoCurrencySymbol] = regionInfo;
                RegionInfoByLocalCultureId[localCultureId] = regionInfo;
                CurrencySymbolByIsoCurrencySymbol[isoCurrencySymbol] = regionInfo.CurrencySymbol;
            }

            #region set Static Currency Fields & CurrencyByIsoCurrencySymbol

            USD = new Currency("US Dollar", "USD", 840, false);
            CurrencyByIsoCurrencySymbol[840] = USD; // US Dollar                      
            JPY = new Currency("Yen", "JPY", 392, false);
            CurrencyByIsoCurrencySymbol[392] = JPY; // Yen             
            ALL = new Currency("Lek", "ALL", 008, false);
            CurrencyByIsoCurrencySymbol[008] = ALL; // Lek            
            DZD = new Currency("Algerian Dinar", "DZD", 012, false);
            CurrencyByIsoCurrencySymbol[012] = DZD; // Algerian Dinar            
            ARS = new Currency("Argentine Peso", "ARS", 032, false);
            CurrencyByIsoCurrencySymbol[032] = ARS; // Argentine Peso            
            AUD = new Currency("Australian Dollar", "AUD", 036, false);
            CurrencyByIsoCurrencySymbol[036] = AUD; // Australian Dollar            
            BSD = new Currency("Bahamian Dollar", "BSD", 044, false);
            CurrencyByIsoCurrencySymbol[044] = BSD; // Bahamian Dollar            
            BHD = new Currency("Bahraini Dinar", "BHD", 048, false);
            CurrencyByIsoCurrencySymbol[048] = BHD; // Bahraini Dinar            
            BDT = new Currency("Taka", "BDT", 050, false);
            CurrencyByIsoCurrencySymbol[050] = BDT; // Taka            
            AMD = new Currency("Armenian Dram", "AMD", 051, false);
            CurrencyByIsoCurrencySymbol[051] = AMD; // Armenian Dram            
            BBD = new Currency("Barbados Dollar", "BBD", 052, false);
            CurrencyByIsoCurrencySymbol[052] = BBD; // Barbados Dollar            
            BMD = new Currency("Bermudian Dollar", "BMD", 060, false);
            CurrencyByIsoCurrencySymbol[060] = BMD; // Bermudian Dollar            
            BTN = new Currency("Ngultrum", "BTN", 064, false);
            CurrencyByIsoCurrencySymbol[064] = BTN; // Ngultrum            
            BOB = new Currency("Boliviano", "BOB", 068, false);
            CurrencyByIsoCurrencySymbol[068] = BOB; // Boliviano            
            BWP = new Currency("Pula", "BWP", 072, false);
            CurrencyByIsoCurrencySymbol[072] = BWP; // Pula            
            BZD = new Currency("Belize Dollar", "BZD", 084, false);
            CurrencyByIsoCurrencySymbol[084] = BZD; // Belize Dollar            
            SBD = new Currency("Solomon Islands Dollar", "SBD", 090, false);
            CurrencyByIsoCurrencySymbol[090] = SBD; // Solomon Islands Dollar            
            BND = new Currency("Brunei Dollar", "BND", 096, false);
            CurrencyByIsoCurrencySymbol[096] = BND; // Brunei Dollar            
            MMK = new Currency("Kyat", "MMK", 104, false);
            CurrencyByIsoCurrencySymbol[104] = MMK; // Kyat            
            BIF = new Currency("Burundi Franc", "BIF", 108, false);
            CurrencyByIsoCurrencySymbol[108] = BIF; // Burundi Franc            
            KHR = new Currency("Riel", "KHR", 116, false);
            CurrencyByIsoCurrencySymbol[116] = KHR; // Riel            
            CAD = new Currency("Canadian Dollar", "CAD", 124, false);
            CurrencyByIsoCurrencySymbol[124] = CAD; // Canadian Dollar            
            CVE = new Currency("Cabo Verde Escudo", "CVE", 132, false);
            CurrencyByIsoCurrencySymbol[132] = CVE; // Cabo Verde Escudo            
            KYD = new Currency("Cayman Islands Dollar", "KYD", 136, false);
            CurrencyByIsoCurrencySymbol[136] = KYD; // Cayman Islands Dollar            
            LKR = new Currency("Sri Lanka Rupee", "LKR", 144, false);
            CurrencyByIsoCurrencySymbol[144] = LKR; // Sri Lanka Rupee            
            CLP = new Currency("Chilean Peso", "CLP", 152, false);
            CurrencyByIsoCurrencySymbol[152] = CLP; // Chilean Peso            
            CNY = new Currency("Yuan Renminbi", "CNY", 156, false);
            CurrencyByIsoCurrencySymbol[156] = CNY; // Yuan Renminbi            
            COP = new Currency("Colombian Peso", "COP", 170, false);
            CurrencyByIsoCurrencySymbol[170] = COP; // Colombian Peso            
            KMF = new Currency("Comorian Franc", "KMF", 174, false);
            CurrencyByIsoCurrencySymbol[174] = KMF; // Comorian Franc            
            CRC = new Currency("Costa Rican Colon", "CRC", 188, false);
            CurrencyByIsoCurrencySymbol[188] = CRC; // Costa Rican Colon            
            CUP = new Currency("Cuban Peso", "CUP", 192, false);
            CurrencyByIsoCurrencySymbol[192] = CUP; // Cuban Peso            
            CZK = new Currency("Czech Koruna", "CZK", 203, false);
            CurrencyByIsoCurrencySymbol[203] = CZK; // Czech Koruna            
            DKK = new Currency("Danish Krone", "DKK", 208, false);
            CurrencyByIsoCurrencySymbol[208] = DKK; // Danish Krone            
            DOP = new Currency("Dominican Peso", "DOP", 214, false);
            CurrencyByIsoCurrencySymbol[214] = DOP; // Dominican Peso            
            SVC = new Currency("El Salvador Colon", "SVC", 222, false);
            CurrencyByIsoCurrencySymbol[222] = SVC; // El Salvador Colon            
            ETB = new Currency("Ethiopian Birr", "ETB", 230, false);
            CurrencyByIsoCurrencySymbol[230] = ETB; // Ethiopian Birr            
            ERN = new Currency("Nakfa", "ERN", 232, false);
            CurrencyByIsoCurrencySymbol[232] = ERN; // Nakfa            
            FKP = new Currency("Falkland Islands Pound", "FKP", 238, false);
            CurrencyByIsoCurrencySymbol[238] = FKP; // Falkland Islands Pound            
            FJD = new Currency("Fiji Dollar", "FJD", 242, false);
            CurrencyByIsoCurrencySymbol[242] = FJD; // Fiji Dollar            
            DJF = new Currency("Djibouti Franc", "DJF", 262, false);
            CurrencyByIsoCurrencySymbol[262] = DJF; // Djibouti Franc            
            GMD = new Currency("Dalasi", "GMD", 270, false);
            CurrencyByIsoCurrencySymbol[270] = GMD; // Dalasi            
            GIP = new Currency("Gibraltar Pound", "GIP", 292, false);
            CurrencyByIsoCurrencySymbol[292] = GIP; // Gibraltar Pound            
            GTQ = new Currency("Quetzal", "GTQ", 320, false);
            CurrencyByIsoCurrencySymbol[320] = GTQ; // Quetzal            
            GNF = new Currency("Guinean Franc", "GNF", 324, false);
            CurrencyByIsoCurrencySymbol[324] = GNF; // Guinean Franc            
            GYD = new Currency("Guyana Dollar", "GYD", 328, false);
            CurrencyByIsoCurrencySymbol[328] = GYD; // Guyana Dollar            
            HTG = new Currency("Gourde", "HTG", 332, false);
            CurrencyByIsoCurrencySymbol[332] = HTG; // Gourde            
            HNL = new Currency("Lempira", "HNL", 340, false);
            CurrencyByIsoCurrencySymbol[340] = HNL; // Lempira            
            HKD = new Currency("Hong Kong Dollar", "HKD", 344, false);
            CurrencyByIsoCurrencySymbol[344] = HKD; // Hong Kong Dollar            
            HUF = new Currency("Forint", "HUF", 348, false);
            CurrencyByIsoCurrencySymbol[348] = HUF; // Forint            
            ISK = new Currency("Iceland Krona", "ISK", 352, false);
            CurrencyByIsoCurrencySymbol[352] = ISK; // Iceland Krona            
            INR = new Currency("Indian Rupee", "INR", 356, false);
            CurrencyByIsoCurrencySymbol[356] = INR; // Indian Rupee            
            IDR = new Currency("Rupiah", "IDR", 360, false);
            CurrencyByIsoCurrencySymbol[360] = IDR; // Rupiah            
            IRR = new Currency("Iranian Rial", "IRR", 364, false);
            CurrencyByIsoCurrencySymbol[364] = IRR; // Iranian Rial            
            IQD = new Currency("Iraqi Dinar", "IQD", 368, false);
            CurrencyByIsoCurrencySymbol[368] = IQD; // Iraqi Dinar            
            ILS = new Currency("New Israeli Sheqel", "ILS", 376, false);
            CurrencyByIsoCurrencySymbol[376] = ILS; // New Israeli Sheqel            
            JMD = new Currency("Jamaican Dollar", "JMD", 388, false);
            CurrencyByIsoCurrencySymbol[388] = JMD; // Jamaican Dollar 
            KZT = new Currency("Tenge", "KZT", 398, false);
            CurrencyByIsoCurrencySymbol[398] = KZT; // Tenge            
            JOD = new Currency("Jordanian Dinar", "JOD", 400, false);
            CurrencyByIsoCurrencySymbol[400] = JOD; // Jordanian Dinar            
            KES = new Currency("Kenyan Shilling", "KES", 404, false);
            CurrencyByIsoCurrencySymbol[404] = KES; // Kenyan Shilling            
            KPW = new Currency("North Korean Won", "KPW", 408, false);
            CurrencyByIsoCurrencySymbol[408] = KPW; // North Korean Won            
            KRW = new Currency("Won", "KRW", 410, false);
            CurrencyByIsoCurrencySymbol[410] = KRW; // Won            
            KWD = new Currency("Kuwaiti Dinar", "KWD", 414, false);
            CurrencyByIsoCurrencySymbol[414] = KWD; // Kuwaiti Dinar            
            KGS = new Currency("Som", "KGS", 417, false);
            CurrencyByIsoCurrencySymbol[417] = KGS; // Som            
            LAK = new Currency("Lao Kip", "LAK", 418, false);
            CurrencyByIsoCurrencySymbol[418] = LAK; // Lao Kip            
            LBP = new Currency("Lebanese Pound", "LBP", 422, false);
            CurrencyByIsoCurrencySymbol[422] = LBP; // Lebanese Pound            
            LSL = new Currency("Loti", "LSL", 426, false);
            CurrencyByIsoCurrencySymbol[426] = LSL; // Loti            
            LRD = new Currency("Liberian Dollar", "LRD", 430, false);
            CurrencyByIsoCurrencySymbol[430] = LRD; // Liberian Dollar            
            LYD = new Currency("Libyan Dinar", "LYD", 434, false);
            CurrencyByIsoCurrencySymbol[434] = LYD; // Libyan Dinar            
            MOP = new Currency("Pataca", "MOP", 446, false);
            CurrencyByIsoCurrencySymbol[446] = MOP; // Pataca            
            MWK = new Currency("Malawi Kwacha", "MWK", 454, false);
            CurrencyByIsoCurrencySymbol[454] = MWK; // Malawi Kwacha            
            MYR = new Currency("Malaysian Ringgit", "MYR", 458, false);
            CurrencyByIsoCurrencySymbol[458] = MYR; // Malaysian Ringgit            
            MVR = new Currency("Rufiyaa", "MVR", 462, false);
            CurrencyByIsoCurrencySymbol[462] = MVR; // Rufiyaa            
            MUR = new Currency("Mauritius Rupee", "MUR", 480, false);
            CurrencyByIsoCurrencySymbol[480] = MUR; // Mauritius Rupee            
            MXN = new Currency("Mexican Peso", "MXN", 484, false);
            CurrencyByIsoCurrencySymbol[484] = MXN; // Mexican Peso            
            MNT = new Currency("Tugrik", "MNT", 496, false);
            CurrencyByIsoCurrencySymbol[496] = MNT; // Tugrik            
            MDL = new Currency("Moldovan Leu", "MDL", 498, false);
            CurrencyByIsoCurrencySymbol[498] = MDL; // Moldovan Leu            
            MAD = new Currency("Moroccan Dirham", "MAD", 504, false);
            CurrencyByIsoCurrencySymbol[504] = MAD; // Moroccan Dirham            
            OMR = new Currency("Rial Omani", "OMR", 512, false);
            CurrencyByIsoCurrencySymbol[512] = OMR; // Rial Omani            
            NAD = new Currency("Namibia Dollar", "NAD", 516, false);
            CurrencyByIsoCurrencySymbol[516] = NAD; // Namibia Dollar            
            NPR = new Currency("Nepalese Rupee", "NPR", 524, false);
            CurrencyByIsoCurrencySymbol[524] = NPR; // Nepalese Rupee            
            ANG = new Currency("Netherlands Antillean Guilder", "ANG", 532, false);
            CurrencyByIsoCurrencySymbol[532] = ANG; // Netherlands Antillean Guilder            
            AWG = new Currency("Aruban Florin", "AWG", 533, false);
            CurrencyByIsoCurrencySymbol[533] = AWG; // Aruban Florin            
            VUV = new Currency("Vatu", "VUV", 548, false);
            CurrencyByIsoCurrencySymbol[548] = VUV; // Vatu            
            NZD = new Currency("New Zealand Dollar", "NZD", 554, false);
            CurrencyByIsoCurrencySymbol[554] = NZD; // New Zealand Dollar            
            NIO = new Currency("Cordoba Oro", "NIO", 558, false);
            CurrencyByIsoCurrencySymbol[558] = NIO; // Cordoba Oro            
            NGN = new Currency("Naira", "NGN", 566, false);
            CurrencyByIsoCurrencySymbol[566] = NGN; // Naira            
            NOK = new Currency("Norwegian Krone", "NOK", 578, false);
            CurrencyByIsoCurrencySymbol[578] = NOK; // Norwegian Krone            
            PKR = new Currency("Pakistan Rupee", "PKR", 586, false);
            CurrencyByIsoCurrencySymbol[586] = PKR; // Pakistan Rupee            
            PAB = new Currency("Balboa", "PAB", 590, false);
            CurrencyByIsoCurrencySymbol[590] = PAB; // Balboa            
            PGK = new Currency("Kina", "PGK", 598, false);
            CurrencyByIsoCurrencySymbol[598] = PGK; // Kina            
            PYG = new Currency("Guarani", "PYG", 600, false);
            CurrencyByIsoCurrencySymbol[600] = PYG; // Guarani            
            PEN = new Currency("Sol", "PEN", 604, false);
            CurrencyByIsoCurrencySymbol[604] = PEN; // Sol            
            PHP = new Currency("Philippine Peso", "PHP", 608, false);
            CurrencyByIsoCurrencySymbol[608] = PHP; // Philippine Peso            
            QAR = new Currency("Qatari Rial", "QAR", 634, false);
            CurrencyByIsoCurrencySymbol[634] = QAR; // Qatari Rial            
            RUB = new Currency("Russian Ruble", "RUB", 643, false);
            CurrencyByIsoCurrencySymbol[643] = RUB; // Russian Ruble            
            RWF = new Currency("Rwanda Franc", "RWF", 646, false);
            CurrencyByIsoCurrencySymbol[646] = RWF; // Rwanda Franc            
            SHP = new Currency("Saint Helena Pound", "SHP", 654, false);
            CurrencyByIsoCurrencySymbol[654] = SHP; // Saint Helena Pound            
            SAR = new Currency("Saudi Riyal", "SAR", 682, false);
            CurrencyByIsoCurrencySymbol[682] = SAR; // Saudi Riyal            
            SCR = new Currency("Seychelles Rupee", "SCR", 690, false);
            CurrencyByIsoCurrencySymbol[690] = SCR; // Seychelles Rupee            
            SGD = new Currency("Singapore Dollar", "SGD", 702, false);
            CurrencyByIsoCurrencySymbol[702] = SGD; // Singapore Dollar            
            VND = new Currency("Dong", "VND", 704, false);
            CurrencyByIsoCurrencySymbol[704] = VND; // Dong            
            SOS = new Currency("Somali Shilling", "SOS", 706, false);
            CurrencyByIsoCurrencySymbol[706] = SOS; // Somali Shilling            
            ZAR = new Currency("Rand", "ZAR", 710, false);
            CurrencyByIsoCurrencySymbol[710] = ZAR; // Rand            
            SSP = new Currency("South Sudanese Pound", "SSP", 728, false);
            CurrencyByIsoCurrencySymbol[728] = SSP; // South Sudanese Pound            
            SZL = new Currency("Lilangeni", "SZL", 748, false);
            CurrencyByIsoCurrencySymbol[748] = SZL; // Lilangeni            
            SEK = new Currency("Swedish Krona", "SEK", 752, false);
            CurrencyByIsoCurrencySymbol[752] = SEK; // Swedish Krona            
            CHF = new Currency("Swiss Franc", "CHF", 756, false);
            CurrencyByIsoCurrencySymbol[756] = CHF; // Swiss Franc            
            SYP = new Currency("Syrian Pound", "SYP", 760, false);
            CurrencyByIsoCurrencySymbol[760] = SYP; // Syrian Pound            
            THB = new Currency("Baht", "THB", 764, false);
            CurrencyByIsoCurrencySymbol[764] = THB; // Baht            
            TOP = new Currency("Pa&rsquo;anga", "TOP", 776, false);
            CurrencyByIsoCurrencySymbol[776] = TOP; // Pa&rsquo;anga            
            TTD = new Currency("Trinidad and Tobago Dollar", "TTD", 780, false);
            CurrencyByIsoCurrencySymbol[780] = TTD; // Trinidad and Tobago Dollar            
            AED = new Currency("UAE Dirham", "AED", 784, false);
            CurrencyByIsoCurrencySymbol[784] = AED; // UAE Dirham            
            TND = new Currency("Tunisian Dinar", "TND", 788, false);
            CurrencyByIsoCurrencySymbol[788] = TND; // Tunisian Dinar            
            UGX = new Currency("Uganda Shilling", "UGX", 800, false);
            CurrencyByIsoCurrencySymbol[800] = UGX; // Uganda Shilling            
            MKD = new Currency("Denar", "MKD", 807, false);
            CurrencyByIsoCurrencySymbol[807] = MKD; // Denar            
            EGP = new Currency("Egyptian Pound", "EGP", 818, false);
            CurrencyByIsoCurrencySymbol[818] = EGP; // Egyptian Pound            
            GBP = new Currency("Pound Sterling", "GBP", 826, false);
            CurrencyByIsoCurrencySymbol[826] = GBP; // Pound Sterling            
            TZS = new Currency("Tanzanian Shilling", "TZS", 834, false);
            CurrencyByIsoCurrencySymbol[834] = TZS; // Tanzanian Shilling            
            UYU = new Currency("Peso Uruguayo", "UYU", 858, false);
            CurrencyByIsoCurrencySymbol[858] = UYU; // Peso Uruguayo            
            UZS = new Currency("Uzbekistan Sum", "UZS", 860, false);
            CurrencyByIsoCurrencySymbol[860] = UZS; // Uzbekistan Sum            
            WST = new Currency("Tala", "WST", 882, false);
            CurrencyByIsoCurrencySymbol[882] = WST; // Tala            
            YER = new Currency("Yemeni Rial", "YER", 886, false);
            CurrencyByIsoCurrencySymbol[886] = YER; // Yemeni Rial            
            TWD = new Currency("New Taiwan Dollar", "TWD", 901, false);
            CurrencyByIsoCurrencySymbol[901] = TWD; // New Taiwan Dollar            
            ZWG = new Currency("Zimbabwe Gold", "ZWG", 924, false);
            CurrencyByIsoCurrencySymbol[924] = ZWG; // Zimbabwe Gold            
            SLE = new Currency("Leone", "SLE", 925, false);
            CurrencyByIsoCurrencySymbol[925] = SLE; // Leone            
            VED = new Currency("Bol&iacute;var Soberano", "VED", 926, false);
            CurrencyByIsoCurrencySymbol[926] = VED; // Bol&iacute;var Soberano            
            UYW = new Currency("Unidad Previsional", "UYW", 927, false);
            CurrencyByIsoCurrencySymbol[927] = UYW; // Unidad Previsional            
            VES = new Currency("Bol&iacute;var Soberano", "VES", 928, false);
            CurrencyByIsoCurrencySymbol[928] = VES; // Bol&iacute;var Soberano            
            MRU = new Currency("Ouguiya", "MRU", 929, false);
            CurrencyByIsoCurrencySymbol[929] = MRU; // Ouguiya            
            STN = new Currency("Dobra", "STN", 930, false);
            CurrencyByIsoCurrencySymbol[930] = STN; // Dobra            
            CUC = new Currency("Peso Convertible", "CUC", 931, false);
            CurrencyByIsoCurrencySymbol[931] = CUC; // Peso Convertible            
            ZWL = new Currency("Zimbabwe Dollar", "ZWL", 932, false);
            CurrencyByIsoCurrencySymbol[932] = ZWL; // Zimbabwe Dollar            
            BYN = new Currency("Belarusian Ruble", "BYN", 933, false);
            CurrencyByIsoCurrencySymbol[933] = BYN; // Belarusian Ruble            
            TMT = new Currency("Turkmenistan New Manat", "TMT", 934, false);
            CurrencyByIsoCurrencySymbol[934] = TMT; // Turkmenistan New Manat            
            GHS = new Currency("Ghana Cedi", "GHS", 936, false);
            CurrencyByIsoCurrencySymbol[936] = GHS; // Ghana Cedi            
            SDG = new Currency("Sudanese Pound", "SDG", 938, false);
            CurrencyByIsoCurrencySymbol[938] = SDG; // Sudanese Pound            
            UYI = new Currency("Uruguay Peso en Unidades Indexadas (UI)", "UYI", 940, true);
            CurrencyByIsoCurrencySymbol[940] = UYI; // Uruguay Peso en Unidades Indexadas (UI)            
            RSD = new Currency("Serbian Dinar", "RSD", 941, false);
            CurrencyByIsoCurrencySymbol[941] = RSD; // Serbian Dinar            
            MZN = new Currency("Mozambique Metical", "MZN", 943, false);
            CurrencyByIsoCurrencySymbol[943] = MZN; // Mozambique Metical            
            AZN = new Currency("Azerbaijan Manat", "AZN", 944, false);
            CurrencyByIsoCurrencySymbol[944] = AZN; // Azerbaijan Manat            
            RON = new Currency("Romanian Leu", "RON", 946, false);
            CurrencyByIsoCurrencySymbol[946] = RON; // Romanian Leu            
            CHE = new Currency("WIR Euro", "CHE", 947, true);
            CurrencyByIsoCurrencySymbol[947] = CHE; // WIR Euro            
            CHW = new Currency("WIR Franc", "CHW", 948, true);
            CurrencyByIsoCurrencySymbol[948] = CHW; // WIR Franc            
            TRY = new Currency("Turkish Lira", "TRY", 949, false);
            CurrencyByIsoCurrencySymbol[949] = TRY; // Turkish Lira            
            XAF = new Currency("CFA Franc BEAC", "XAF", 950, false);
            CurrencyByIsoCurrencySymbol[950] = XAF; // CFA Franc BEAC            
            XCD = new Currency("East Caribbean Dollar", "XCD", 951, false);
            CurrencyByIsoCurrencySymbol[951] = XCD; // East Caribbean Dollar            
            XOF = new Currency("CFA Franc BCEAO", "XOF", 952, false);
            CurrencyByIsoCurrencySymbol[952] = XOF; // CFA Franc BCEAO            
            XPF = new Currency("CFP Franc", "XPF", 953, false);
            CurrencyByIsoCurrencySymbol[953] = XPF; // CFP Franc            
            XBA = new Currency("Bond Markets Unit European Composite Unit (EURCO)", "XBA", 955, false);
            CurrencyByIsoCurrencySymbol[955] = XBA; // Bond Markets Unit European Composite Unit (EURCO)            
            XBB = new Currency("Bond Markets Unit European Monetary Unit (E.M.U.-6)", "XBB", 956, false);
            CurrencyByIsoCurrencySymbol[956] = XBB; // Bond Markets Unit European Monetary Unit (E.M.U.-6)            
            XBC = new Currency("Bond Markets Unit European Unit of Account 9 (E.U.A.-9)", "XBC", 957, false);
            CurrencyByIsoCurrencySymbol[957] =
                XBC; // Bond Markets Unit European Unit of Account 9 (E.U.A.-9)            
            XBD = new Currency("Bond Markets Unit European Unit of Account 17 (E.U.A.-17)", "XBD", 958, false);
            CurrencyByIsoCurrencySymbol[958] =
                XBD; // Bond Markets Unit European Unit of Account 17 (E.U.A.-17)            
            XAU = new Currency("Gold", "XAU", 959, false);
            CurrencyByIsoCurrencySymbol[959] = XAU; // Gold            
            XDR = new Currency("SDR (Special Drawing Right)", "XDR", 960, false);
            CurrencyByIsoCurrencySymbol[960] = XDR; // SDR (Special Drawing Right)            
            XAG = new Currency("Silver", "XAG", 961, false);
            CurrencyByIsoCurrencySymbol[961] = XAG; // Silver            
            XPT = new Currency("Platinum", "XPT", 962, false);
            CurrencyByIsoCurrencySymbol[962] = XPT; // Platinum            
            XTS = new Currency("Codes specifically reserved for testing purposes", "XTS", 963, false);
            CurrencyByIsoCurrencySymbol[963] = XTS; // Codes specifically reserved for testing purposes            
            XPD = new Currency("Palladium", "XPD", 964, false);
            CurrencyByIsoCurrencySymbol[964] = XPD; // Palladium            
            XUA = new Currency("ADB Unit of Account", "XUA", 965, false);
            CurrencyByIsoCurrencySymbol[965] = XUA; // ADB Unit of Account            
            ZMW = new Currency("Zambian Kwacha", "ZMW", 967, false);
            CurrencyByIsoCurrencySymbol[967] = ZMW; // Zambian Kwacha            
            SRD = new Currency("Surinam Dollar", "SRD", 968, false);
            CurrencyByIsoCurrencySymbol[968] = SRD; // Surinam Dollar            
            MGA = new Currency("Malagasy Ariary", "MGA", 969, false);
            CurrencyByIsoCurrencySymbol[969] = MGA; // Malagasy Ariary            
            COU = new Currency("Unidad de Valor Real", "COU", 970, true);
            CurrencyByIsoCurrencySymbol[970] = COU; // Unidad de Valor Real            
            AFN = new Currency("Afghani", "AFN", 971, false);
            CurrencyByIsoCurrencySymbol[971] = AFN; // Afghani            
            TJS = new Currency("Somoni", "TJS", 972, false);
            CurrencyByIsoCurrencySymbol[972] = TJS; // Somoni            
            AOA = new Currency("Kwanza", "AOA", 973, false);
            CurrencyByIsoCurrencySymbol[973] = AOA; // Kwanza            
            BGN = new Currency("Bulgarian Lev", "BGN", 975, false);
            CurrencyByIsoCurrencySymbol[975] = BGN; // Bulgarian Lev            
            CDF = new Currency("Congolese Franc", "CDF", 976, false);
            CurrencyByIsoCurrencySymbol[976] = CDF; // Congolese Franc            
            BAM = new Currency("Convertible Mark", "BAM", 977, false);
            CurrencyByIsoCurrencySymbol[977] = BAM; // Convertible Mark            
            EUR = new Currency("Euro", "EUR", 978, false);
            CurrencyByIsoCurrencySymbol[978] = EUR; // Euro            
            MXV = new Currency("Mexican Unidad de Inversion (UDI)", "MXV", 979, true);
            CurrencyByIsoCurrencySymbol[979] = MXV; // Mexican Unidad de Inversion (UDI)            
            UAH = new Currency("Hryvnia", "UAH", 980, false);
            CurrencyByIsoCurrencySymbol[980] = UAH; // Hryvnia            
            GEL = new Currency("Lari", "GEL", 981, false);
            CurrencyByIsoCurrencySymbol[981] = GEL; // Lari            
            BOV = new Currency("Mvdol", "BOV", 984, true);
            CurrencyByIsoCurrencySymbol[984] = BOV; // Mvdol            
            PLN = new Currency("Zloty", "PLN", 985, false);
            CurrencyByIsoCurrencySymbol[985] = PLN; // Zloty            
            BRL = new Currency("Brazilian Real", "BRL", 986, false);
            CurrencyByIsoCurrencySymbol[986] = BRL; // Brazilian Real            
            CLF = new Currency("Unidad de Fomento", "CLF", 990, true);
            CurrencyByIsoCurrencySymbol[990] = CLF; // Unidad de Fomento            
            XSU = new Currency("Sucre", "XSU", 994, false);
            CurrencyByIsoCurrencySymbol[994] = XSU; // Sucre            
            USN = new Currency("US Dollar (Next day)", "USN", 997, true);
            CurrencyByIsoCurrencySymbol[997] = USN; // US Dollar (Next day)            
            USS = new Currency("US Dollar (Same day)", "USS", 998, true);
            CurrencyByIsoCurrencySymbol[998] = USS; // US Dollar (Same day)            
            XXX = new Currency("The codes assigned for transactions where no currency is involved", "XXX", 999, false);
            CurrencyByIsoCurrencySymbol[999] = XXX; // The codes assigned for transactions where no currency is involved

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
                    isoCurrencySymbols = new List<int>();
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

        /// <summary>
        ///      Try Parse
        ///      by Iso Currency <see cref="Currency" /> Code (e.i. USD, GBP)
        ///      then by Currency <see cref="Currency" /> Symbol (e.i. $, £)
        /// </summary>
        /// <returns>
        ///     The out <see cref="Currency" /> which corresponds
        ///     to the current Iso Currency Code Or Currency Symbol.
        /// </returns>
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

        /// <summary>
        ///     Try Parse by Iso Currency <see cref="Currency" /> Code (e.i. USD, GBP)
        /// </summary>
        /// <returns>
        ///     The out <see cref="Currency" /> which corresponds
        ///     to the current Iso Currency Code.
        /// </returns>
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

        /// <summary>
        ///      Try Parse by Currency <see cref="Currency" /> Symbol (e.i. $, £)
        /// </summary>
        /// <returns>
        ///     The out <see cref="Currency" /> which corresponds
        ///     to the current Currency Symbol.
        /// </returns>
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
                && !isoCurrencySymbols.Contains(isoCurrencySymbol))
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

        /// <summary>
        ///     Creates a <see cref="Currency" /> instance from the
        ///     <see cref="CultureInfo.CurrentCulture" />.
        /// </summary>
        /// <returns>
        ///     The <see cref="Currency" /> which corresponds
        ///     to the current culture.
        /// </returns>
        public static Currency FromCurrentCulture()
        {
            return FromCultureInfo(CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///     Creates a <see cref="Currency" /> instance from the
        ///     given <see cref="CultureInfo" />.
        /// </summary>
        /// <param name="cultureInfo">
        ///     The <see cref="CultureInfo" /> from which to create the currency.
        /// </param>
        /// <returns>
        ///     The <see cref="Currency" /> which corresponds
        ///     to <paramref name="cultureInfo" />.
        /// </returns>
        public static Currency FromCultureInfo(CultureInfo cultureInfo)
        {
            if (IsoCurrencySymbolByLocalCultureId.TryGetValue(cultureInfo.LCID, out var localCultureId)
                && CurrencyByIsoCurrencySymbol.TryGetValue(localCultureId, out var currency))
                return currency;

            throw new InvalidOperationException("Unknown culture: " + cultureInfo);
        }

        /// <summary>
        ///     Creates a <see cref="Currency" /> instance from its
        ///     3-letter ISO 4217 code.
        /// </summary>
        /// <param name="code">The ISO 4217 3-letter currency code.</param>
        /// <returns>
        ///     The <see cref="Currency" /> which corresponds
        ///     to the ISO 4217 3-letter <paramref name="code" />.
        /// </returns>
        public static Currency FromIso3LetterCode(string code)
        {
            return new Currency(code);
        }

        /// <summary>
        ///     Compares two currency values for equality.
        /// </summary>
        /// <param name="left">The left side to compare.</param>
        /// <param name="right">The right side to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if they are equal; <see langword="false" /> otherwise.
        /// </returns>
        public static bool operator ==(Currency left, Currency right)
        {
            return left.Equals(right);
        }

        /// <summary>
        ///     Compares two currency values for inequality.
        /// </summary>
        /// <param name="left">The left side to compare.</param>
        /// <param name="right">The right side to compare.</param>
        /// <returns>
        ///     <see langword="true" /> if they are not equal; <see langword="false" /> otherwise.
        /// </returns>
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

        #region IEquatable<Currency> Members

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
        public static string GetValueOrDefault(this IDictionary<string, string> table, string key)
        {
            string value;

            return !table.TryGetValue(key, out value) ? null : value;
        }

        public static RegionInfo GetValueOrDefault(this IDictionary<string, RegionInfo> table, string key)
        {
            RegionInfo value;

            return !table.TryGetValue(key, out value) ? null : value;
        }
    }
}