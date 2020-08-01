using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ModuloRastreoOcular
{
    class ButtonAlt : Button
    {
        private System.Timers.Timer clickTimer;
        private IntermediateClass intermediate;
        
        public ButtonAlt() : base()
        {
            intermediate = IntermediateClass.GetInstance();
            if (intermediate.mouseControl && (intermediate.clickTimer > 0))
            {
                clickTimer = new System.Timers.Timer(intermediate.clickTimer * 1000);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (clickTimer != null)
            {
                clickTimer.Start();
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (clickTimer != null)
            {
                clickTimer.Stop();
            }
        }

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;

        /// <summary>
        /// Event that generates a click at the mouse's current position once a timer hits zero
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PollUpdates(object sender, EventArgs e)
        {
            //Console.WriteLine(Cursor.Position);
            mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            intermediate.clickRegister = 1;
        }
    }
}
