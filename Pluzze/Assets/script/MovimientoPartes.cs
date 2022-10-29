using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class MovimientoPartes : MonoBehaviour
{
   private  System.Timers.Timer aTimer;
    public   GameObject principal;
    public   GameObject secundario;
    public   GameObject hijoPrincipal;
    public   GameObject hijoSecundario;
    public GameObject cubo;
   
    Vector3 targetPosition;
    public float speed = 10;
    private  int num;
    private Boolean esVisible;
    public float smoothTime = 0.5f;
    Vector3 velocity;


    void Start()
    {
        aTimer = new System.Timers.Timer(2000);
        aTimer.Elapsed += new ElapsedEventHandler(OnTick);
        aTimer.Start();
       
        //esVisible =  principal.activeInHierarchy;
        principal.SetActive(false);

        //Debug.Log("Nombre del principal " +principal.name);
        

        //Debug.Log("Nombre del secundario   " + gameObject.name);

      

        //hijoPrincipal = principal.transform.GetChild(2).gameObject;
        //Debug.Log("hijo del principal " + hijoPrincipal.name);

        //hijoSecundario = transform.GetChild(2).gameObject;
        //Debug.Log("hijo del secundario " + hijoSecundario.name);

        //hijoSecundario.transform.position = hijoPrincipal.transform.position;
       // Debug.Log(hijoSecundario.transform.parent);
        //hijo.transform.position = player.transform.position + offSet

        targetPosition = new Vector3(27f, 2.5f, 7f);
    }

    private void Update()
    {
        

        if (num > 0 && num < 13)
        {
            num = num - 1;
            hijoPrincipal = principal.transform.GetChild(num-1).gameObject;
            hijoSecundario = secundario.transform.GetChild(num).gameObject;
            hijoSecundario.transform.position = hijoPrincipal.transform.position;
           

            
        }


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
            int result;
            using (MySqlConnection Con = new MySqlConnection(CreateConnStr("127.0.0.1","unity","root","123")))
            {
                Con.Open();

                using (MySqlCommand com = new MySqlCommand("pa_obtenerID", Con))
                {
                    com.CommandType = System.Data.CommandType.StoredProcedure;
                    result = (int)com.ExecuteNonQuery();  

                    if(result > 0)
                    {
                        num = result;
                    }
                    Debug.Log("recupero ====> " + num.ToString());
                }

            }
        }
        catch(Exception ex)
        {
            Debug.Log("Error en la base de datos    " + ex.Message);
        }
    }

    public static string CreateConnStr(string server, string databaseName, string user, string pass)
    {

        string connStr = "server=" + server + ";database=" + databaseName + ";uid=" +
        user + ";password=" + pass + ";CHARSET=utf8;pooling=false;Allow User Variables=True";
        return connStr;
    }

}
