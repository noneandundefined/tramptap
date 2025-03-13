using tramptap.Internal.Interfaces;

namespace tramptap.Internal.Repository
{
    public static class ClickRepository
    {
        private static long clicks = 0;
        private static short click_for_tap = 1;
        private static short energy_count = 1000;
        private static short energy_count_limit = 1000;

        /// <summary>
        /// Получение сколько кликов за 1 тап
        /// </summary>
        public static short ClickForTap()
        {
            return click_for_tap;
        }

        /// <summary>
        /// Получение текущих кликов
        /// </summary>
        public static long ReadClick()
        {
            return clicks;
        }

        public static short ReadEnergyCountLimit()
        {
            return energy_count_limit;
        }

        /// <summary>
        /// Чтения энергии пользователя
        /// </summary>
        public static short ReadEnergyCount()
        {
            return energy_count;
        }

        /// <summary>
        /// Уменьшение энергии пользователя
        /// </summary>
        public static void WriteEnergyCount()
        {
            energy_count -= click_for_tap;
        }

        /// <summary>
        /// Увеличение кол-во кликов за 1 тап
        /// </summary>
        public static void PayClick(short click, decimal price)
        {
            click_for_tap = click;
            clicks -= (long)price;
        }

        /// <summary>
        /// Запись клика
        /// </summary>
        public static void WriteClick()
        {
            clicks += click_for_tap;
        }
    }
}
