
using LibPresentacionesJH._1._2.InterfacesPresentacion;
using Newtonsoft.Json;
using System.Text;

namespace LibPresentacionesJH._1._1.ImplementacionesPresentacion
{
    public class Comunicaciones : IComunicaciones
    {
        public async Task<Dictionary<string, object>> Ejecutar(Dictionary<string, object> datos)
        {
            // Se obtiene la URL de destino.
            var url = datos["Url"].ToString();

            // Se obtiene el método HTTP a utilizar.
            var metodo = datos["Metodo"].ToString();

            // Se retiran estos datos del diccionario porque no hacen parte del body.
            datos.Remove("Url");
            datos.Remove("Metodo");

            // Si existe una entidad, se convierte a JSON.
            var stringData = datos.ContainsKey("Entidad") ?
                JsonConvert.SerializeObject(datos["Entidad"]) : "{}";

            // Se crea el contenido del body en formato JSON.
            var body = new StringContent(stringData, Encoding.UTF8, "application/json");

            // Se crea el cliente HTTP.
            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 4, 0);

            // Variable para guardar la respuesta.
            HttpResponseMessage? message = null;

            // Según el método, se ejecuta la petición correspondiente.
            if (metodo == "GET")
            {
                message = await httpClient.GetAsync(url);
            }
            else if (metodo == "POST")
            {
                message = await httpClient.PostAsync(url, body);
            }
            else if (metodo == "PUT")
            {
                message = await httpClient.PutAsync(url, body);
            }
            else if (metodo == "DELETE")
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, url);
                request.Content = body;
                message = await httpClient.SendAsync(request);
            }
            else
            {
                throw new Exception("Metodo no soportado");
            }

            // Si la respuesta no fue exitosa, se lanza error.
            if (!message.IsSuccessStatusCode)
                throw new Exception("Error Comunicacion");

            // Se lee la respuesta como texto.
            var resp = await message.Content.ReadAsStringAsync();

            // Se libera el cliente HTTP.
            httpClient.Dispose();
            httpClient = null;

            // Se limpia el texto de respuesta.
            resp = Replace(resp);

            // Se devuelve la respuesta en el diccionario.
            return new Dictionary<string, object>() {
                { "Valor", resp }
            };
        }

        private string Replace(string resp)
        {
            return resp.Replace("\\\\r\\\\n", "")
                .Replace("\\r\\n", "")
                .Replace("\\", "")
                .Replace("\\\"", "\"")
                .Replace("\"", "'")
                .Replace("'[", "[")
                .Replace("]'", "]")
                .Replace("'{'", "{'")
                .Replace("\\\\", "\\")
                .Replace("'}'", "'}")
                .Replace("}'", "}")
                .Replace("\\n", "")
                .Replace("\\r", "")
                .Replace("    ", "")
                .Replace("'{", "{")
                .Replace("\"", "")
                .Replace("  ", "")
                .Replace("null", "''");
        }
    }
}
