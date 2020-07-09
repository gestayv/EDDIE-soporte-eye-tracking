using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ModuloRastreoOcular
{
    public class ReticleDrawing
    {
        public Image reticle;
        Form transparentForm;
        int x, y;
        // Recibo imagen o ruta de imagen que actua como reticula

        // Alternativas:
        // 1. Recibo elemento gráfico de la form sobre la cual dibujo
        // 2. Uso las librerías del sistema para dibujar
        // 3. Uso una librería gráfica para dibujar
        public ReticleDrawing()
        {
            transparentForm = new Form
            {
                BackColor = Color.White,
                TransparencyKey = Color.White,
                FormBorderStyle = FormBorderStyle.None,
                Bounds = Screen.PrimaryScreen.Bounds,
                TopMost = true
            };
            transparentForm.Paint += TransparentForm_Paint;
            transparentForm.Show();
        }

        public void updateData(string xCoord, string yCoord)
        {
            x = Int32.Parse(xCoord) - reticle.Width / 2;
            y = Int32.Parse(yCoord) - reticle.Height / 2;
            if (transparentForm.InvokeRequired)
            {
                transparentForm.Invoke(new MethodInvoker(updateGUI));
            }
        }

        public void updateGUI() 
        {
            transparentForm.Invalidate();
            transparentForm.Update();
        }

        private void TransparentForm_Paint(object sender, PaintEventArgs e)
        {
            if(reticle != null)
            {
                Point ulCorner = new Point(x, y);
                Size imgSize = new Size(reticle.Width, reticle.Height);
                Rectangle destRec = new Rectangle(ulCorner, imgSize);


                e.Graphics.DrawImage(reticle, destRec);
            }
        }
    }

}
