using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    //[SerializeField] public GameObject player;
    public GameObject player;
    private NavMeshAgent navMeshAgent;

    public static float attackDamage;
    public static float enemySpeed;

    public GameObject enemySource;
    //public static bool deathState = false;
    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            navMeshAgent.destination = player.transform.position;
        }

        //if (deathState)
        //{
            //Destroy(this.gameObject);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerController>().TakeHit(attackDamage);
            enemySource.GetComponent<EnemySound>().AttackSound();
            Destroy(this.gameObject);
        }
    }


}
