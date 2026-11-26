using System;
using System.Collections.Generic;

namespace Amortización
{
    public class FilaAmortizacion
    {
        public int Periodo { get; set; }
        public double? Abono { get; set; }
        public double? InteresSobreSaldo { get; set; }
        public double? Amortizacion { get; set; }
        public double Saldo { get; set; }
    }

    public class CalculadoraAmortizacion
    {
        public double CalcularTasaPeriodica(double tasaPorcentaje, bool esEfectivaAnual, int periodosAnuales)
        {
            if (periodosAnuales <= 0)
                throw new ArgumentException("Los períodos anuales deben ser mayores que 0.");

            if (tasaPorcentaje < 0)
                throw new ArgumentException("La tasa de interés no puede ser negativa.");

            double tasaDecimal = tasaPorcentaje / 100.0;

            if (esEfectivaAnual)
            {
                return Math.Pow(1.0 + tasaDecimal, 1.0 / periodosAnuales) - 1.0;
            }

            return tasaDecimal;
        }

        public List<FilaAmortizacion> GenerarTablaConPlazo(
            double deuda,
            double tasaPorcentaje,
            bool esEfectivaAnual,
            int periodosAnuales,
            int numeroPeriodos,
            int periodosSoloInteres,
            out double cuotaCalculada,
            out double tasaPeriodica)
        {
            ValidarDatosBase(deuda, numeroPeriodos);

            if (periodosSoloInteres < 0 || periodosSoloInteres >= numeroPeriodos)
                throw new ArgumentException("Los períodos de gracia deben ser menores al total de períodos.");

            tasaPeriodica = CalcularTasaPeriodica(tasaPorcentaje, esEfectivaAnual, periodosAnuales);
            int periodosAmortizacion = numeroPeriodos - periodosSoloInteres;

            if (tasaPeriodica == 0)
            {
                cuotaCalculada = Math.Round(deuda / periodosAmortizacion, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                double factor = 1.0 - Math.Pow(1.0 + tasaPeriodica, -periodosAmortizacion);
                double cuotaExacta = (deuda * tasaPeriodica) / factor;
                cuotaCalculada = Math.Round(cuotaExacta, 2, MidpointRounding.AwayFromZero);
            }

            var filas = new List<FilaAmortizacion>
            {
                new FilaAmortizacion
                {
                    Periodo = 0,
                    Abono = null,
                    InteresSobreSaldo = null,
                    Amortizacion = null,
                    Saldo = Math.Round(deuda, 2, MidpointRounding.AwayFromZero)
                }
            };

            double saldo = Math.Round(deuda, 2, MidpointRounding.AwayFromZero);

            for (int k = 1; k <= numeroPeriodos; k++)
            {
                double interes = Math.Round(saldo * tasaPeriodica, 2, MidpointRounding.AwayFromZero);

                if (k <= periodosSoloInteres)
                {
                    filas.Add(new FilaAmortizacion
                    {
                        Periodo = k,
                        Abono = interes,
                        InteresSobreSaldo = interes,
                        Amortizacion = 0.0,
                        Saldo = saldo
                    });
                }
                else if (k < numeroPeriodos)
                {
                    double amortizacion = Math.Round(cuotaCalculada - interes, 2, MidpointRounding.AwayFromZero);
                    saldo = Math.Round(saldo - amortizacion, 2, MidpointRounding.AwayFromZero);

                    if (saldo < 0) saldo = 0.0;

                    filas.Add(new FilaAmortizacion
                    {
                        Periodo = k,
                        Abono = cuotaCalculada,
                        InteresSobreSaldo = interes,
                        Amortizacion = amortizacion,
                        Saldo = saldo
                    });
                }
                else
                {
                    double amortizacionFinal = saldo;
                    double abonoFinal = Math.Round(amortizacionFinal + interes, 2, MidpointRounding.AwayFromZero);
                    saldo = 0.0;

                    filas.Add(new FilaAmortizacion
                    {
                        Periodo = k,
                        Abono = abonoFinal,
                        InteresSobreSaldo = interes,
                        Amortizacion = amortizacionFinal,
                        Saldo = saldo
                    });
                }
            }

            return filas;
        }

        public List<FilaAmortizacion> GenerarTablaAmortizacionConstante(
            double deuda,
            double tasaPorcentaje,
            bool esEfectivaAnual,
            int periodosAnuales,
            int numeroPeriodos,
            out double amortizacionConstante,
            out double tasaPeriodica)
        {
            ValidarDatosBase(deuda, numeroPeriodos);

            tasaPeriodica = CalcularTasaPeriodica(tasaPorcentaje, esEfectivaAnual, periodosAnuales);
            amortizacionConstante = Math.Round(deuda / numeroPeriodos, 2, MidpointRounding.AwayFromZero);

            double saldo = Math.Round(deuda, 2, MidpointRounding.AwayFromZero);

            var filas = new List<FilaAmortizacion>
            {
                new FilaAmortizacion
                {
                    Periodo = 0,
                    Abono = null,
                    InteresSobreSaldo = null,
                    Amortizacion = null,
                    Saldo = saldo
                }
            };

            for (int k = 1; k <= numeroPeriodos; k++)
            {
                double interes = Math.Round(saldo * tasaPeriodica, 2, MidpointRounding.AwayFromZero);
                double amortizacion = (k < numeroPeriodos) ? amortizacionConstante : saldo;
                double abono = Math.Round(amortizacion + interes, 2, MidpointRounding.AwayFromZero);
                saldo = (k < numeroPeriodos)
                    ? Math.Round(saldo - amortizacion, 2, MidpointRounding.AwayFromZero)
                    : 0.0;

                filas.Add(new FilaAmortizacion
                {
                    Periodo = k,
                    Abono = abono,
                    InteresSobreSaldo = interes,
                    Amortizacion = amortizacion,
                    Saldo = saldo
                });
            }

            return filas;
        }

        public List<FilaAmortizacion> GenerarTablaConAbonoFijo(
            double deuda,
            double tasaPorcentaje,
            bool esEfectivaAnual,
            int periodosAnuales,
            double abonoFijo,
            out double tasaPeriodica)
        {
            if (deuda <= 0)
                throw new ArgumentException("El monto de la deuda debe ser mayor que 0.");

            if (abonoFijo <= 0)
                throw new ArgumentException("El abono fijo debe ser mayor que 0.");

            tasaPeriodica = CalcularTasaPeriodica(tasaPorcentaje, esEfectivaAnual, periodosAnuales);
            double saldo = Math.Round(deuda, 2, MidpointRounding.AwayFromZero);
            double abono = Math.Round(abonoFijo, 2, MidpointRounding.AwayFromZero);

            double primerInteres = Math.Round(saldo * tasaPeriodica, 2, MidpointRounding.AwayFromZero);
            if (abono <= primerInteres)
                throw new ArgumentException("El abono fijo es menor o igual al interés del primer período; la deuda nunca se liquidará.");

            var filas = new List<FilaAmortizacion>
            {
                new FilaAmortizacion
                {
                    Periodo = 0,
                    Abono = null,
                    InteresSobreSaldo = null,
                    Amortizacion = null,
                    Saldo = saldo
                }
            };

            int k = 0;
            const int limiteSeguridad = 1200;

            while (saldo > 0.0 && k < limiteSeguridad)
            {
                k++;
                double interes = Math.Round(saldo * tasaPeriodica, 2, MidpointRounding.AwayFromZero);
                double totalRequerido = Math.Round(saldo + interes, 2, MidpointRounding.AwayFromZero);

                if (abono >= totalRequerido)
                {
                    double amortizacionFinal = saldo;
                    filas.Add(new FilaAmortizacion
                    {
                        Periodo = k,
                        Abono = totalRequerido,
                        InteresSobreSaldo = interes,
                        Amortizacion = amortizacionFinal,
                        Saldo = 0.0
                    });
                    saldo = 0.0;
                }
                else
                {
                    double amortizacion = Math.Round(abono - interes, 2, MidpointRounding.AwayFromZero);
                    saldo = Math.Round(saldo - amortizacion, 2, MidpointRounding.AwayFromZero);

                    filas.Add(new FilaAmortizacion
                    {
                        Periodo = k,
                        Abono = abono,
                        InteresSobreSaldo = interes,
                        Amortizacion = amortizacion,
                        Saldo = saldo
                    });
                }
            }

            if (k >= limiteSeguridad && saldo > 0.0)
                throw new InvalidOperationException("Se alcanzó el límite máximo de períodos (1200).");

            return filas;
        }

        private void ValidarDatosBase(double deuda, int numeroPeriodos)
        {
            if (deuda <= 0)
                throw new ArgumentException("El monto de la deuda debe ser mayor que 0.");

            if (numeroPeriodos <= 0)
                throw new ArgumentException("El número de períodos debe ser mayor que 0.");
        }
    }
}
