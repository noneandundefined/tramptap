using tramptap.Internal.Interfaces;

namespace tramptap.Internal.Repository
{
    public static class ClickRepository
    {
        private static long clicks = 7000000;
        private static short click_for_tap = 1;
        private static short energy_count = 100;
        private static short energy_count_limit = 100;

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

        /// <summary>
        /// Уменьшение лимита энергии пользователя
        /// </summary>
        public static short ReadEnergyCountLimit()
        {
            return energy_count_limit;
        }

        public static void WriteEnergy()
        {
            energy_count += click_for_tap;
        }

        /// <summary>
        /// Чтения энергии пользователя
        /// </summary>
        public static short ReadEnergyCount()
        {
            return energy_count;
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
        /// Увеличение кол-во лимита энергии
        /// </summary>
        public static void PayEnergyLimit(short energy, decimal price)
        {
            energy_count_limit = energy;
            clicks -= (long)price;
        }

        /// <summary>
        /// Запись клика
        /// </summary>
        public static void WriteClick()
        {
            if (energy_count - click_for_tap < 0)
            {
                return;
            }

            clicks += click_for_tap;
            energy_count -= click_for_tap;
        }
    }
}
