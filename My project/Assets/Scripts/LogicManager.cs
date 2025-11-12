using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicManager : MonoBehaviour
{    
    public GameObject CurrentCheckPoint;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        CurrentCheckPoint= null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RespawnPlayer()
    {
       FindObjectOfType<player_movement>().transform.position = CurrentCheckPoint.transform.position;
    }
}
