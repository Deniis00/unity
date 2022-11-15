using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Timers;
using UnityEngine;
using System.Xml;


public class MovimientoPartes : MonoBehaviour
{
    private System.Timers.Timer aTimer;
    public GameObject principal;
    public GameObject secundario;
    public GameObject hijoPrincipal;
    public GameObject hijoSecundario;
    public GameObject cubo;

    Vector3 primeraPosicion = new Vector3(115.98f, 136.90f, 76.00f);
    Vector3 posicionFinal;
    public float speed = 20f;
    private int num = 0;
    Vector3 velocity = Vector3.zero;
    private int muestraPrimeraPosicion = 0;
    private int muestraSegundaPosicion = 0;
    private Funcionario funcionarioMostrar;
    private string URLString = "C:/config/config.xml";
    private string url_ip;
   

    void Start()
    {   
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);

        obtenerUrlAPI();
        aTimer.Start();
        ObtenerUsuarioAMostrar();

    }


    private void FixedUpdate()
    {
        if (num > 0)
        {
            
            
           
            foreach (Transform chil in principal.transform)
            {
               
                if (Int32.Parse(chil.gameObject.name) == num)
                {
                    hijoPrincipal = chil.transform.gameObject;
                    posicionFinal = hijoPrincipal.transform.position;
                }
            }

            foreach (Transform chil in secundario.transform)
            {

                if (Int32.Parse(chil.gameObject.name) == num)
                {
                    hijoSecundario = chil.transform.gameObject;
                }
            }

            num = 0;

            muestraPrimeraPosicion = 1;
            muestraSegundaPosicion = 1;

            
        }
        if (hijoSecundario != null)
        {


            if (Math.Round(hijoSecundario.transform.position.x, 2) != Math.Round(primeraPosicion.x, 2)
                && Math.Round(hijoSecundario.transform.position.y, 2) != Math.Round(primeraPosicion.y, 2)
                && Math.Round(hijoSecundario.transform.position.z, 2) != Math.Round(primeraPosicion.z, 2)
                && muestraPrimeraPosicion == 1)
            {
                //Debug.Log("muestraPrimeraPosicion");
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, primeraPosicion, ref velocity, 0.2f);
            }
            else if(muestraSegundaPosicion == 1)
            {
                muestraPrimeraPosicion = 0;
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, posicionFinal, ref velocity, 2);
               

                if (Math.Round(hijoSecundario.transform.position.x, 2) == Math.Round(posicionFinal.x, 2)
                      && Math.Round(hijoSecundario.transform.position.y, 2) == Math.Round(posicionFinal.y, 2)
                      && Math.Round(hijoSecundario.transform.position.z, 2) == Math.Round(posicionFinal.z, 2)
                     )
                {
                    muestraSegundaPosicion = 0;
                    hijoPrincipal.SetActive(false);
                    hijoSecundario.transform.position = hijoPrincipal.transform.position;
                    
                }

            }
        }

    }
    private void Update()
    {





    }

    private void OnTick(object source, ElapsedEventArgs e)
    {

        ObtenerUsuarioAMostrar();

    }

    void OnDisable()
    {
        aTimer.Dispose();
    
    }

    private void ObtenerUsuarioAMostrar()
    {
        try
        {   
            WebRequest request = WebRequest.Create(url_ip+"api/obtener-muestra-pantalla");
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //XmlTextReader reader = new XmlTextReader(stream);
                    StreamReader reader = new StreamReader(stream);

                    var json = reader.ReadToEnd();

                    Data data = JsonConvert.DeserializeObject<Data>(json);

                    //Debug.Log("success ====> " + data.success);
                    if (data.success == 1)
                    {
                        //Debug.Log("id ===>   " + data.funcionario.id);
                        //Debug.Log("id_funcionario ===>   " + data.funcionario.id_funcionario);
                       // Debug.Log("nombre_funcionario ===>   " + data.funcionario.nombre_funcionario);

                    
                        funcionarioMostrar = new Funcionario();
                        funcionarioMostrar = data.funcionario;
                        num = funcionarioMostrar.id_funcionario;
                    //   Debug.Log("funcionarioMostrar =====>  " + num);
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
            WebRequest request = WebRequest.Create(url_ip+"api/actualizar-bloque-mostrado/"+ par_idFuncionario);
            request.Method = "PUT";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    //XmlTextReader reader = new XmlTextReader(stream);
                    StreamReader reader = new StreamReader(stream);

                    var json = reader.ReadToEnd();

                    Data data = JsonConvert.DeserializeObject<Data>(json);

                  //  Debug.Log("************    actualizarIdFuncionario    *****************");
                   // Debug.Log("success ====> " + data.success);
                    //Debug.Log("message ====> " + data.message);

                }
            }


        }
        catch (Exception ex)
        {
            Debug.Log("Error al actualizar bloque mostrado " + ex.Message);
        }
    }


    public void obtenerUrlAPI()
    {
        XmlDocument xmlDcoument = new XmlDocument();
        xmlDcoument.Load(URLString);
        XmlNodeList xmlNodeList = xmlDcoument.DocumentElement.SelectNodes("/configuration");
        foreach (XmlNode xmlNode in xmlNodeList)
        {
            url_ip = xmlNode.SelectSingleNode("urlAPI").InnerText;
        }
        
;    }


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