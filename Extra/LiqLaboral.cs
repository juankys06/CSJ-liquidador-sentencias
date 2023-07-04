
using System;

namespace liquidador_web.Extra
{
    public class LiqLaboral
    {
        public static double CalculaDias(DateTime f_inicio, DateTime f_final) {
            int M = (Math.Abs(f_final.Year - f_inicio.Year) * 12) + f_final.Month - f_inicio.Month;
            double Y = M / 12;
            double mDias = M * 30;
            int D;

            DateTime FT = f_inicio.AddMonths(M);
            if (FT == f_final)
                D = 0;
            else
                D = (f_final - FT).Days;

            return mDias += D;
        }
    }

    public class Cesantias
    {
        public int numero;
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public double dias;
        public double salarioPromedio;
        public double cesantias;
        public double intereses;
        public double rendimiento;
    }

    public class Primas {
        public int numero;
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public double dias;
        public double salarioPromedio;
        public double primas;
    }

    public class Vacaciones {
        public int numero;
        public DateTime fechaInicial;
        public DateTime fechaFinal;
        public double dias;
        public double salarioPromedio;
        public double vacaciones;
    }
}
