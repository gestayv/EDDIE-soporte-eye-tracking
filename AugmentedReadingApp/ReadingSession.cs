using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Clicks asincrónicos?
    // Cuál es la llamada asincrónica, abrir la form? dibujar a pantalla y mover el mouse?
namespace AugmentedReadingApp
{
    static class ReadingSession
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new InteractionCoordinator());
        }
    }
}
