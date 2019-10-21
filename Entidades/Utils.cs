using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Entidades
{
    public static class Utils
    {
        public static int ToInt(this string entero)
        {
            int.TryParse(entero, out int valor);
            return valor;
        }
        public static decimal ToDecimal(this string decimales)
        {
            Decimal.TryParse(decimales, out decimal valor);
            return valor;
        }
        public static void ShowToastr(this Page page, string message, string title, string type = "info")
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "toastr_message",
            String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
        }
        public static bool EsNulo(this object obj)
        {
            return obj == null;
        }
        public static Analisis ToAnalisis(this object obj)
        {
            return (Analisis)obj;
        }
        public static TiposAnalisis ToTipoAnalisis(this object obj)
        {
            return (TiposAnalisis)obj;
        }
        public static Pago ToPago(this object obj)
        {
            return (Pago)obj;
        }
        public static DateTime ToDatetime(this object obj)
        {
            DateTime.TryParse(obj.ToString(), out DateTime value);
            return value;
        }
        static readonly string FECHA_FORMAT = "yyyy-MM-dd";
        public static string ToFormatDate(this DateTime dateTime)
        {
            return dateTime.ToString(FECHA_FORMAT);
        }
    }
}
