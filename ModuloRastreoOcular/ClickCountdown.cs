using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ModuloRastreoOcular
{
    class ClickCountdown
    {
        List<Form> forms = new List<Form>();
        private System.Timers.Timer clickTimer;
        public bool executeClick;
       

        /// <summary>
        /// Method to add events to buttons, comboboxes, checkboxes and menustrips of a form.
        /// Said events are related to a timer that, once it reaches zero, it generates a click
        /// in the mouse's actual position.
        /// </summary>
        /// <param name="addEvents"></param>
        public void AssignEvent(Form addEvents)
        {
            forms.Add(addEvents);
            foreach (var button in addEvents.Controls.OfType<Button>())
            {
                button.MouseEnter += OnMouseEnterButton;
                button.MouseLeave += OnMouseLeaveButton;
            }
            foreach (var combobox in addEvents.Controls.OfType<ComboBox>())
            {
                combobox.MouseEnter += OnMouseEnterButton;
                combobox.MouseLeave += OnMouseLeaveButton;
            }
            foreach (var checkbox in addEvents.Controls.OfType<CheckBox>())
            {
                checkbox.MouseEnter += OnMouseEnterButton;
                checkbox.MouseLeave += OnMouseLeaveButton;
            }
            foreach (MenuStrip toolStrip in addEvents.Controls.OfType<MenuStrip>())
            {
                foreach (ToolStripDropDownItem item in toolStrip.Items)
                {
                    item.MouseEnter += OnMouseEnterButton;
                    item.MouseLeave += OnMouseLeaveButton;
                    foreach (ToolStripItem dropDownItem in item.DropDownItems)
                    {
                        dropDownItem.MouseEnter += OnMouseEnterButton;
                        dropDownItem.MouseLeave += OnMouseLeaveButton;
                    }
                }
                
            }
        }

        public void RemoveEvent()
        {
            foreach (Form removeEvents in forms)
            {
                foreach (var button in removeEvents.Controls.OfType<Button>())
                {
                    button.MouseEnter -= OnMouseEnterButton;
                    button.MouseLeave -= OnMouseLeaveButton;
                }
                foreach (var combobox in removeEvents.Controls.OfType<ComboBox>())
                {
                    combobox.MouseEnter -= OnMouseEnterButton;
                    combobox.MouseLeave -= OnMouseLeaveButton;
                }
                foreach (var checkbox in removeEvents.Controls.OfType<CheckBox>())
                {
                    checkbox.MouseEnter -= OnMouseEnterButton;
                    checkbox.MouseLeave -= OnMouseLeaveButton;
                }
                foreach (MenuStrip toolStrip in removeEvents.Controls.OfType<MenuStrip>())
                {
                    foreach (ToolStripDropDownItem item in toolStrip.Items)
                    {
                        item.MouseEnter -= OnMouseEnterButton;
                        item.MouseLeave -= OnMouseLeaveButton;
                        foreach (ToolStripItem dropDownItem in item.DropDownItems)
                        {
                            dropDownItem.MouseEnter -= OnMouseEnterButton;
                            dropDownItem.MouseLeave -= OnMouseLeaveButton;
                        }
                    }

                }
            }
            forms.Clear();
        }

        public void CreateTimer(int seconds)
        {
            clickTimer = new System.Timers.Timer(seconds*1000);
            clickTimer.Elapsed += PollUpdates;
        }

        /// <summary>
        /// Event that starts a timer once the mouse enters a Form control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseEnterButton(object sender, EventArgs e)
        {
            if(clickTimer != null)
                clickTimer.Start();
        }

        /// <summary>
        /// Event that stops a timer countdown once the mouse leaves a Form control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMouseLeaveButton(object sender, EventArgs e)
        {
            if(sender.ToString() == "Configurar")
            {
                Console.WriteLine(sender);
            }
            if(clickTimer != null)
                clickTimer.Stop();
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
            if (executeClick)
            {
                mouse_event(MOUSEEVENTF_LEFTDOWN, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
            }
            
        }
    }
}
