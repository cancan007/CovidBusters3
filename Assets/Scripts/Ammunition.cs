using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ammunition : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private float seconds = 0;
    private float vecX, vecZ;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        vecX = Random.Range(transform.position.x - 30, transform.position.x + 30);
        vecZ = Random.Range(transform.position.z - 30, transform.position.z + 30);
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if (seconds >= 5)
        {
            vecX = Random.Range(transform.position.x - 30, transform.position.x + 30);
            vecZ = Random.Range(transform.position.z - 30, transform.position.z + 30);
            seconds = 0;
        }

        navMeshAgent.destination = new Vector3(vecX, 0, vecZ);
    }
}
