using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JapanManager : MonoBehaviour
{
    private GameObject player;
    public static Vector3 location;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        location = new Vector3(-51, 2, -23);
        player.transform.position = location;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
