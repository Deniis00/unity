using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Timers;
using UnityEditor;
using UnityEngine;

public class MovimientoPartes : MonoBehaviour
{
   private  System.Timers.Timer aTimer;
    public   GameObject principal;
    public   GameObject secundario;
    public   GameObject hijoPrincipal;
    public   GameObject hijoSecundario;
    public GameObject cubo;
   
    Vector3 primeraPosicion = new Vector3(16.71f, 5.71f, 36.43f );
    Vector3 posicionFinal;
    public float speed = 20f;
    private  int num = 0;
    private Boolean esVisible;
    private float smoothTime = 10f;
    private int  result = 0;
    Vector3 velocity = Vector3.zero;
    private MySqlDataReader comDr;

    private float smoothedValue = 0.0f;
    private float velocity2 = 0.0F;
    //void Update()
    //{
    //    smoothedValue = Mathf.SmoothDamp(smoothedValue, 100, ref velocity, 10);
    //    Debug.Log(smoothedValue);
    //}
    private int primero = 0;

    void Start()
    {
        aTimer = new System.Timers.Timer(1000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);
        aTimer.Start();
       
       
      // principal.SetActive(false);

        //hijoPrincipal = principal.transform.GetChild(0).gameObject;
       // hijoSecundario = secundario.transform.GetChild(0).gameObject;
        //hijoSecundario.transform.position = hijoPrincipal.transform.position;
        //  hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, hijoPrincipal.transform.position, ref velocity, smoothTime, speed);
       

    }

    private void Update()
    {

        if (num > 0)
        {
            hijoPrincipal = principal.transform.GetChild(num - 1).gameObject;
            hijoSecundario = secundario.transform.GetChild(num - 1).gameObject;
            posicionFinal = hijoPrincipal.transform.position;
            num = 0;
            primero = 1;
        }
        if (hijoSecundario != null)
        {
            //Math.Round(4.34m, 2);
            //Debug.Log("Positcion hijo ======>  x  => " + Math.Round(hijoSecundario.transform.position.x,2) + "  y => "+ hijoSecundario.transform.position.y +"  z => " + hijoSecundario.transform.position.z) ;
            //Debug.Log("Positcion posicionFinal ======>   " + posicionFinal);
            //Debug.Log("Positcion primeraPosicion ======>   " + primeraPosicion);

            if ((Math.Round(hijoSecundario.transform.position.x,2) != Math.Round(primeraPosicion.x,2))
                && (Math.Round(hijoSecundario.transform.position.y, 2) != Math.Round(primeraPosicion.y, 2)) 
                && (Math.Round(hijoSecundario.transform.position.z, 2) != Math.Round(primeraPosicion.z, 2))
                && primero == 1)
            {
                Debug.Log("if   " + primeraPosicion + "  ======>   " + hijoSecundario.transform.position);
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, primeraPosicion, ref velocity, 2);
            }
            else
            {
                primero = 0;
                Debug.Log("Else   " + primeraPosicion+ "  ======>   " + hijoSecundario.transform.position);
                hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, posicionFinal, ref velocity, 2);
            }
        }



        // Move the object forward along its z axis 1 unit/second.
        //hijoSecundario.transform.Translate(Vector3.forward * Time.deltaTime);

        // Move the object upward in world space 1 unit/second.
        // hijoPrincipal.transform.Translate(Vector3.up * Time.deltaTime, Space.World);

        //hijoPrincipal = principal.transform.GetChild(0).gameObject;
        //hijoSecundario = secundario.transform.GetChild(0).gameObject;
        //hijoSecundario.transform.position = Vector3.SmoothDamp(hijoSecundario.transform.position, hijoPrincipal.transform.position, ref velocity, smoothTime, speed);
        
        //hijoSecundario.transform.position =Vector3.SmoothDamp(hijoSecundario.transform.position, hijoPrincipal.transform.position,  ref velocity, 1);
        


        //hijoSecundario.transform.position = Vector3.MoveTowards(hijoSecundario.transform.position, hijoPrincipal.transform.position, speed * Time.deltaTime);
        //Debug.Log("update ====> " + num);

       


        //cubo.transform.position = Vector3.MoveTowards(cubo.transform.position, targetPosition, speed * Time.deltaTime);
        //cubo.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, speed);

    }

    private  void OnTick(object source, ElapsedEventArgs e) {

        obtenerIdUsuario();
        
    }
   
     void OnDisable () {
        aTimer.Dispose();
        Debug.Log("timerr");
    }
   
    private void obtenerIdUsuario()
    {   
        

        try
        {
            
            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("192.168.3.221","pega","root","humaita@.20")))
            {
                Con.Open();

                using (MySqlCommand com = new MySqlCommand("pa_ffa_pluzze", Con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("par_opcion",1);
                    com.Parameters.AddWithValue("par_idFuncionario",0);


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
        catch(Exception ex)
        {
            Debug.Log("Error en la base de datos  obtenerIdUsuario  " + ex.Message);

        }
       
    }
    private void actualizarIdUsuario( int par_idFuncionario)
    {


        try
        {
           
            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("192.168.3.221", "pega", "root", "humaita@.20")))
            {
                Con.Open();

                using (MySqlCommand com = new MySqlCommand("pa_ffa_pluzze", Con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    com.Parameters.Clear();
                    com.Parameters.AddWithValue("par_opcion", 2);
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
