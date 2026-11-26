using System;
using System.IO;
using System.Text;

namespace Amortización
{
    public class HistorialArchivo
    {
        private readonly string _rutaArchivo;

        public HistorialArchivo(string nombreArchivo = "historial_amortizaciones.txt")
        {
            _rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, nombreArchivo);
        }

        public void GuardarRegistro(
            string modalidad,
            double deuda,
            double tasaIngresada,
            string tipoTasa,
            int periodosAnuales,
            double tasaPeriodica,
            double valorReferencia,
            int totalPeriodos,
            double totalIntereses,
            double totalPagado)
        {
            var sb = new StringBuilder();
            sb.AppendLine("============================================================");
            sb.AppendLine($"Fecha y hora : {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine($"Modalidad    : {modalidad}");
            sb.AppendLine($"Deuda inicial: {deuda:F2}");
            sb.AppendLine($"Tasa ingresada: {tasaIngresada:F4}% ({tipoTasa}) | Pagos/año: {periodosAnuales}");
            sb.AppendLine($"Tasa periódica: {tasaPeriodica * 100.0:F4}%");
            sb.AppendLine($"Referencia   : {valorReferencia:F2} | Períodos: {totalPeriodos}");
            sb.AppendLine($"Total interés: {totalIntereses:F2} | Total pagado: {totalPagado:F2}");
            sb.AppendLine("============================================================");
            sb.AppendLine();

            File.AppendAllText(_rutaArchivo, sb.ToString(), Encoding.UTF8);
        }

        public string LeerHistorial()
        {
            if (!File.Exists(_rutaArchivo))
                return "Aún no hay cálculos registrados en el historial.";

            string contenido = File.ReadAllText(_rutaArchivo, Encoding.UTF8);
            return string.IsNullOrWhiteSpace(contenido)
                ? "El archivo de historial está vacío."
                : contenido;
        }
    }
}
