using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

// TODO: Update diagrama de clases interactioncoordinator ya no tiene agregación con intermediateclass
// TODO: UPDATE diagramas de actividad, de acuerdo a los cambios que se han realizado
// TODO: Registrar clicks en guardado de datos (usar un flag cuando se genera un click, que se le pase al registro de datos)
// TODO: Clicks asincrónicos?
    // Cuál es la llamada asincrónica, abrir la form? dibujar a pantalla y mover el mouse?
// TODO: Modificar clase base para añadir evento de onmouseenter/hover (visualización de datos?)}
    // Cómo le entrego cuanto tarda el click?
// TODO: Guardar configuraciones, cargar siempre la ultima configuración (opcional?)
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
