using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace Amortización
{
    public partial class Form1 : Form
    {
        private readonly CalculadoraAmortizacion _calculadora = new CalculadoraAmortizacion();
        private readonly HistorialArchivo _historial = new HistorialArchivo();

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            BtnCalcular.Click += BtnCalcular_Click;
            BtnLimpiar.Click += BtnLimpiar_Click;
            BtnCerrar.Click += BtnCerrar_Click;
            BtnHistorial.Click += BtnHistorial_Click;
            CajaAbono.SelectedIndexChanged += CajaAbono_SelectedIndexChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (CajaAbono.Items.Count > 0 && CajaAbono.SelectedIndex < 0)
                CajaAbono.SelectedIndex = 0;

            if (CajaInteres.Items.Count > 0 && CajaInteres.SelectedIndex < 0)
                CajaInteres.SelectedIndex = 0;

            if (string.IsNullOrWhiteSpace(TextGracia.Text))
                TextGracia.Text = "0";

            ActualizarEtiquetaDato();
        }

        private void CajaAbono_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarEtiquetaDato();
        }

        private void ActualizarEtiquetaDato()
        {
            string textoAbono = CajaAbono.SelectedItem?.ToString() ?? "";
            bool esAbonoFijoConocido =
                textoAbono.IndexOf("abono fijo", StringComparison.OrdinalIgnoreCase) >= 0 ||
                textoAbono.IndexOf("conocido", StringComparison.OrdinalIgnoreCase) >= 0;

            bool esAmortizacionConstante =
                textoAbono.IndexOf("constante", StringComparison.OrdinalIgnoreCase) >= 0;

            if (esAbonoFijoConocido)
            {
                label6.Text = "Monto del abono fijo ($):";
                TextGracia.Text = "0";
                TextGracia.Enabled = false;
            }
            else if (esAmortizacionConstante)
            {
                label6.Text = "Número de períodos (N):";
                TextGracia.Text = "0";
                TextGracia.Enabled = false;
            }
            else
            {
                label6.Text = "Número de períodos (N):";
                TextGracia.Enabled = true;
            }
        }

        private void BtnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                double deuda = LeerDouble(TextDeuda.Text, "monto de la deuda");
                double tasa = LeerDouble(TextInteres.Text, "tasa de interés");
                int periodosAnuales = LeerEntero(TextPeriodos.Text, "pagos por año");
                double dato = LeerDouble(TextDato.Text, label6.Text.Replace(":", ""));
                int periodosSoloInteres = LeerEntero(
                    string.IsNullOrWhiteSpace(TextGracia.Text) ? "0" : TextGracia.Text,
                    "períodos de gracia"
                );

                string textoTasa = CajaInteres.SelectedItem?.ToString() ?? "";
                bool esEfectivaAnual = textoTasa.IndexOf("anual", StringComparison.OrdinalIgnoreCase) >= 0;

                string textoAbono = CajaAbono.SelectedItem?.ToString() ?? "";
                bool esAmortizacionConstante =
                    textoAbono.IndexOf("constante", StringComparison.OrdinalIgnoreCase) >= 0;
                bool esAbonoFijoConocido =
                    textoAbono.IndexOf("abono fijo", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    textoAbono.IndexOf("conocido", StringComparison.OrdinalIgnoreCase) >= 0;

                List<FilaAmortizacion> filas;
                double tasaPeriodica;
                double abonoReferencia;

                if (esAbonoFijoConocido)
                {
                    filas = _calculadora.GenerarTablaConAbonoFijo(
                        deuda,
                        tasa,
                        esEfectivaAnual,
                        periodosAnuales,
                        dato,
                        out tasaPeriodica
                    );
                    abonoReferencia = dato;
                }
                else if (esAmortizacionConstante)
                {
                    int numeroPeriodos = Convert.ToInt32(Math.Round(dato));
                    if (numeroPeriodos <= 0)
                        throw new ArgumentException("El número de períodos debe ser mayor que 0.");

                    filas = _calculadora.GenerarTablaAmortizacionConstante(
                        deuda,
                        tasa,
                        esEfectivaAnual,
                        periodosAnuales,
                        numeroPeriodos,
                        out abonoReferencia,
                        out tasaPeriodica
                    );
                }
                else
                {
                    int numeroPeriodos = Convert.ToInt32(Math.Round(dato));
                    if (numeroPeriodos <= 0)
                        throw new ArgumentException("El número de períodos debe ser mayor que 0.");

                    filas = _calculadora.GenerarTablaConPlazo(
                        deuda,
                        tasa,
                        esEfectivaAnual,
                        periodosAnuales,
                        numeroPeriodos,
                        periodosSoloInteres,
                        out abonoReferencia,
                        out tasaPeriodica
                    );
                }

                double totalIntereses = filas.Sum(f => f.InteresSobreSaldo ?? 0.0);
                double totalPagado = filas.Sum(f => f.Abono ?? 0.0);
                int totalPeriodos = filas.Count > 0 ? filas.Max(f => f.Periodo) : 0;

                MostrarTablaEnCajaLista(
                    filas,
                    tasaPeriodica,
                    abonoReferencia,
                    esAbonoFijoConocido,
                    esAmortizacionConstante,
                    totalIntereses,
                    totalPagado
                );

                _historial.GuardarRegistro(
                    string.IsNullOrWhiteSpace(textoAbono) ? "General" : textoAbono,
                    deuda,
                    tasa,
                    string.IsNullOrWhiteSpace(textoTasa) ? "Tasa" : textoTasa,
                    periodosAnuales,
                    tasaPeriodica,
                    abonoReferencia,
                    totalPeriodos,
                    totalIntereses,
                    totalPagado
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }

        private void MostrarTablaEnCajaLista(
            List<FilaAmortizacion> filas,
            double tasaPeriodica,
            double abonoReferencia,
            bool esAbonoFijoConocido,
            bool esAmortizacionConstante,
            double totalIntereses,
            double totalPagado)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Tasa periódica (i*): {tasaPeriodica * 100.0:F4}%");

            if (esAmortizacionConstante)
                sb.AppendLine($"Amortización constante: {abonoReferencia:F2}");
            else if (!esAbonoFijoConocido)
                sb.AppendLine($"Cuota calculada (R): {abonoReferencia:F2}");
            else
                sb.AppendLine($"Total de períodos: {filas.Max(f => f.Periodo)}");

            sb.AppendLine(new string('-', 66));
            sb.AppendLine(string.Format(
                "{0,-7} {1,13} {2,14} {3,14} {4,14}",
                "Per.", "Abono", "Interés", "Amortización", "Saldo"
            ));
            sb.AppendLine(new string('-', 66));

            foreach (var f in filas)
            {
                string abonoStr = f.Abono.HasValue ? f.Abono.Value.ToString("F2") : "-";
                string intStr = f.InteresSobreSaldo.HasValue ? f.InteresSobreSaldo.Value.ToString("F2") : "-";
                string amortStr = f.Amortizacion.HasValue ? f.Amortizacion.Value.ToString("F2") : "-";
                string saldoStr = f.Saldo.ToString("F2");

                sb.AppendLine(string.Format(
                    "{0,-7} {1,13} {2,14} {3,14} {4,14}",
                    f.Periodo, abonoStr, intStr, amortStr, saldoStr
                ));
            }

            sb.AppendLine(new string('-', 66));
            sb.AppendLine($"Total intereses: {totalIntereses:F2}  |  Total pagado: {totalPagado:F2}");

            CajaLista.Text = sb.ToString();
        }

        private void BtnHistorial_Click(object sender, EventArgs e)
        {
            string contenido = _historial.LeerHistorial();

            using (var ventanaHistorial = new Form())
            {
                ventanaHistorial.Text = "Historial de amortizaciones";
                ventanaHistorial.Size = new Size(780, 420);
                ventanaHistorial.StartPosition = FormStartPosition.CenterParent;

                var cuadroTexto = new RichTextBox
                {
                    Dock = DockStyle.Fill,
                    ReadOnly = true,
                    WordWrap = false,
                    ScrollBars = RichTextBoxScrollBars.Both,
                    Font = new Font("Consolas", 9.5f),
                    Text = contenido
                };

                ventanaHistorial.Controls.Add(cuadroTexto);
                ventanaHistorial.ShowDialog(this);
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TextDeuda.Clear();
            TextInteres.Clear();
            TextPeriodos.Clear();
            TextDato.Clear();
            TextGracia.Text = "0";
            CajaLista.Clear();

            if (CajaAbono.Items.Count > 0)
                CajaAbono.SelectedIndex = 0;
            if (CajaInteres.Items.Count > 0)
                CajaInteres.SelectedIndex = 0;

            ActualizarEtiquetaDato();
            TextDeuda.Focus();
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private double LeerDouble(string texto, string nombreCampo)
        {
            string limpio = (texto ?? "").Trim().Replace(',', '.');
            if (!double.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, out double valor))
                throw new ArgumentException($"Ingresa un número válido en {nombreCampo}.");
            return valor;
        }

        private int LeerEntero(string texto, string nombreCampo)
        {
            string limpio = (texto ?? "").Trim();
            if (!int.TryParse(limpio, NumberStyles.Integer, CultureInfo.InvariantCulture, out int valor))
                throw new ArgumentException($"Ingresa un número entero válido en {nombreCampo}.");
            return valor;
        }
    }
}