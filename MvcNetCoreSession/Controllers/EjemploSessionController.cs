using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using MvcNetCoreSession.Extensions;
using MvcNetCoreSession.Helpers;
using MvcNetCoreSession.Models;
using System.Security.Cryptography.X509Certificates;

namespace MvcNetCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionMascota(string accion)
        {
            if (accion!=null)
            {
                if (accion.ToLower()=="almacenar")
                {
                    Mascota mascota = new Mascota();
                    mascota.Nombre="Timon";
                    mascota.Raza="Perro";
                    mascota.Edad=20;
                    //PARA ALMACENAR OBJETOS Mascota debemos convertirlos a byte
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    //ALMACENAMOS EL OBJETO EN SESSION MEDIANTE SET
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"]="Mascota almacenada";
                }else if (accion.ToLower()=="mostrar")
                {
                    byte[]data = HttpContext.Session.Get("MASCOTA");
                    //CONVERTIMOS LOS BYTES RECUPERADOS A OBJETO MASCOTA
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);
                    ViewData["MASCOTA"]=mascota;
                }
                
            }
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if (accion!=null)
            {
                if (accion.ToLower()=="almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora",
                       DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"]="DATOS ALMACENADOS EN LA SESSION";
                }else if (accion.ToLower()=="mostrar")
                {
                    //RECUPERAMOS DATOS DE LA SESSION
                    ViewData["USUARIO"]=HttpContext.Session.GetString("nombre");
                    ViewData["HORA"]=HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }
        public IActionResult SessionCollection(String accion)
        {
            if (accion!=null)
            {
                if (accion.ToLower()=="almacenar")
                {
                    List<Mascota> mascotas = new List<Mascota>
                    {
                        new Mascota{Nombre="Yaki", Raza="Perro",Edad=9},
                        new Mascota{Nombre="Zaza", Raza="Italiano",Edad=28},
                        new Mascota{Nombre="Bryant", Raza=" Persona",Edad=15},
                        new Mascota{Nombre="Perry", Raza="Otnitorrinco",Edad=31}
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotas);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"]="Coleccion almacenada";
                    return View();
                }
                else if (accion.ToLower()=="mostrar")
                {
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotas = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotas);
                }
            }
            return View();
            
            
        }

        public IActionResult SessionMascotaJson(string accion)
        {
            if (accion!=null)
            {
                if (accion.ToLower()=="almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre="Simba",
                        Raza="Leon",
                        Edad=9
                    };
                    //UTILIZAMOS EL HELPER PARA CONVERTIR EL OBJETO A STRING
                    string jsonMascota = HelperJSONSession.SerializeObject<Mascota>(mascota);
                    //ALMACENAMOS EL SESSION A STRING
                    HttpContext.Session.SetString("MASCOTA", jsonMascota);
                    ViewData["MENSAJE"]="Mascota Json almacenada";
                }
                else if (accion.ToLower()=="mostrar")
                {
                    //RECUPERAMOS EL STRING JSON DE MASCOTA
                    string json = HttpContext.Session.GetString("MASCOTA");
                    Mascota mascota = HelperJSONSession.DeserializeObject<Mascota>(json);
                    ViewData["MASCOTA"]=mascota;
                }
            }
            return View();
        }

        public IActionResult SessionMascotaObject(string accion)
        {
            if (accion!=null)
            {
                if (accion.ToLower()=="almacenar")
                {
                    Mascota mascota = new Mascota
                    {
                        Nombre="Olaf",
                        Raza="Muñeco",
                        Edad=9
                    };
                    HttpContext.Session.SetObject("MASCOTAOBJECT", mascota);
                    ViewData["MENSAJE"]="Mascota OBJECT almacenada";
                }
                else if (accion.ToLower()=="mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAOBJECT");
                    ViewData["MASCOTAOBJECT"]=mascota;
                }
            }
            return View();
        }
    }
}
