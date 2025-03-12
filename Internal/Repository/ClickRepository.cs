using tramptap.Internal.Interfaces;

namespace tramptap.Internal.Repository
{
    public static class ClickRepository
    {
        private static long clicks = 0;
        private static short click_for_tap = 1;
        private static short energy_count = 1000;

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
        /// Чтения энергии пользователя
        /// </summary>
        public static short ReadEnergyCount()
        {
            return energy_count;
        }

        /// <summary>
        /// Увеличение кол-во кликов за 1 тап
        /// </summary>
        public static void PayClick(short click)
        {
            click_for_tap += click;
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
