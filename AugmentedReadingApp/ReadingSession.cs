using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Registrar clicks en guardado de datos (usar un flag cuando se genera un click, que se le pase al registro de datos)
// TODO: Clicks asincrónicos?
// TODO: Modificar clase base para añadir evento de onmouseenter/hover (visualización de datos?)}
// TODO: Guardar configuraciones, cargar siempre la ultima configuración
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
