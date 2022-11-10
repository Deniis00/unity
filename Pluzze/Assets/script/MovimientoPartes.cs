using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Timers;
using System.Xml.Linq;
using Unity.VisualScripting;
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
    private Funcionario funcionarioMostrar;
    private string empresa;

    void Start()
    {
        aTimer = new System.Timers.Timer(5000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);
        aTimer.Start();
        ObtenerUsuarioAMostrar();
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
        ObtenerUsuarioAMostrar();

    }

    void OnDisable()
    {
        aTimer.Dispose();
        Debug.Log("timerr");
    }

    private void ObtenerUsuarioAMostrar()
    {
        try
        {   
            WebRequest request = WebRequest.Create("http://ffa-2022/api/obtener-muestra-pantalla");
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //XmlTextReader reader = new XmlTextReader(stream);
                    StreamReader reader = new StreamReader(stream);

                    var json = reader.ReadToEnd();

                    Data data = JsonConvert.DeserializeObject<Data>(json);

                    Debug.Log("success ====> " + data.success);
                    if (data.success == 1)
                    {
                        Debug.Log("id ===>   " + data.funcionario.id);
                        Debug.Log("id_funcionario ===>   " + data.funcionario.id_funcionario);
                        Debug.Log("nombre_funcionario ===>   " + data.funcionario.nombre_funcionario);

                    
                        funcionarioMostrar = new Funcionario();
                        funcionarioMostrar = data.funcionario;
                        num = funcionarioMostrar.id;
                        Debug.Log("funcionarioMostrar =====>  " + funcionarioMostrar.id);
                        actualizarIdFuncionario(funcionarioMostrar.id);
                    }

                    

                }
            }

        }
        catch (Exception ex)
        {
            Debug.Log("Error al consultar API " + ex.Message);

        }
    }

   


   
    private void actualizarIdFuncionario(int par_idFuncionario)
    {


        try
        {
            WebRequest request = WebRequest.Create("http://ffa-2022/api/actualizar-bloque-mostrado/"+ par_idFuncionario);
            request.Method = "PUT";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //XmlTextReader reader = new XmlTextReader(stream);
                    StreamReader reader = new StreamReader(stream);

                    var json = reader.ReadToEnd();

                    Data data = JsonConvert.DeserializeObject<Data>(json);

                    Debug.Log("************    actualizarIdFuncionario    *****************");
                    Debug.Log("success ====> " + data.success);
                    Debug.Log("message ====> " + data.message);

                }
            }


        }
        catch (Exception ex)
        {
            Debug.Log("Error al actualizar bloque mostrado " + ex.Message);
        }
    }



    public static string CreateConnStr(string server, string databaseName, string user, string pass)
    {

        string connStr = "server=" + server + ";database=" + databaseName + ";uid=" +
        user + ";password=" + pass + ";CHARSET=utf8;pooling=false;Allow User Variables=True";
        return connStr;
    }


}


public class Funcionario
{
    public int id { get; set; }
    public int id_funcionario { get; set; }
    public string nombre_funcionario { get; set; }
}

public class Data
{
    public Funcionario funcionario { get; set; }
    public int success { get; set; }
    public int error { get; set; }
    public string message { get; set; } 
}