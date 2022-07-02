using System;

namespace EstacBem.WebUI.Extensions
{
    public static class MyExtensions
    {
        public static string PtBR_ddMMYYYY_HHmm(this DateTime? date)
        {
            return date == null ? "" : date.Value.ToString("dd/MM/yyyy HH:mm");
        }

        public static string PtBR_ddMMYYYY(this DateTime? date)
        {
            return date == null ? "" : date.Value.ToString("dd/MM/yyyy");
        }

        public static string PtBR_ddMMYYYY(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }


        public static string PtBR_ddMMYYYY_HHmm(this DateTime date)
        {
            return date.ToString("dd/MM/yyyy HH:mm");
        }

        public static string PtBR_HHmm(this DateTime? date)
        {
            return date == null ? "" : date.Value.ToString("HH:mm");
        }

        public static string PtBR_HHmm(this DateTime date)
        {
            return date.ToString("HH:mm");
        }

        public static string PlacaFormatada(this string valor)
        {
            return valor.Trim().ToUpper();
        }
    }
}
