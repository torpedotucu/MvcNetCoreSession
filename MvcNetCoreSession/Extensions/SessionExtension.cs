using MvcNetCoreSession.Helpers;

namespace MvcNetCoreSession.Extensions
{
    public static class SessionExtension
    {
        //CREAMOS UN METODO PARA RECUPERAR CUALQUIER OBJETO 
        public static T GetObject<T>(this ISession session, string key)
        {
            //DEBEMOS RECUPERAR LO QUE TENEMOS ALMACENADO
            string json = session.GetString(key);
            if (json==null)
            {
                return default(T);
            }
            else
            {
                //RECUPERAMOS EL OBJETO QUE TENEMOS ALMACENADO DENTRO DE NUESTRA KEY
                T data = HelperJSONSession.DeserializeObject<T>(json);
                return data;
            }
        }

        public static void SetObject(this ISession session, string key, object value)
        {
            string data = HelperJSONSession.SerializeObject(value);
            //ALMACENAMOS EL JSON DENTRO DE SESSION
            session.SetString(key, data);
        }
    }
}
