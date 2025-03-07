using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tramptap.Internal.Interfaces
{
    public interface ClickInterface
    {
        int ClickForTap();

        int ReadClick();

        void PayClick(int click);

        void WriteClick();
    }
}
