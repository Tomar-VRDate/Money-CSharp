using System.Runtime.Serialization;

namespace System
{
    [Serializable]
    public class MoneyAllocationException : Exception
    {
        public MoneyAllocationException(Money amountToDistribute,
            Money distributionTotal,
            decimal[] distribution)
        {
            AmountToDistribute = amountToDistribute;
            Distribution = distribution;
            DistributionTotal = distributionTotal;
        }

        public MoneyAllocationException(Money amountToDistribute,
            Money distributionTotal,
            decimal[] distribution,
            string message)
            : base(message)
        {
            AmountToDistribute = amountToDistribute;
            Distribution = distribution;
            DistributionTotal = distributionTotal;
        }

        public MoneyAllocationException(Money amountToDistribute,
            Money distributionTotal,
            decimal[] distribution,
            string message,
            Exception inner)
            : base(message, inner)
        {
            AmountToDistribute = amountToDistribute;
            Distribution = distribution;
            DistributionTotal = distributionTotal;
        }

        protected MoneyAllocationException(SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
            AmountToDistribute = (Money)info.GetValue("_amountToDistribute",
                typeof(Money));
            DistributionTotal = (Money)info.GetValue("_distributionTotal",
                typeof(Money));
            Distribution = (decimal[])info.GetValue("_distribution",
                typeof(decimal[]));
        }

        public decimal[] Distribution { get; }

        public Money DistributionTotal { get; }

        public Money AmountToDistribute { get; }
    }
}