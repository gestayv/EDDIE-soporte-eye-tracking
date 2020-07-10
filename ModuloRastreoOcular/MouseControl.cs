using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ModuloRastreoOcular
{
    class MouseControl
    {
        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x02;

        public void MoveCursor(string xCoordinate, string yCoordinate)
        {
            int x = Int32.Parse(xCoordinate);
            int y = Int32.Parse(yCoordinate);
            SetCursorPos(x, y);
        }


    }
}
