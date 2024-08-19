using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace System
{
    /// <summary>
    ///     Represents a system of money within which <see cref="Money" />
    ///     amounts can be compared and arithmetic can be performed.
    /// </summary>
    [Serializable]
    public struct Currency : IEquatable<Currency>, IFormatProvider
    {
        #region Static Currency Fields

        public static readonly Currency None = new Currency(0);

        /// <summary>
        ///     Lek
        /// </summary>
        public static readonly Currency All;

        /// <summary>
        ///     Algerian Dinar
        /// </summary>
        public static readonly Currency Dzd;

        /// <summary>
        ///     Argentine Peso
        /// </summary>
        public static readonly Currency Ars;

        /// <summary>
        ///     Australian Dollar
        /// </summary>
        public static readonly Currency Aud;

        /// <summary>
        ///     Bahamian Dollar
        /// </summary>
        public static readonly Currency Bsd;

        /// <summary>
        ///     Bahraini Dinar
        /// </summary>
        public static readonly Currency Bhd;

        /// <summary>
        ///     Taka
        /// </summary>
        public static readonly Currency Bdt;

        /// <summary>
        ///     Armenian Dram
        /// </summary>
        public static readonly Currency Amd;

        /// <summary>
        ///     Barbados Dollar
        /// </summary>
        public static readonly Currency Bbd;

        /// <summary>
        ///     Bermudian Dollar
        ///     (customarily known as
        ///     Bermuda Dollar)
        /// </summary>
        public static readonly Currency Bmd;

        /// <summary>
        ///     Boliviano
        /// </summary>
        public static readonly Currency Bob;

        /// <summary>
        ///     Pula
        /// </summary>
        public static readonly Currency Bwp;

        /// <summary>
        ///     Belize Dollar
        /// </summary>
        public static readonly Currency Bzd;

        /// <summary>
        ///     Solomon Islands Dollar
        /// </summary>
        public static readonly Currency Sbd;

        /// <summary>
        ///     Brunei Dollar
        /// </summary>
        public static readonly Currency Bnd;

        /// <summary>
        ///     Kyat
        /// </summary>
        public static readonly Currency Mmk;

        /// <summary>
        ///     Burundi Franc
        /// </summary>
        public static readonly Currency Bif;

        /// <summary>
        ///     Riel
        /// </summary>
        public static readonly Currency Khr;

        /// <summary>
        ///     Canadian Dollar
        /// </summary>
        public static readonly Currency Cad;

        /// <summary>
        ///     Cape Verde Escudo
        /// </summary>
        public static readonly Currency Cve;

        /// <summary>
        ///     Cayman Islands Dollar
        /// </summary>
        public static readonly Currency Kyd;

        /// <summary>
        ///     Sri Lanka Rupee
        /// </summary>
        public static readonly Currency Lkr;

        /// <summary>
        ///     Chilean Peso
        /// </summary>
        public static readonly Currency Clp;

        /// <summary>
        ///     Yuan Renminbi
        /// </summary>
        public static readonly Currency Cny;

        /// <summary>
        ///     Colombian Peso
        /// </summary>
        public static readonly Currency Cop;

        /// <summary>
        ///     Comoro Franc
        /// </summary>
        public static readonly Currency Kmf;

        /// <summary>
        ///     Costa Rican Colon
        /// </summary>
        public static readonly Currency Crc;

        /// <summary>
        ///     Croatian Kuna
        /// </summary>
        public static readonly Currency Hrk;

        /// <summary>
        ///     Cuban Peso
        /// </summary>
        public static readonly Currency Cup;

        /// <summary>
        ///     Czech Koruna
        /// </summary>
        public static readonly Currency Czk;

        /// <summary>
        ///     Danish Krone
        /// </summary>
        public static readonly Currency Dkk;

        /// <summary>
        ///     Dominican Peso
        /// </summary>
        public static readonly Currency Dop;

        /// <summary>
        ///     El Salvador Colon
        /// </summary>
        public static readonly Currency Svc;

        /// <summary>
        ///     Ethiopian Birr
        /// </summary>
        public static readonly Currency Etb;

        /// <summary>
        ///     Nakfa
        /// </summary>
        public static readonly Currency Ern;

        /// <summary>
        ///     Kroon
        /// </summary>
        public static readonly Currency Eek;

        /// <summary>
        ///     Falkland Islands Pound
        /// </summary>
        public static readonly Currency Fkp;

        /// <summary>
        ///     Fiji Dollar
        /// </summary>
        public static readonly Currency Fjd;

        /// <summary>
        ///     Djibouti Franc
        /// </summary>
        public static readonly Currency Djf;

        /// <summary>
        ///     Dalasi
        /// </summary>
        public static readonly Currency Gmd;

        /// <summary>
        ///     Gibraltar Pound
        /// </summary>
        public static readonly Currency Gip;

        /// <summary>
        ///     Quetzal
        /// </summary>
        public static readonly Currency Gtq;

        /// <summary>
        ///     Guinea Franc
        /// </summary>
        public static readonly Currency Gnf;

        /// <summary>
        ///     Guyana Dollar
        /// </summary>
        public static readonly Currency Gyd;

        /// <summary>
        ///     Gourde
        /// </summary>
        public static readonly Currency Htg;

        /// <summary>
        ///     Lempira
        /// </summary>
        public static readonly Currency Hnl;

        /// <summary>
        ///     Hong Kong Dollar
        /// </summary>
        public static readonly Currency Hkd;

        /// <summary>
        ///     Forint
        /// </summary>
        public static readonly Currency Huf;

        /// <summary>
        ///     Iceland Krona
        /// </summary>
        public static readonly Currency Isk;

        /// <summary>
        ///     Indian Rupee
        /// </summary>
        public static readonly Currency Inr;

        /// <summary>
        ///     Rupiah
        /// </summary>
        public static readonly Currency Idr;

        /// <summary>
        ///     Iranian Rial
        /// </summary>
        public static readonly Currency Irr;

        /// <summary>
        ///     Iraqi Dinar
        /// </summary>
        public static readonly Currency Iqd;

        /// <summary>
        ///     New Israeli Sheqel
        /// </summary>
        public static readonly Currency Ils;

        /// <summary>
        ///     Jamaican Dollar
        /// </summary>
        public static readonly Currency Jmd;

        /// <summary>
        ///     Yen
        /// </summary>
        public static readonly Currency Jpy;

        /// <summary>
        ///     Tenge
        /// </summary>
        public static readonly Currency Kzt;

        /// <summary>
        ///     Jordanian Dinar
        /// </summary>
        public static readonly Currency Jod;

        /// <summary>
        ///     Kenyan Shilling
        /// </summary>
        public static readonly Currency Kes;

        /// <summary>
        ///     North Korean Won
        /// </summary>
        public static readonly Currency Kpw;

        /// <summary>
        ///     Won
        /// </summary>
        public static readonly Currency Krw;

        /// <summary>
        ///     Kuwaiti Dinar
        /// </summary>
        public static readonly Currency Kwd;

        /// <summary>
        ///     Som
        /// </summary>
        public static readonly Currency Kgs;

        /// <summary>
        ///     Kip
        /// </summary>
        public static readonly Currency Lak;

        /// <summary>
        ///     Lebanese Pound
        /// </summary>
        public static readonly Currency Lbp;

        /// <summary>
        ///     Latvian Lats
        /// </summary>
        public static readonly Currency Lvl;

        /// <summary>
        ///     Liberian Dollar
        /// </summary>
        public static readonly Currency Lrd;

        /// <summary>
        ///     Libyan Dinar
        /// </summary>
        public static readonly Currency Lyd;

        /// <summary>
        ///     Lithuanian Litas
        /// </summary>
        public static readonly Currency Ltl;

        /// <summary>
        ///     Pataca
        /// </summary>
        public static readonly Currency Mop;

        /// <summary>
        ///     Kwacha
        /// </summary>
        public static readonly Currency Mwk;

        /// <summary>
        ///     Malaysian Ringgit
        /// </summary>
        public static readonly Currency Myr;

        /// <summary>
        ///     Rufiyaa
        /// </summary>
        public static readonly Currency Mvr;

        /// <summary>
        ///     Ouguiya
        /// </summary>
        public static readonly Currency Mro;

        /// <summary>
        ///     Mauritius Rupee
        /// </summary>
        public static readonly Currency Mur;

        /// <summary>
        ///     Mexican Peso
        /// </summary>
        public static readonly Currency Mxn;

        /// <summary>
        ///     Tugrik
        /// </summary>
        public static readonly Currency Mnt;

        /// <summary>
        ///     Moldovan Leu
        /// </summary>
        public static readonly Currency Mdl;

        /// <summary>
        ///     Moroccan Dirham
        /// </summary>
        public static readonly Currency Mad;

        /// <summary>
        ///     Rial Omani
        /// </summary>
        public static readonly Currency Omr;

        /// <summary>
        ///     Nepalese Rupee
        /// </summary>
        public static readonly Currency Npr;

        /// <summary>
        ///     Netherlands Antillian Guilder
        /// </summary>
        public static readonly Currency Ang;

        /// <summary>
        ///     Aruban Guilder
        /// </summary>
        public static readonly Currency Awg;

        /// <summary>
        ///     Vatu
        /// </summary>
        public static readonly Currency Vuv;

        /// <summary>
        ///     New Zealand Dollar
        /// </summary>
        public static readonly Currency Nzd;

        /// <summary>
        ///     Cordoba Oro
        /// </summary>
        public static readonly Currency Nio;

        /// <summary>
        ///     Naira
        /// </summary>
        public static readonly Currency Ngn;

        /// <summary>
        ///     Norwegian Krone
        /// </summary>
        public static readonly Currency Nok;

        /// <summary>
        ///     Pakistan Rupee
        /// </summary>
        public static readonly Currency Pkr;

        /// <summary>
        ///     Balboa
        /// </summary>
        public static readonly Currency Pab;

        /// <summary>
        ///     Kina
        /// </summary>
        public static readonly Currency Pgk;

        /// <summary>
        ///     Guarani
        /// </summary>
        public static readonly Currency Pyg;

        /// <summary>
        ///     Nuevo Sol
        /// </summary>
        public static readonly Currency Pen;

        /// <summary>
        ///     Philippine Peso
        /// </summary>
        public static readonly Currency Php;

        /// <summary>
        ///     Guinea-Bissau Peso
        /// </summary>
        public static readonly Currency Gwp;

        /// <summary>
        ///     Qatari Rial
        /// </summary>
        public static readonly Currency Qar;

        /// <summary>
        ///     Russian Ruble
        /// </summary>
        public static readonly Currency Rub;

        /// <summary>
        ///     Rwanda Franc
        /// </summary>
        public static readonly Currency Rwf;

        /// <summary>
        ///     Saint Helena Pound
        /// </summary>
        public static readonly Currency Shp;

        /// <summary>
        ///     Dobra
        /// </summary>
        public static readonly Currency Std;

        /// <summary>
        ///     Saudi Riyal
        /// </summary>
        public static readonly Currency Sar;

        /// <summary>
        ///     Seychelles Rupee
        /// </summary>
        public static readonly Currency Scr;

        /// <summary>
        ///     Leone
        /// </summary>
        public static readonly Currency Sll;

        /// <summary>
        ///     Singapore Dollar
        /// </summary>
        public static readonly Currency Sgd;

        /// <summary>
        ///     Slovak Koruna
        /// </summary>
        public static readonly Currency Skk;

        /// <summary>
        ///     Dong
        /// </summary>
        public static readonly Currency Vnd;

        /// <summary>
        ///     Somali Shilling
        /// </summary>
        public static readonly Currency Sos;

        /// <summary>
        ///     Rand
        /// </summary>
        public static readonly Currency Zar;

        /// <summary>
        ///     Zimbabwe Dollar
        /// </summary>
        public static readonly Currency Zwd;

        /// <summary>
        ///     Lilangeni
        /// </summary>
        public static readonly Currency Szl;

        /// <summary>
        ///     Swedish Krona
        /// </summary>
        public static readonly Currency Sek;

        /// <summary>
        ///     Swiss Franc
        /// </summary>
        public static readonly Currency Chf;

        /// <summary>
        ///     Syrian Pound
        /// </summary>
        public static readonly Currency Syp;

        /// <summary>
        ///     Baht
        /// </summary>
        public static readonly Currency Thb;

        /// <summary>
        ///     Pa'anga
        /// </summary>
        public static readonly Currency Top;

        /// <summary>
        ///     Trinidad and Tobago
        ///     Dollar
        /// </summary>
        public static readonly Currency Ttd;

        /// <summary>
        ///     UAE Dirham
        /// </summary>
        public static readonly Currency Aed;

        /// <summary>
        ///     Tunisian Dinar
        /// </summary>
        public static readonly Currency Tnd;

        /// <summary>
        ///     Manat
        /// </summary>
        public static readonly Currency Tmm;

        /// <summary>
        ///     Uganda Shilling
        /// </summary>
        public static readonly Currency Ugx;

        /// <summary>
        ///     Denar
        /// </summary>
        public static readonly Currency Mkd;

        /// <summary>
        ///     Egyptian Pound
        /// </summary>
        public static readonly Currency Egp;

        /// <summary>
        ///     Pound Sterling
        /// </summary>
        public static readonly Currency Gbp;

        /// <summary>
        ///     Tanzanian Shilling
        /// </summary>
        public static readonly Currency Tzs;

        /// <summary>
        ///     US Dollar
        /// </summary>
        public static readonly Currency Usd;

        /// <summary>
        ///     Peso Uruguayo
        /// </summary>
        public static readonly Currency Uyu;

        /// <summary>
        ///     Uzbekistan Sum
        /// </summary>
        public static readonly Currency Uzs;

        /// <summary>
        ///     Tala
        /// </summary>
        public static readonly Currency Wst;

        /// <summary>
        ///     Yemeni Rial
        /// </summary>
        public static readonly Currency Yer;

        /// <summary>
        ///     Kwacha
        /// </summary>
        public static readonly Currency Zmk;

        /// <summary>
        ///     New Taiwan Dollar
        /// </summary>
        public static readonly Currency Twd;

        /// <summary>
        ///     Ghana Cedi
        /// </summary>
        public static readonly Currency Ghs;

        /// <summary>
        ///     Bolivar Fuerte
        /// </summary>
        public static readonly Currency Vef;

        /// <summary>
        ///     Sudanese Pound
        /// </summary>
        public static readonly Currency Sdg;

        /// <summary>
        ///     Serbian Dinar
        /// </summary>
        public static readonly Currency Rsd;

        /// <summary>
        ///     Metical
        /// </summary>
        public static readonly Currency Mzn;

        /// <summary>
        ///     Azerbaijanian Manat
        /// </summary>
        public static readonly Currency Azn;

        /// <summary>
        ///     New Leu
        /// </summary>
        public static readonly Currency Ron;

        /// <summary>
        ///     New Turkish Lira
        /// </summary>
        public static readonly Currency Try;

        /// <summary>
        ///     CFA Franc BEAC
        /// </summary>
        public static readonly Currency Xaf;

        /// <summary>
        ///     East Caribbean Dollar
        /// </summary>
        public static readonly Currency Xcd;

        /// <summary>
        ///     CFA Franc BCEAO
        /// </summary>
        public static readonly Currency Xof;

        /// <summary>
        ///     CFP Franc
        /// </summary>
        public static readonly Currency Xpf;

        /// <summary>
        ///     Bond Markets Units
        ///     European Composite Unit
        ///     (EURCO)
        /// </summary>
        public static readonly Currency Xba;

        /// <summary>
        ///     European Monetary
        ///     Unit (E.M.U.-6)
        /// </summary>
        public static readonly Currency Xbb;

        /// <summary>
        ///     European Unit of
        ///     Account 9(E.U.A.-9)
        /// </summary>
        public static readonly Currency Xbc;

        /// <summary>
        ///     European Unit of
        ///     Account 17(E.U.A.-17)
        /// </summary>
        public static readonly Currency Xbd;

        /// <summary>
        ///     Gold
        /// </summary>
        public static readonly Currency Xau;

        /// <summary>
        ///     SDR
        /// </summary>
        public static readonly Currency Xdr;

        /// <summary>
        ///     Silver
        /// </summary>
        public static readonly Currency Xag;

        /// <summary>
        ///     Platinum
        /// </summary>
        public static readonly Currency Xpt;

        /// <summary>
        ///     Codes specifically
        ///     reserved for testing
        ///     purposes
        /// </summary>
        public static readonly Currency Xts;

        /// <summary>
        ///     Palladium
        /// </summary>
        public static readonly Currency Xpd;

        /// <summary>
        ///     Surinam Dollar
        /// </summary>
        public static readonly Currency Srd;

        /// <summary>
        ///     Malagasy Ariary
        /// </summary>
        public static readonly Currency Mga;

        /// <summary>
        ///     Afghani
        /// </summary>
        public static readonly Currency Afn;

        /// <summary>
        ///     Somoni
        /// </summary>
        public static readonly Currency Tjs;

        /// <summary>
        ///     Kwanza
        /// </summary>
        public static readonly Currency Aoa;

        /// <summary>
        ///     Belarussian Ruble
        /// </summary>
        public static readonly Currency Byr;

        /// <summary>
        ///     Bulgarian Lev
        /// </summary>
        public static readonly Currency Bgn;

        /// <summary>
        ///     Franc Congolais
        /// </summary>
        public static readonly Currency Cdf;

        /// <summary>
        ///     Convertible Marks
        /// </summary>
        public static readonly Currency Bam;

        /// <summary>
        ///     Euro
        /// </summary>
        public static readonly Currency Eur;

        /// <summary>
        ///     Hryvnia
        /// </summary>
        public static readonly Currency Uah;

        /// <summary>
        ///     Lari
        /// </summary>
        public static readonly Currency Gel;

        /// <summary>
        ///     Zloty
        /// </summary>
        public static readonly Currency Pln;

        /// <summary>
        ///     Brazilian Real
        /// </summary>
        public static readonly Currency Brl;

        /// <summary>
        ///     The codes assigned for
        ///     transactions where no
        ///     currency is involved.
        /// </summary>
        public static readonly Currency Xxx;

        #endregion

        public static readonly Dictionary<int, CurrencyInfo> CurrencyInfoByIsoCurrencySymbol
            = new Dictionary<int, CurrencyInfo>();

        public static readonly Dictionary<string, int> IsoCurrencySymbolByIsoCurrencyCode
            = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);

        public static readonly Dictionary<string, List<int>> IsoCurrencySymbolsByCurrencySymbol
            = new Dictionary<string, List<int>>(StringComparer.InvariantCultureIgnoreCase);

        public static readonly Dictionary<int, int> IsoCurrencySymbolByLocalCultureId
            = new Dictionary<int, int>();

        public static readonly Dictionary<int, List<int>> LocalCultureIdsByIsoCurrencySymbol
            = new Dictionary<int, List<int>>();

        private readonly int _isoCurrencyCode;

        public static readonly Dictionary<string, List<int>> LocalCultureIdsByIsoCurrencyCode =
            new Dictionary<string, List<int>>();

        public static readonly Dictionary<string, string> CurrencySymbolByIsoCurrencySymbol =
            new Dictionary<string, string>();

        public static readonly Dictionary<string, RegionInfo> RegionInfoByIsoCurrencySymbol =
            new Dictionary<string, RegionInfo>();

        public static readonly Dictionary<int, RegionInfo> RegionInfoByLocalCultureId =
            new Dictionary<int, RegionInfo>();

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


            CurrencyInfoByIsoCurrencySymbol[840] = new CurrencyInfo("US Dollar", "USD", 840);
            CurrencyInfoByIsoCurrencySymbol[008] = new CurrencyInfo("Lek", "ALL", 008);
            CurrencyInfoByIsoCurrencySymbol[012] = new CurrencyInfo("Algerian Dinar", "DZD", 012);
            CurrencyInfoByIsoCurrencySymbol[032] = new CurrencyInfo("Argentine Peso", "ARS", 032);
            CurrencyInfoByIsoCurrencySymbol[036] = new CurrencyInfo("Australian Dollar", "AUD", 036);
            CurrencyInfoByIsoCurrencySymbol[044] = new CurrencyInfo("Bahamian Dollar", "BSD", 044);
            CurrencyInfoByIsoCurrencySymbol[048] = new CurrencyInfo("Bahraini Dinar", "BHD", 048);
            CurrencyInfoByIsoCurrencySymbol[050] = new CurrencyInfo("Taka", "BDT", 050);
            CurrencyInfoByIsoCurrencySymbol[051] = new CurrencyInfo("Armenian Dram", "AMD", 051);
            CurrencyInfoByIsoCurrencySymbol[052] = new CurrencyInfo("Barbados Dollar", "BBD", 052);
            CurrencyInfoByIsoCurrencySymbol[060] =
                new CurrencyInfo("Bermudian Dollar (customarily known as Bermuda Dollar)", "BMD", 060);
            CurrencyInfoByIsoCurrencySymbol[068] = new CurrencyInfo("Boliviano", "BOB", 068);
            CurrencyInfoByIsoCurrencySymbol[072] = new CurrencyInfo("Pula", "BWP", 072);
            CurrencyInfoByIsoCurrencySymbol[084] = new CurrencyInfo("Belize Dollar", "BZD", 084);
            CurrencyInfoByIsoCurrencySymbol[090] = new CurrencyInfo("Solomon Islands Dollar", "SBD", 090);
            CurrencyInfoByIsoCurrencySymbol[096] = new CurrencyInfo("Brunei Dollar", "BND", 096);
            CurrencyInfoByIsoCurrencySymbol[104] = new CurrencyInfo("Kyat", "MMK", 104);
            CurrencyInfoByIsoCurrencySymbol[108] = new CurrencyInfo("Burundi Franc", "BIF", 108);
            CurrencyInfoByIsoCurrencySymbol[116] = new CurrencyInfo("Riel", "KHR", 116);
            CurrencyInfoByIsoCurrencySymbol[124] = new CurrencyInfo("Canadian Dollar", "CAD", 124);
            CurrencyInfoByIsoCurrencySymbol[132] = new CurrencyInfo("Cape Verde Escudo", "CVE", 132);
            CurrencyInfoByIsoCurrencySymbol[136] = new CurrencyInfo("Cayman Islands Dollar", "KYD", 136);
            CurrencyInfoByIsoCurrencySymbol[144] = new CurrencyInfo("Sri Lanka Rupee", "LKR", 144);
            CurrencyInfoByIsoCurrencySymbol[152] = new CurrencyInfo("Chilean Peso", "CLP", 152);
            CurrencyInfoByIsoCurrencySymbol[156] = new CurrencyInfo("Yuan Renminbi", "CNY", 156);
            CurrencyInfoByIsoCurrencySymbol[170] = new CurrencyInfo("Colombian Peso", "COP", 170);
            CurrencyInfoByIsoCurrencySymbol[174] = new CurrencyInfo("Comoro Franc", "KMF", 174);
            CurrencyInfoByIsoCurrencySymbol[188] = new CurrencyInfo("Costa Rican Colon", "CRC", 188);
            CurrencyInfoByIsoCurrencySymbol[191] = new CurrencyInfo("Croatian Kuna", "HRK", 191);
            CurrencyInfoByIsoCurrencySymbol[192] = new CurrencyInfo("Cuban Peso", "CUP", 192);
            CurrencyInfoByIsoCurrencySymbol[203] = new CurrencyInfo("Czech Koruna", "CZK", 203);
            CurrencyInfoByIsoCurrencySymbol[208] = new CurrencyInfo("Danish Krone", "DKK", 208);
            CurrencyInfoByIsoCurrencySymbol[214] = new CurrencyInfo("Dominican Peso", "DOP", 214);
            CurrencyInfoByIsoCurrencySymbol[222] = new CurrencyInfo("El Salvador Colon", "SVC", 222);
            CurrencyInfoByIsoCurrencySymbol[230] = new CurrencyInfo("Ethiopian Birr", "ETB", 230);
            CurrencyInfoByIsoCurrencySymbol[232] = new CurrencyInfo("Nakfa", "ERN", 232);
            CurrencyInfoByIsoCurrencySymbol[233] = new CurrencyInfo("Kroon", "EEK", 233);
            CurrencyInfoByIsoCurrencySymbol[238] = new CurrencyInfo("Falkland Islands Pound", "FKP", 238);
            CurrencyInfoByIsoCurrencySymbol[242] = new CurrencyInfo("Fiji Dollar", "FJD", 242);
            CurrencyInfoByIsoCurrencySymbol[262] = new CurrencyInfo("Djibouti Franc", "DJF", 262);
            CurrencyInfoByIsoCurrencySymbol[270] = new CurrencyInfo("Dalasi", "GMD", 270);
            CurrencyInfoByIsoCurrencySymbol[292] = new CurrencyInfo("Gibraltar Pound", "GIP", 292);
            CurrencyInfoByIsoCurrencySymbol[320] = new CurrencyInfo("Quetzal", "GTQ", 320);
            CurrencyInfoByIsoCurrencySymbol[324] = new CurrencyInfo("Guinea Franc", "GNF", 324);
            CurrencyInfoByIsoCurrencySymbol[328] = new CurrencyInfo("Guyana Dollar", "GYD", 328);
            CurrencyInfoByIsoCurrencySymbol[332] = new CurrencyInfo("Gourde", "HTG", 332);
            CurrencyInfoByIsoCurrencySymbol[340] = new CurrencyInfo("Lempira", "HNL", 340);
            CurrencyInfoByIsoCurrencySymbol[344] = new CurrencyInfo("Hong Kong Dollar", "HKD", 344);
            CurrencyInfoByIsoCurrencySymbol[348] = new CurrencyInfo("Forint", "HUF", 348);
            CurrencyInfoByIsoCurrencySymbol[352] = new CurrencyInfo("Iceland Krona", "ISK", 352);
            CurrencyInfoByIsoCurrencySymbol[356] = new CurrencyInfo("Indian Rupee", "INR", 356);
            CurrencyInfoByIsoCurrencySymbol[360] = new CurrencyInfo("Rupiah", "IDR", 360);
            CurrencyInfoByIsoCurrencySymbol[364] = new CurrencyInfo("Iranian Rial", "IRR", 364);
            CurrencyInfoByIsoCurrencySymbol[368] = new CurrencyInfo("Iraqi Dinar", "IQD", 368);
            CurrencyInfoByIsoCurrencySymbol[376] = new CurrencyInfo("New Israeli Sheqel", "ILS", 376);
            CurrencyInfoByIsoCurrencySymbol[388] = new CurrencyInfo("Jamaican Dollar", "JMD", 388);
            CurrencyInfoByIsoCurrencySymbol[392] = new CurrencyInfo("Yen", "JPY", 392);
            CurrencyInfoByIsoCurrencySymbol[398] = new CurrencyInfo("Tenge", "KZT", 398);
            CurrencyInfoByIsoCurrencySymbol[400] = new CurrencyInfo("Jordanian Dinar", "JOD", 400);
            CurrencyInfoByIsoCurrencySymbol[404] = new CurrencyInfo("Kenyan Shilling", "KES", 404);
            CurrencyInfoByIsoCurrencySymbol[408] = new CurrencyInfo("North Korean Won", "KPW", 408);
            CurrencyInfoByIsoCurrencySymbol[410] = new CurrencyInfo("Won", "KRW", 410);
            CurrencyInfoByIsoCurrencySymbol[414] = new CurrencyInfo("Kuwaiti Dinar", "KWD", 414);
            CurrencyInfoByIsoCurrencySymbol[417] = new CurrencyInfo("Som", "KGS", 417);
            CurrencyInfoByIsoCurrencySymbol[418] = new CurrencyInfo("Kip", "LAK", 418);
            CurrencyInfoByIsoCurrencySymbol[422] = new CurrencyInfo("Lebanese Pound", "LBP", 422);
            CurrencyInfoByIsoCurrencySymbol[428] = new CurrencyInfo("Latvian Lats", "LVL", 428);
            CurrencyInfoByIsoCurrencySymbol[430] = new CurrencyInfo("Liberian Dollar", "LRD", 430);
            CurrencyInfoByIsoCurrencySymbol[434] = new CurrencyInfo("Libyan Dinar", "LYD", 434);
            CurrencyInfoByIsoCurrencySymbol[440] = new CurrencyInfo("Lithuanian Litas", "LTL", 440);
            CurrencyInfoByIsoCurrencySymbol[446] = new CurrencyInfo("Pataca", "MOP", 446);
            CurrencyInfoByIsoCurrencySymbol[454] = new CurrencyInfo("Kwacha", "MWK", 454);
            CurrencyInfoByIsoCurrencySymbol[458] = new CurrencyInfo("Malaysian Ringgit", "MYR", 458);
            CurrencyInfoByIsoCurrencySymbol[462] = new CurrencyInfo("Rufiyaa", "MVR", 462);
            CurrencyInfoByIsoCurrencySymbol[478] = new CurrencyInfo("Ouguiya", "MRO", 478);
            CurrencyInfoByIsoCurrencySymbol[480] = new CurrencyInfo("Mauritius Rupee", "MUR", 480);
            CurrencyInfoByIsoCurrencySymbol[484] = new CurrencyInfo("Mexican Peso", "MXN", 484);
            CurrencyInfoByIsoCurrencySymbol[496] = new CurrencyInfo("Tugrik", "MNT", 496);
            CurrencyInfoByIsoCurrencySymbol[498] = new CurrencyInfo("Moldovan Leu", "MDL", 498);
            CurrencyInfoByIsoCurrencySymbol[504] = new CurrencyInfo("Moroccan Dirham", "MAD", 504);
            CurrencyInfoByIsoCurrencySymbol[512] = new CurrencyInfo("Rial Omani", "OMR", 512);
            CurrencyInfoByIsoCurrencySymbol[524] = new CurrencyInfo("Nepalese Rupee", "NPR", 524);
            CurrencyInfoByIsoCurrencySymbol[532] = new CurrencyInfo("Netherlands Antillian Guilder", "ANG", 532);
            CurrencyInfoByIsoCurrencySymbol[533] = new CurrencyInfo("Aruban Guilder", "AWG", 533);
            CurrencyInfoByIsoCurrencySymbol[548] = new CurrencyInfo("Vatu", "VUV", 548);
            CurrencyInfoByIsoCurrencySymbol[554] = new CurrencyInfo("New Zealand Dollar", "NZD", 554);
            CurrencyInfoByIsoCurrencySymbol[558] = new CurrencyInfo("Cordoba Oro", "NIO", 558);
            CurrencyInfoByIsoCurrencySymbol[566] = new CurrencyInfo("Naira", "NGN", 566);
            CurrencyInfoByIsoCurrencySymbol[578] = new CurrencyInfo("Norwegian Krone", "NOK", 578);
            CurrencyInfoByIsoCurrencySymbol[586] = new CurrencyInfo("Pakistan Rupee", "PKR", 586);
            CurrencyInfoByIsoCurrencySymbol[590] = new CurrencyInfo("Balboa", "PAB", 590);
            CurrencyInfoByIsoCurrencySymbol[598] = new CurrencyInfo("Kina", "PGK", 598);
            CurrencyInfoByIsoCurrencySymbol[600] = new CurrencyInfo("Guarani", "PYG", 600);
            CurrencyInfoByIsoCurrencySymbol[604] = new CurrencyInfo("Nuevo Sol", "PEN", 604);
            CurrencyInfoByIsoCurrencySymbol[608] = new CurrencyInfo("Philippine Peso", "PHP", 608);
            CurrencyInfoByIsoCurrencySymbol[624] = new CurrencyInfo("Guinea-Bissau Peso", "GWP", 624);
            CurrencyInfoByIsoCurrencySymbol[634] = new CurrencyInfo("Qatari Rial", "QAR", 634);
            CurrencyInfoByIsoCurrencySymbol[643] = new CurrencyInfo("Russian Ruble", "RUB", 643);
            CurrencyInfoByIsoCurrencySymbol[646] = new CurrencyInfo("Rwanda Franc", "RWF", 646);
            CurrencyInfoByIsoCurrencySymbol[654] = new CurrencyInfo("Saint Helena Pound", "SHP", 654);
            CurrencyInfoByIsoCurrencySymbol[678] = new CurrencyInfo("Dobra", "STD", 678);
            CurrencyInfoByIsoCurrencySymbol[682] = new CurrencyInfo("Saudi Riyal", "SAR", 682);
            CurrencyInfoByIsoCurrencySymbol[690] = new CurrencyInfo("Seychelles Rupee", "SCR", 690);
            CurrencyInfoByIsoCurrencySymbol[694] = new CurrencyInfo("Leone", "SLL", 694);
            CurrencyInfoByIsoCurrencySymbol[702] = new CurrencyInfo("Singapore Dollar", "SGD", 702);
            CurrencyInfoByIsoCurrencySymbol[703] = new CurrencyInfo("Slovak Koruna", "SKK", 703);
            CurrencyInfoByIsoCurrencySymbol[704] = new CurrencyInfo("Dong", "VND", 704);
            CurrencyInfoByIsoCurrencySymbol[706] = new CurrencyInfo("Somali Shilling", "SOS", 706);
            CurrencyInfoByIsoCurrencySymbol[710] = new CurrencyInfo("Rand", "ZAR", 710);
            CurrencyInfoByIsoCurrencySymbol[716] = new CurrencyInfo("Zimbabwe Dollar", "ZWD", 716);
            CurrencyInfoByIsoCurrencySymbol[748] = new CurrencyInfo("Lilangeni", "SZL", 748);
            CurrencyInfoByIsoCurrencySymbol[752] = new CurrencyInfo("Swedish Krona", "SEK", 752);
            CurrencyInfoByIsoCurrencySymbol[756] = new CurrencyInfo("Swiss Franc", "CHF", 756);
            CurrencyInfoByIsoCurrencySymbol[760] = new CurrencyInfo("Syrian Pound", "SYP", 760);
            CurrencyInfoByIsoCurrencySymbol[764] = new CurrencyInfo("Baht", "THB", 764);
            CurrencyInfoByIsoCurrencySymbol[776] = new CurrencyInfo("Pa'anga", "TOP", 776);
            CurrencyInfoByIsoCurrencySymbol[780] = new CurrencyInfo("Trinidad and Tobago Dollar", "TTD", 780);
            CurrencyInfoByIsoCurrencySymbol[784] = new CurrencyInfo("UAE Dirham", "AED", 784);
            CurrencyInfoByIsoCurrencySymbol[788] = new CurrencyInfo("Tunisian Dinar", "TND", 788);
            CurrencyInfoByIsoCurrencySymbol[795] = new CurrencyInfo("Manat", "TMM", 795);
            CurrencyInfoByIsoCurrencySymbol[800] = new CurrencyInfo("Uganda Shilling", "UGX", 800);
            CurrencyInfoByIsoCurrencySymbol[807] = new CurrencyInfo("Denar", "MKD", 807);
            CurrencyInfoByIsoCurrencySymbol[818] = new CurrencyInfo("Egyptian Pound", "EGP", 818);
            CurrencyInfoByIsoCurrencySymbol[826] = new CurrencyInfo("Pound Sterling", "GBP", 826);
            CurrencyInfoByIsoCurrencySymbol[834] = new CurrencyInfo("Tanzanian Shilling", "TZS", 834);
            CurrencyInfoByIsoCurrencySymbol[858] = new CurrencyInfo("Peso Uruguayo", "UYU", 858);
            CurrencyInfoByIsoCurrencySymbol[860] = new CurrencyInfo("Uzbekistan Sum", "UZS", 860);
            CurrencyInfoByIsoCurrencySymbol[882] = new CurrencyInfo("Tala", "WST", 882);
            CurrencyInfoByIsoCurrencySymbol[886] = new CurrencyInfo("Yemeni Rial", "YER", 886);
            CurrencyInfoByIsoCurrencySymbol[894] = new CurrencyInfo("Kwacha", "ZMK", 894);
            CurrencyInfoByIsoCurrencySymbol[901] = new CurrencyInfo("New Taiwan Dollar", "TWD", 901);
            CurrencyInfoByIsoCurrencySymbol[936] = new CurrencyInfo("Ghana Cedi", "GHS", 936);
            CurrencyInfoByIsoCurrencySymbol[937] = new CurrencyInfo("Bolivar Fuerte", "VEF", 937);
            CurrencyInfoByIsoCurrencySymbol[938] = new CurrencyInfo("Sudanese Pound", "SDG", 938);
            CurrencyInfoByIsoCurrencySymbol[941] = new CurrencyInfo("Serbian Dinar", "RSD", 941);
            CurrencyInfoByIsoCurrencySymbol[943] = new CurrencyInfo("Metical", "MZN", 943);
            CurrencyInfoByIsoCurrencySymbol[944] = new CurrencyInfo("Azerbaijanian Manat", "AZN", 944);
            CurrencyInfoByIsoCurrencySymbol[946] = new CurrencyInfo("New Leu", "RON", 946);
            CurrencyInfoByIsoCurrencySymbol[949] = new CurrencyInfo("New Turkish Lira", "TRY", 949);
            CurrencyInfoByIsoCurrencySymbol[950] = new CurrencyInfo("CFA Franc BEAC", "XAF", 950);
            CurrencyInfoByIsoCurrencySymbol[951] = new CurrencyInfo("East Caribbean Dollar", "XCD", 951);
            CurrencyInfoByIsoCurrencySymbol[952] = new CurrencyInfo("CFA Franc BCEAO", "XOF", 952);
            CurrencyInfoByIsoCurrencySymbol[953] = new CurrencyInfo("CFP Franc", "XPF", 953);
            CurrencyInfoByIsoCurrencySymbol[955] =
                new CurrencyInfo("Bond Markets Units European Composite Unit (EURCO)", "XBA", 955);
            CurrencyInfoByIsoCurrencySymbol[956] = new CurrencyInfo("European Monetary Unit (E.M.U.-6)", "XBB", 956);
            CurrencyInfoByIsoCurrencySymbol[957] = new CurrencyInfo("European Unit of Account 9(E.U.A.-9)", "XBC", 957);
            CurrencyInfoByIsoCurrencySymbol[958] =
                new CurrencyInfo("European Unit of Account 17(E.U.A.-17)", "XBD", 958);
            CurrencyInfoByIsoCurrencySymbol[959] = new CurrencyInfo("Gold", "XAU", 959);
            CurrencyInfoByIsoCurrencySymbol[960] = new CurrencyInfo("SDR", "XDR", 960);
            CurrencyInfoByIsoCurrencySymbol[961] = new CurrencyInfo("Silver", "XAG", 961);
            CurrencyInfoByIsoCurrencySymbol[962] = new CurrencyInfo("Platinum", "XPT", 962);
            CurrencyInfoByIsoCurrencySymbol[963] =
                new CurrencyInfo("Codes specifically reserved for testing purposes", "XTS", 963);
            CurrencyInfoByIsoCurrencySymbol[964] = new CurrencyInfo("Palladium", "XPD", 964);
            CurrencyInfoByIsoCurrencySymbol[968] = new CurrencyInfo("Surinam Dollar", "SRD", 968);
            CurrencyInfoByIsoCurrencySymbol[969] = new CurrencyInfo("Malagasy Ariary", "MGA", 969);
            CurrencyInfoByIsoCurrencySymbol[971] = new CurrencyInfo("Afghani", "AFN", 971);
            CurrencyInfoByIsoCurrencySymbol[972] = new CurrencyInfo("Somoni", "TJS", 972);
            CurrencyInfoByIsoCurrencySymbol[973] = new CurrencyInfo("Kwanza", "AOA", 973);
            CurrencyInfoByIsoCurrencySymbol[974] = new CurrencyInfo("Belarussian Ruble", "BYR", 974);
            CurrencyInfoByIsoCurrencySymbol[975] = new CurrencyInfo("Bulgarian Lev", "BGN", 975);
            CurrencyInfoByIsoCurrencySymbol[976] = new CurrencyInfo("Franc Congolais", "CDF", 976);
            CurrencyInfoByIsoCurrencySymbol[977] = new CurrencyInfo("Convertible Marks", "BAM", 977);
            CurrencyInfoByIsoCurrencySymbol[978] = new CurrencyInfo("Euro", "EUR", 978);
            CurrencyInfoByIsoCurrencySymbol[980] = new CurrencyInfo("Hryvnia", "UAH", 980);
            CurrencyInfoByIsoCurrencySymbol[981] = new CurrencyInfo("Lari", "GEL", 981);
            CurrencyInfoByIsoCurrencySymbol[985] = new CurrencyInfo("Zloty", "PLN", 985);
            CurrencyInfoByIsoCurrencySymbol[986] = new CurrencyInfo("Brazilian Real", "BRL", 986);
            CurrencyInfoByIsoCurrencySymbol[999] =
                new CurrencyInfo("The codes assigned for transactions where no currency is involved are:", "XXX", 999);


            foreach (var currencyEntry in CurrencyInfoByIsoCurrencySymbol.Values)
            {
                var isoCurrencyCode = currencyEntry.IsoCurrencySymbol;
                var currencySymbol = currencyEntry.CurrencySymbol;

                if (LocalCultureIdsByIsoCurrencyCode.TryGetValue(isoCurrencyCode, out var localCultureIds))
                {
                    foreach (var localCultureId in localCultureIds)
                        IsoCurrencySymbolByLocalCultureId[localCultureId] = currencyEntry.IsoCurrencyCode;

                    LocalCultureIdsByIsoCurrencySymbol[currencyEntry.IsoCurrencyCode] = localCultureIds;
                }

                IsoCurrencySymbolByIsoCurrencyCode[isoCurrencyCode] = currencyEntry.IsoCurrencyCode;

                if (currencySymbol == null) continue;

                if (!IsoCurrencySymbolsByCurrencySymbol.TryGetValue(currencySymbol, out var isoCurrencySymbols))
                {
                    isoCurrencySymbols = new List<int>();
                    IsoCurrencySymbolsByCurrencySymbol[currencySymbol] = isoCurrencySymbols;
                }

                isoCurrencySymbols.Add(currencyEntry.IsoCurrencyCode);
            }

            All = new Currency(008);
            Dzd = new Currency(012);
            Ars = new Currency(032);
            Aud = new Currency(036);
            Bsd = new Currency(044);
            Bhd = new Currency(048);
            Bdt = new Currency(050);
            Amd = new Currency(051);
            Bbd = new Currency(052);
            Bmd = new Currency(060);
            Bob = new Currency(068);
            Bwp = new Currency(072);
            Bzd = new Currency(084);
            Sbd = new Currency(090);
            Bnd = new Currency(096);
            Mmk = new Currency(104);
            Bif = new Currency(108);
            Khr = new Currency(116);
            Cad = new Currency(124);
            Cve = new Currency(132);
            Kyd = new Currency(136);
            Lkr = new Currency(144);
            Clp = new Currency(152);
            Cny = new Currency(156);
            Cop = new Currency(170);
            Kmf = new Currency(174);
            Crc = new Currency(188);
            Hrk = new Currency(191);
            Cup = new Currency(192);
            Czk = new Currency(203);
            Dkk = new Currency(208);
            Dop = new Currency(214);
            Svc = new Currency(222);
            Etb = new Currency(230);
            Ern = new Currency(232);
            Eek = new Currency(233);
            Fkp = new Currency(238);
            Fjd = new Currency(242);
            Djf = new Currency(262);
            Gmd = new Currency(270);
            Gip = new Currency(292);
            Gtq = new Currency(320);
            Gnf = new Currency(324);
            Gyd = new Currency(328);
            Htg = new Currency(332);
            Hnl = new Currency(340);
            Hkd = new Currency(344);
            Huf = new Currency(348);
            Isk = new Currency(352);
            Inr = new Currency(356);
            Idr = new Currency(360);
            Irr = new Currency(364);
            Iqd = new Currency(368);
            Ils = new Currency(376);
            Jmd = new Currency(388);
            Jpy = new Currency(392);
            Kzt = new Currency(398);
            Jod = new Currency(400);
            Kes = new Currency(404);
            Kpw = new Currency(408);
            Krw = new Currency(410);
            Kwd = new Currency(414);
            Kgs = new Currency(417);
            Lak = new Currency(418);
            Lbp = new Currency(422);
            Lvl = new Currency(428);
            Lrd = new Currency(430);
            Lyd = new Currency(434);
            Ltl = new Currency(440);
            Mop = new Currency(446);
            Mwk = new Currency(454);
            Myr = new Currency(458);
            Mvr = new Currency(462);
            Mro = new Currency(478);
            Mur = new Currency(480);
            Mxn = new Currency(484);
            Mnt = new Currency(496);
            Mdl = new Currency(498);
            Mad = new Currency(504);
            Omr = new Currency(512);
            Npr = new Currency(524);
            Ang = new Currency(532);
            Awg = new Currency(533);
            Vuv = new Currency(548);
            Nzd = new Currency(554);
            Nio = new Currency(558);
            Ngn = new Currency(566);
            Nok = new Currency(578);
            Pkr = new Currency(586);
            Pab = new Currency(590);
            Pgk = new Currency(598);
            Pyg = new Currency(600);
            Pen = new Currency(604);
            Php = new Currency(608);
            Gwp = new Currency(624);
            Qar = new Currency(634);
            Rub = new Currency(643);
            Rwf = new Currency(646);
            Shp = new Currency(654);
            Std = new Currency(678);
            Sar = new Currency(682);
            Scr = new Currency(690);
            Sll = new Currency(694);
            Sgd = new Currency(702);
            Skk = new Currency(703);
            Vnd = new Currency(704);
            Sos = new Currency(706);
            Zar = new Currency(710);
            Zwd = new Currency(716);
            Szl = new Currency(748);
            Sek = new Currency(752);
            Chf = new Currency(756);
            Syp = new Currency(760);
            Thb = new Currency(764);
            Top = new Currency(776);
            Ttd = new Currency(780);
            Aed = new Currency(784);
            Tnd = new Currency(788);
            Tmm = new Currency(795);
            Ugx = new Currency(800);
            Mkd = new Currency(807);
            Egp = new Currency(818);
            Gbp = new Currency(826);
            Tzs = new Currency(834);
            Usd = new Currency(840);
            Uyu = new Currency(858);
            Uzs = new Currency(860);
            Wst = new Currency(882);
            Yer = new Currency(886);
            Zmk = new Currency(894);
            Twd = new Currency(901);
            Ghs = new Currency(936);
            Vef = new Currency(937);
            Sdg = new Currency(938);
            Rsd = new Currency(941);
            Mzn = new Currency(943);
            Azn = new Currency(944);
            Ron = new Currency(946);
            Try = new Currency(949);
            Xaf = new Currency(950);
            Xcd = new Currency(951);
            Xof = new Currency(952);
            Xpf = new Currency(953);
            Xba = new Currency(955);
            Xbb = new Currency(956);
            Xbc = new Currency(957);
            Xbd = new Currency(958);
            Xau = new Currency(959);
            Xdr = new Currency(960);
            Xag = new Currency(961);
            Xpt = new Currency(962);
            Xts = new Currency(963);
            Xpd = new Currency(964);
            Srd = new Currency(968);
            Mga = new Currency(969);
            Afn = new Currency(971);
            Tjs = new Currency(972);
            Aoa = new Currency(973);
            Byr = new Currency(974);
            Bgn = new Currency(975);
            Cdf = new Currency(976);
            Bam = new Currency(977);
            Eur = new Currency(978);
            Uah = new Currency(980);
            Gel = new Currency(981);
            Pln = new Currency(985);
            Brl = new Currency(986);
            Xxx = new Currency(999);
        }

        private static CurrencyInfo GetCurrencyInfo(int isoCurrencySymbol)
        {
            if (isoCurrencySymbol == 0)
            {
                return Xxx._currencyInfo;
            }

            if (!CurrencyInfoByIsoCurrencySymbol.TryGetValue(isoCurrencySymbol, out var currencyInfo))
                throw new InvalidOperationException("Unknown currency: " + isoCurrencySymbol +
                                                    "numeric ISO 4217 currency code.");

            return currencyInfo;
        }

        public struct CurrencyInfo : IEquatable<CurrencyInfo>
        {
            internal readonly string CurrencyName;
            internal readonly string CurrencySymbol;
            internal readonly string IsoCurrencySymbol;
            internal readonly ushort IsoCurrencyCode;
            internal readonly RegionInfo RegionInfo;
            internal readonly NumberFormatInfo NumberFormatInfo;

            internal CurrencyInfo(
                string currencyName,
                string isoCurrencySymbol,
                ushort isoCurrencyCode)
            {
                CurrencyName = currencyName;
                IsoCurrencyCode = isoCurrencyCode;
                IsoCurrencySymbol = isoCurrencySymbol;
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

            #region IEquatable<CurrencyInfo> Members

            public bool Equals(CurrencyInfo other)
            {
                return IsoCurrencyCode == other.IsoCurrencyCode;
            }

            #endregion
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

            _currencyInfo = currency._currencyInfo;
            _isoCurrencyCode = currency._isoCurrencyCode;
        }

        public static bool TryParse(string isoCurrencyCodeOrCurrencySymbol, out Currency currency)
        {
            currency = None;

            isoCurrencyCodeOrCurrencySymbol = isoCurrencyCodeOrCurrencySymbol.Trim();

            if (IsoCurrencySymbolByIsoCurrencyCode.TryGetValue(isoCurrencyCodeOrCurrencySymbol,
                    out var isoCurrencySymbol))
            {
                currency = new Currency(isoCurrencySymbol);
                return true;
            }

            if (!IsoCurrencySymbolsByCurrencySymbol.TryGetValue(isoCurrencyCodeOrCurrencySymbol,
                    out var isoCurrencySymbols))
            {
                return false;
            }

            if (IsoCurrencySymbolByLocalCultureId.TryGetValue(Thread.CurrentThread.CurrentCulture.LCID,
                    out isoCurrencySymbol)
                && !isoCurrencySymbols.Contains(isoCurrencySymbol))
            {
                isoCurrencySymbol = isoCurrencySymbols[0];
            }

            currency = new Currency(isoCurrencySymbol);
            return true;
        }

        private CurrencyInfo _currencyInfo;

        public Currency(int isoCurrencyCode)
        {
            _currencyInfo = GetCurrencyInfo(isoCurrencyCode);
            _isoCurrencyCode = isoCurrencyCode;
        }


        public string CurrencyName => _currencyInfo.CurrencyName;

        public string CurrencySymbol => _currencyInfo.CurrencySymbol;

        public string IsoCurrencySymbol => _currencyInfo.IsoCurrencySymbol;

        public int IsoCurrencyCode => _currencyInfo.IsoCurrencyCode;

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
            int currencyId;

            if (IsoCurrencySymbolByLocalCultureId.TryGetValue(cultureInfo.LCID, out currencyId))
                return new Currency(currencyId);

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
            return 609502847 ^ _isoCurrencyCode.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Currency)) return false;

            var other = (Currency)obj;
            return Equals(other);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", CurrencyName, IsoCurrencySymbol);
        }


        #region IEquatable<Currency> Members

        public bool Equals(Currency other)
        {
            return _isoCurrencyCode == other._isoCurrencyCode;
        }

        #endregion

        #region IFormatProvider Members

        public object GetFormat(Type formatType)
        {
            if (formatType != typeof(NumberFormatInfo)) return null;

            return GetNumberFormatInfo(this._isoCurrencyCode);
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