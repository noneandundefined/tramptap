using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace tramptap.Internal.Repository
{
    public class ShopRepository
    {
        private static Dictionary<short, decimal> TapDictionary { get; } = new Dictionary<short, decimal>()
        {
            { 1, 0m },
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

        private static Dictionary<short, decimal> EnergyDictionary { get; } = new Dictionary<short, decimal>()
        {
            { 100, 1000m },
            { 200, 2000m },
            { 300, 3000m },
            { 400, 4000m },
            { 500, 5000m },
            { 1000, 6000m },
            { 1500, 7000m },
            { 2000, 8000m },
            { 2500, 100000m },
            { 3000, 200000m },
            { 3500, 300000m },
            { 4000, 400000m },
            { 5000, 500000m },
            { 6000, 600000m },
            { 7000, 700000m },
            { 8000, 800000m },
            { 9000, 900000m },
            { 10000, 1000000m },
            { 30000, 5000000m },
        };

        //private static short energy_max = 1000;

        public static (short Key, decimal Value)? GetPriceForTap()
        {
            var keys = TapDictionary.Keys.OrderBy(k => k).ToList();
            int index = keys.IndexOf(ClickRepository.ClickForTap());

            if (index != -1 && index < keys.Count - 1)
            {
                short nextKey = keys[index + 1];
                return (nextKey, TapDictionary[nextKey]);
            }
            else
            {
                return (100, TapDictionary[100]);
            }
        }

        public static (short Key, decimal Value)? GetPriceForEnergy()
        {
            var keys = EnergyDictionary.Keys.OrderBy(k => k).ToList();
            int index = keys.IndexOf(ClickRepository.ReadEnergyCountLimit());

            if (index != -1 && index < keys.Count - 1)
            {
                short nextKey = keys[index + 1];
                return (nextKey, EnergyDictionary[nextKey]);
            }
            else
            {
                return (30000, TapDictionary[30000]);
            }
        }
    }
}
