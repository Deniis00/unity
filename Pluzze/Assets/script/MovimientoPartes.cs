using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Xml.Linq;
using UnityEngine;



public class MovimientoPartes : MonoBehaviour
{
    private System.Timers.Timer aTimer;
    public GameObject principal;
    public GameObject secundario;
    public GameObject hijoPrincipal;
    public GameObject hijoSecundario;
    public GameObject cubo;

    Vector3 primeraPosicion = new Vector3(16.71000f, 5.71000f, 36.43000f);
    Vector3 posicionFinal;
    public float speed = 20f;
    private int num = 0;
    private int result = 0;
    Vector3 velocity = Vector3.zero;
    private int muestraPrimeraPosicion = 0;

    private string empresa;

    void Start()
    {
        aTimer = new System.Timers.Timer(10000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);
        //aTimer.Start();
        pruebaAPI();
        //extraerDatosJsonR();
        // principal.SetActive(false);
    }


    private void FixedUpdate()
    {
        if (num > 0)
        {
            hijoPrincipal = principal.transform.GetChild(num - 1).gameObject;
            hijoSecundario = secundario.transform.GetChild(num - 1).gameObject;
            posicionFinal = hijoPrincipal.transform.position;
            num = 0;
            muestraPrimeraPosicion = 1;
        }
        if (hijoSecundario != null)
        {


            if (Math.Round(hijoSecundario.transform.position.x, 3) != Math.Round(primeraPosicion.x, 3)
                && Math.Round(hijoSecundario.transform.position.y, 3) != Math.Round(primeraPosicion.y, 3)
                && Math.Round(hijoSecundario.transform.position.z, 3) != Math.Round(primeraPosicion.z, 3)
                && muestraPrimeraPosicion == 1)
            {
                Debug.Log("muestraPrimeraPosicion");
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, primeraPosicion, ref velocity, 1);
            }
            else
            {
                muestraPrimeraPosicion = 0;
                Debug.Log("muestraPrimeraPosicion = 0");
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, posicionFinal, ref velocity, 1);
            }
        }

    }
    private void Update()
    {





    }

    private void OnTick(object source, ElapsedEventArgs e)
    {

        Debug.Log("OnTick");
        pruebaAPI();
        //obtenerIdUsuario();

    }

    void OnDisable()
    {
        aTimer.Dispose();
        Debug.Log("timerr");
    }

    private void pruebaAPI()
    {
        try
        {   
            WebRequest request = WebRequest.Create("http://ffa-2022:90/api/obtener-muestra-pantalla");
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //XmlTextReader reader = new XmlTextReader(stream);
                    StreamReader reader = new StreamReader(stream);
                    string resp = reader.ReadToEnd();
                   Debug.Log(resp);

                    dynamic jsonObj = JsonConvert.DeserializeObject(resp);
                    foreach (var obj in jsonObj.Properties())
                    {
                        if (obj.Name == "data")
                        {
                            foreach (var obj2 in obj.Value)
                            {
                                Debug.Log(obj2);
                            }
                        }
                    }


                    //JArray jsonPreservar = JArray.Parse(resp);
                    /*var objetos = JObject.Parse(resp);



                    Debug.Log("pasa objetos");

                  string  totalCreditsRemoved = (String)objetos.Value["totalCreditsRemoved"];
                    /* foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                     {
                         //Aqui para poder identificar las propiedades y sus valores
                         /*foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                         {
                             string propiedad = jsonOPropiedades.Name;
                             if (propiedad.Equals("idgoOperacion"))
                             {
                                 var idgoOperacion = Convert.ToInt32(jsonOPropiedades.Value);
                             }
                         }/*

                         Debug.Log(jsonOperaciones);
                         //Aqui puedes acceder al objeto y obtener sus valores
                         var idgoOperacion = Convert.ToInt32(jsonOperaciones["idgoOperacion"]);

                     }*/


                }
            }


           

        }
        catch (Exception ex)
        {
            Debug.Log("Error al consultar API " + ex.Message);

        }
    }

    private void extraerDatosJsonR()
    {
        String cadena = "http://ffa-2022:90/api/obtener-muestra-pantalla";

        var httpWebRequest = (HttpWebRequest)WebRequest.Create(cadena);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "GET";
        try
        {
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                //Extraer datos de la cadena Json 
                var result = streamReader.ReadToEnd();

                Debug.Log(result);
                //Convertir la cadena de texto de un Json a n JArray
                JArray jsonPreservar = JArray.Parse(result);
                foreach (JObject jsonOperaciones in jsonPreservar.Children<JObject>())
                {
                    //Aqui para poder identificar las propiedades y sus valores
                    foreach (JProperty jsonOPropiedades in jsonOperaciones.Properties())
                    {
                        //Debes recorrer el arreglo completo, asi puedes obtener todos los valores las propiedades
                        string propiedad = jsonOPropiedades.Name;

                        Debug.Log("propiedad ====> "+ propiedad);

                        //Leer Json y sacar valor de un campo
                        if (propiedad.Equals("data"))
                        {
                            //Recorrer Json tomando parte de los valores
                           // var val = Convert.ToString(jsonOPropiedades.Value);
                            
                            //Debug.Log(val);
                            //return;
                        }
                    }
                }
                JObject data = JObject.Parse(jsonPreservar[0].ToString());
                var id = Convert.ToInt32(data["id"]);
                Debug.Log(id);
            }
        }
        catch (WebException ex)
        {
            Debug.Log("Error al consultar API " + ex.Message);
        }

    }



    private void obtenerIdUsuario()
    {


        try
        {

            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("192.168.3.221", "ffa_2022", "root", "humaita@.20")))
            {
                Con.Open();

                using (MySqlCommand com = new MySqlCommand("pa_obtener_mostrar", Con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;

                    result = Convert.ToInt32(com.ExecuteScalar());

                    if (result > 0)
                    {
                        num = result;
                        actualizarIdUsuario(result);
                        Debug.Log("recupero ====> " + num);
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error en la base de datos  obtenerIdUsuario  " + ex.Message);

        }

    }
    private void actualizarIdUsuario(int par_idFuncionario)
    {


        try
        {

            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("192.168.3.221", "ffa_2022", "root", "humaita@.20")))
            {
                Con.Open();

                using (MySqlCommand com = new MySqlCommand("pa_actualizar_mostrar", Con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("par_idFuncionario", par_idFuncionario);
                    com.ExecuteNonQuery();

                }

            }
        }
        catch (Exception ex)
        {
            Debug.Log("Error en la base de datos  actualizarIdUsuario  " + ex.Message);
        }
    }



    public static string CreateConnStr(string server, string databaseName, string user, string pass)
    {

        string connStr = "server=" + server + ";database=" + databaseName + ";uid=" +
        user + ";password=" + pass + ";CHARSET=utf8;pooling=false;Allow User Variables=True";
        return connStr;
    }


}

public class Person
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string City { get; set; }
}
public class DataReadyPerson
{
    public int DataReadPersonId { get; set; }
    public string fname { get; set; }
    public string lname { get; set; }
    public string CityOfResidence { get; set; }
}