using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Como aplicar un evento a todas las forms de una aplicación
// TODO: Como aplicar un evento a todos los botones de una form
// TODO: Manejo de coordenadas de eye tracking
// TODO: Cambiar posición de picture box en tiempo real.
// TODO: Ajustar picture box a tamaño de reticula
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
