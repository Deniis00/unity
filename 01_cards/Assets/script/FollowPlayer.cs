using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offSet = new Vector3(0, 8, -10);
    private void Update()
    {
        transform.position = player.transform.position + offSet;
        
    }

}
