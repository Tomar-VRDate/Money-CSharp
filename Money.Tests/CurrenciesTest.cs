using System;
using Xunit;

namespace System.Tests
{
    public class CurrenciesTest
    {

        [Fact]
        public void Transform()
        {
            Currencies.Transform(Currencies.SourceUrl,Currencies.InputPath,Currencies.XsltPath,Currencies.OutputPath);
        }
    }
}