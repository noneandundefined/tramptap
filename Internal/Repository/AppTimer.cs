using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using tramptap.View;

namespace tramptap.Internal.Repository
{
    public class AppTimer
    {
        private static AppTimer _instance;
        private DispatcherTimer _timer;
        private DispatcherTimer _timerPass;

        public event EventHandler Tick;

        public AppTimer()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                _timer.Tick += (sender, e) => Tick?.Invoke(this, EventArgs.Empty);
            }

            if (_timerPass == null && ClickRepository.ReadPassive() > 0)
            {
                _timerPass = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1.2) };
                _timerPass.Tick += (sender, e) => Tick?.Invoke(this, EventArgs.Empty);
                _timerPass.Start();
            }
        }

        public static AppTimer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppTimer();
                }
                return _instance;
            }
        }

        public void StopEnergy()
        {
            this._timer.Stop();
        }

        public void StartEnergy()
        {
            if (!_timer.IsEnabled)
            {
                this._timer.Start();
            }
        }

        public void StopPassive()
        {
            this._timerPass.Stop();
        }

        public void StartPassive()
        {
            if (!_timerPass.IsEnabled && ClickRepository.ReadPassive() > 0)
            {
                this._timerPass.Start();
            }
        }
    }
}
