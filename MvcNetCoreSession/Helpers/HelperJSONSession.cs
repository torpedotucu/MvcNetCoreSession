using Newtonsoft.Json;

namespace MvcNetCoreSession.Helpers
{
    public class HelperJSONSession
    {
        //VAMOS A UTILIZAR EL METODO GetString() COMO HERRAMIENTA
        //ALAMCENARERMOS OBJETOS CON SERIALIZE DE JSON
        public static string SerializeObject<T>(T data)
        {
            //CONVERTIMOS EL OBJETO A STRING MEDIANTE Newton
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        //RECIBIREMOS UN STRING Y LO CONVERTIMOS A T
        public static T DeserializeObject<T>(string data)
        {
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
    }
}
