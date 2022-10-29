using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Propiedades
    [Range(0f, 100f), SerializeField, Tooltip("Indica la velocidad del coche")]
    private float velocidad = 5f;
 
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mover el coche
        
        transform.Translate(velocidad * Time.deltaTime * Vector3.forward);
    }
}
