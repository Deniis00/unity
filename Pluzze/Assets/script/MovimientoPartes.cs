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
   
    Vector3 primeraPosicion = new Vector3(16.71000f, 5.71000f, 36.43000f );
    Vector3 posicionFinal;
    public float speed = 20f;
    private  int num = 0;
    private int  result = 0;
    Vector3 velocity = Vector3.zero;
    private int muestraPrimeraPosicion = 0;

    void Start()
    {
        aTimer = new System.Timers.Timer(3000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);
        aTimer.Start();
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


            if (Math.Round(hijoSecundario.transform.position.x,3) != Math.Round(primeraPosicion.x, 3)
                && Math.Round(hijoSecundario.transform.position.y,3) != Math.Round(primeraPosicion.y, 3)
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

    private  void OnTick(object source, ElapsedEventArgs e) {

        Debug.Log("OnTick");
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
            
            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("192.168.3.221","ffa_2022","root","humaita@.20")))
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
        catch(Exception ex)
        {
            Debug.Log("Error en la base de datos  obtenerIdUsuario  " + ex.Message);

        }
       
    }
    private void actualizarIdUsuario( int par_idFuncionario)
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
