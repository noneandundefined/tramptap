using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tramptap.Internal.Repository
{
    public class ShopRepository
    {
        private static Dictionary<short, decimal> TapDictionary { get; } = new Dictionary<short, decimal>()
        {
            { 2, 100m },
            { 3, 200m },
            { 4, 300m },
            { 5, 400m },
            { 6, 500m },
            { 7, 600m },
            { 8, 700m },
            { 9, 800m },
            { 10, 10000m },
            { 20, 20000m },
            { 30, 30000m },
            { 40, 40000m },
            { 50, 50000m },
            { 60, 60000m },
            { 70, 70000m },
            { 80, 80000m },
            { 90, 90000m },
            { 100, 100000m },
        };

        private static short energy_max = 1000;

        public static (short Key, decimal Value)? GetPriceForTap()
        {
            if (TapDictionary.ContainsKey((short)(ClickRepository.ClickForTap() + 1)))
            {
                return ((short)(ClickRepository.ClickForTap() + 1), TapDictionary[(short)(ClickRepository.ClickForTap() + 1)]);
            }

            return null;
        }
    }
}
