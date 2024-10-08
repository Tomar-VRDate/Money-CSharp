﻿namespace System
{
    public class MoneyDistributor
    {
        private readonly RoundingPlaces _precision;
        private readonly FractionReceivers _receiver;
        private readonly Money _toDistribute;
        private Money _distributedTotal;
        private decimal[] _distribution;

        public MoneyDistributor(Money amountToDistribute,
            FractionReceivers receiver,
            RoundingPlaces precision)
        {
            _toDistribute = amountToDistribute;
            _receiver = receiver;
            _precision = precision;
        }

        public Money[] Distribute(params decimal[] distribution)
        {
            _distribution = distribution;
            throw new NotImplementedException();
        }

        public Money[] Distribute(int count)
        {
            if (count < 1)
                throw new ArgumentOutOfRangeException("count",
                    count,
                    "The number of divisions " +
                    "which should be made " +
                    "must be greater than 0.");

            return Distribute(1 / count);
        }

        public Money[] Distribute(decimal distribution)
        {
            if (distribution > 1 || distribution <= 0)
                throw new ArgumentOutOfRangeException("distribution",
                    distribution,
                    "A uniform distribution must be " +
                    "greater than 0 and " +
                    "less than or equal to 1.0");

            _distribution = new decimal[1];
            _distribution[0] = distribution;

            var distributionCount = (int)Math.Floor(1 / distribution);
            var result = new Money[distributionCount];

            _distributedTotal = new Money(0, _toDistribute.Currency);
            var quantum = (decimal)Math.Pow(10, -(int)_precision);

            for (var i = 0; i < distributionCount; i++)
            {
                var toDistribute = _toDistribute;
                var part = toDistribute / distributionCount;
                part = Math.Round(part - 0.5M * quantum,
                    (int)_precision,
                    MidpointRounding.AwayFromZero);
                result[i] = part;
                _distributedTotal += part;
            }

            var remainder = _toDistribute - _distributedTotal;

            switch (_receiver)
            {
                case FractionReceivers.FirstToLast:
                    for (var i = 0; i < remainder / quantum; i++)
                    {
                        result[i] += quantum;
                        _distributedTotal += quantum;
                    }

                    break;
                case FractionReceivers.LastToFirst:
                    for (var i = (int)(remainder / quantum); i > 0; i--)
                    {
                        result[i] += quantum;
                        _distributedTotal += quantum;
                    }

                    break;
                case FractionReceivers.Random:
                    // need the mersenne twister code... System.Random isn't good enough
                    throw new NotImplementedException();
            }

            if (_distributedTotal != _toDistribute)
                throw new MoneyAllocationException(_toDistribute,
                    _distributedTotal,
                    _distribution);

            return result;
        }

        public Money[] Distribute(decimal distribution1, decimal distribution2)
        {
            var distributionSum = distribution1 + distribution2;

            if (distributionSum <= 0 || distributionSum > 1)
                throw new ArgumentException("The sum of the distributions" +
                                            "must be greater than 0 and " +
                                            "less than or equal to 1");

            var result = new Money[2];
            throw new NotImplementedException();
        }

        public Money[] Distribute(decimal distribution1,
            decimal distribution2,
            decimal distribution3)
        {
            throw new NotImplementedException();
        }
    }
}