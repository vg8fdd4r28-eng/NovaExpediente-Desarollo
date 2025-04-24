using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace NovaExpediente.Application.Common.CV
{
    public class LectorJson
    {

        //private static string ruta = "C:\\HAC.Pagos\\Documentador\\DocumentacionMVC\\RepositorioDeDatos\\";
        private static string ruta = "";
        public static Dictionary<string, object> leer(string datos)
        {

            //  string datos = File.ReadAllText(ruta + name);

            return (Dictionary<string, object>)Deserializar(datos);

        }

        public static object Deserializar(string aTexto)
        {
            return Deserializar(JsonConvert.DeserializeObject(aTexto));
        }

        private static object Deserializar(object aObjeto)
        {
            if (aObjeto is JObject)
                return Deserializar(((JObject)aObjeto).ToObject<Dictionary<string, object>>());
            else if (aObjeto is JArray)
                return Deserializar(((JArray)aObjeto).ToObject<List<Dictionary<string, object>>>());
            if (aObjeto is Dictionary<string, object>)
                return Deserializar((Dictionary<string, object>)aObjeto);
            else if (aObjeto is List<Dictionary<string, object>>)
                return Deserializar((List<Dictionary<string, object>>)aObjeto);
            else
                return aObjeto;
        }

        private static Dictionary<string, object> Deserializar(Dictionary<string, object> aObjeto)
        {
            Dictionary<string, object> diccionario = new Dictionary<string, object>();

            foreach (string llave in aObjeto.Keys)
                diccionario.Add(llave, Deserializar(aObjeto[llave]));

            return diccionario;
        }

        private static List<Dictionary<string, object>> Deserializar(List<Dictionary<string, object>> aObjeto)
        {
            List<Dictionary<string, object>> lista = new List<Dictionary<string, object>>();

            foreach (Dictionary<string, object> diccionario in aObjeto)
                lista.Add((Dictionary<string, object>)Deserializar(diccionario));

            return lista;
        }



    }
}
