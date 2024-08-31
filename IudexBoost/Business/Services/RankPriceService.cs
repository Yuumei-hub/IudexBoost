namespace IudexBoost.Business.Services
{
    public class RankPriceService
    {
        private readonly Dictionary<string, decimal> _rankPrices;

        public RankPriceService()
        {
            _rankPrices = new Dictionary<string, decimal>
        {
            {"Bronze 5", 0},
            {"Bronze 4", 4},
            {"Bronze 3", 5},
            {"Bronze 2", 6},
            {"Bronze 1", 8},
            {"Silver 5", 10},
            {"Silver 4", 13},
            {"Silver 3", 16},
            {"Silver 2", 19},
            {"Silver 1", 21},
            {"Gold 5", 24},
            {"Gold 4", 27},
            {"Gold 3", 30},
            {"Gold 2", 33},
            {"Gold 1", 36},
            {"Platinum 5", 39},
            {"Platinum 4", 43},
            {"Platinum 3", 47},
            {"Platinum 2", 51},
            {"Platinum 1", 55},
            {"Diamond 5", 59},
            {"Diamond 4", 64},
            {"Diamond 3", 69},
            {"Diamond 2", 74},
            {"Diamond 1", 79},
            {"Master 5", 88},
            {"Master 4", 94},
            {"Master 3", 100},
            {"Master 2", 106},
            {"Master 1", 112},
            {"Grandmaster 5", 118},
        };
        }

        public decimal GetPrice(string rank1, string rank2)
        {
            if (!_rankPrices.TryGetValue(rank1, out var price1))
            {
                throw new KeyNotFoundException($"Rank '{rank1}' not found.");
            }

            if (!_rankPrices.TryGetValue(rank2, out var price2))
            {
                throw new KeyNotFoundException($"Rank '{rank2}' not found.");
            }

            var priceDifference = price2 - price1;

            if (priceDifference <= 0)
            {
                throw new InvalidOperationException("Price difference must be greater than zero.");
            }

            return priceDifference;
        }

        public Dictionary<string, decimal> GetRankPrices()
        {
            return _rankPrices;
        }
    }
}
