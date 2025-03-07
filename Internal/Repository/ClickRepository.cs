using tramptap.Internal.Interfaces;

namespace tramptap.Internal.Repository
{
    public class ClickRepository : ClickInterface
    {
        private int clicks = 0;
        private int click_for_tap = 1;

        /// <summary>
        /// Получение сколько кликов за 1 тап
        /// </summary>
        public int ClickForTap()
        {
            return this.click_for_tap;
        }

        /// <summary>
        /// Получение текущих кликов
        /// </summary>
        public int ReadClick()
        {
            return this.clicks;
        }

        /// <summary>
        /// Увеличение кол-во кликов за 1 тап
        /// </summary>
        public void PayClick(int click)
        {
            this.click_for_tap += click;
        }

        /// <summary>
        /// Запись клика
        /// </summary>
        public void WriteClick()
        {
            this.clicks += this.click_for_tap;
        }
    }
}
