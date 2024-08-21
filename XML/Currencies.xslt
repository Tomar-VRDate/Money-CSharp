<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:output method="xml" indent="yes"/>
    <xsl:key name="currency-by-id" match="CcyNtry" use="CcyNbr"/>

    <xsl:template match="/ISO_4217">
        <xsl:text>&#xa;</xsl:text> <!-- Line break -->
        <!-- Processing instruction for the XSLT stylesheet -->
        <xsl:processing-instruction name="xml-stylesheet"> type="text/xsl" href="./Currency.xslt"</xsl:processing-instruction>
        <xsl:text>&#xa;</xsl:text> <!-- Line break -->
        <!-- Grouped currency entries -->
        <Currencies>
        <Updated><xsl:value-of select="@Pblshd"/></Updated>
        <SourceURL>https://www.six-group.com/dam/download/financial-information/data-center/iso-currrency/lists/list-one.xml</SourceURL>
            <xsl:for-each select="//CcyNtry[generate-id() = generate-id(key('currency-by-id', CcyNbr)[1])]">
                <!-- Sort by IsoCurrencySymbol -->
                <xsl:sort select="CcyNbr" data-type="number" order="ascending"/>
                <Currency>
                    <CurrencyName><xsl:value-of select="CcyNm"/></CurrencyName>
                    <IsoCurrencyCode><xsl:value-of select="Ccy"/></IsoCurrencyCode>
                    <IsoCurrencySymbol><xsl:value-of select="CcyNbr"/></IsoCurrencySymbol>
                    <xsl:if test="CcyNm/@IsFund">
                        <IsFund><xsl:value-of select="CcyNm/@IsFund"/></IsFund>
                    </xsl:if>
                    <CountryNames>
                        <xsl:for-each select="key('currency-by-id', CcyNbr)">
                            <CountryName><xsl:value-of select="CtryNm"/></CountryName>
                        </xsl:for-each>
                    </CountryNames>
                </Currency>
            </xsl:for-each>
        </Currencies>
    </xsl:template>
</xsl:stylesheet>
