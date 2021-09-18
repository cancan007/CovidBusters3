using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    float seconds;
    float secondsForScene;
    float ammoTime;
    public static float secondsClear;
    // Start is called before the first frame update
    public GameObject player;
    public static bool goalFlag = true;

    public static int virusNum = 0;

    public static float firstInterval;
    public static float secondInterval;
    public static float thirdInterval;
    public static float fourthInterval;

    public GameObject enemySound;

    void Start()
    {
        GameState.ammoFlag = true;
    }
    // Update is called once per frame
    void Update()
    {
        secondsClear += Time.deltaTime;

        seconds += Time.deltaTime;

        if (!PlayerController.colliderBool)
        {
            secondsForScene += Time.deltaTime;
            if(secondsForScene > 5)
            {
                PlayerController.colliderBool = true;
                secondsForScene = 0;
            }
        }

        // india
        if(seconds > firstInterval && PlayerController.sceneCount == 0)
        {
            GameObject obj = (GameObject)Resources.Load("virus02-1");
            GameObject enemy = (GameObject)Instantiate(obj , new Vector3(Random.Range(-5.0f - 10f,-5.0f + 10f), 3.5f, Random.Range(6f - 10f, 6f + 10f)), Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.player = player;
            enemyScript.enemySource = enemySound;
            seconds = 0;

            int luckyNum = Random.Range(0, 9);
            if(luckyNum >= 7)
            {
                GameObject objA = (GameObject)Resources.Load("SyringeAmmo");
                GameObject ammo = (GameObject)Instantiate(objA, new Vector3(Random.Range(-5.0f - 10f, -5.0f + 10f), 3.5f, Random.Range(6f - 10f, 6f + 10f)), Quaternion.identity);
                
            }
        }
        else if(seconds > secondInterval && PlayerController.sceneCount == 1)
        {
            GameObject obj = (GameObject)Resources.Load("virus02-1");
            Vector3 dL = DutchManager.location;
            GameObject enemy = (GameObject)Instantiate(obj, new Vector3(Random.Range(dL.x-12, dL.x+12), 0.5f, Random.Range(dL.z-12, dL.z+12)) , Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.player = player;
            enemyScript.enemySource = enemySound;
            seconds = 0;

            int luckyNum = Random.Range(0, 9);
            if (luckyNum >= 7)
            {
                GameObject objA = (GameObject)Resources.Load("SyringeAmmo");
                GameObject ammo = (GameObject)Instantiate(objA, new Vector3(Random.Range(dL.x - 12, dL.x + 12), 5f, Random.Range(dL.z - 12, dL.z + 12)), Quaternion.identity);

            }
        }
        else if (seconds > thirdInterval && PlayerController.sceneCount == 2)
        {
            GameObject obj = (GameObject)Resources.Load("virus02-1");
            Vector3 jL = JapanManager.location;
            GameObject enemy = (GameObject)Instantiate(obj, new Vector3(Random.Range(jL.x - 10, jL.x + 10), 3.5f, Random.Range(jL.z - 10, jL.z + 10)), Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.player = player;
            enemyScript.enemySource = enemySound;
            seconds = 0;

            int luckyNum = Random.Range(0, 9);
            if (luckyNum >= 7)
            {
                GameObject objA = (GameObject)Resources.Load("SyringeAmmo");
                GameObject ammo = (GameObject)Instantiate(objA, new Vector3(Random.Range(jL.x - 10, jL.x + 10), 5, Random.Range(jL.z - 10, jL.z + 10)), Quaternion.identity);

            }
        }
        else if (seconds > fourthInterval && PlayerController.sceneCount == 3)
        {
            GameObject obj = (GameObject)Resources.Load("virus02-1");
            Vector3 cL = ChinaManager.location;
            GameObject enemy = (GameObject)Instantiate(obj, new Vector3(Random.Range(cL.x - 15, cL.x + 15), -4, Random.Range(cL.z - 15, cL.z + 15)), Quaternion.identity);
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            enemyScript.player = player;
            enemyScript.enemySource = enemySound;
            seconds = 0;

            int luckyNum = Random.Range(0, 9);
            if (luckyNum >= 7)
            {
                GameObject objA = (GameObject)Resources.Load("SyringeAmmo");
                GameObject ammo = (GameObject)Instantiate(objA, new Vector3(Random.Range(cL.x - 15, cL.x + 15), -2, Random.Range(cL.z - 15, cL.z + 15)), Quaternion.identity);

            }
        }

        if (PlayerController.numEnemy >= 1 && PlayerController.sceneCount == 0 && goalFlag)
        {
            GameObject objG = (GameObject)Resources.Load("Goal3");
            GameObject goal = (GameObject)Instantiate(objG, new Vector3(Random.Range(-5-5, -5+5), 1f, Random.Range(8-5, 8+5)), Quaternion.identity);
            goalFlag = false;
        }
        else if(PlayerController.numEnemy >= 1 && PlayerController.sceneCount == 1 && goalFlag)
        {
            GameObject objG = (GameObject)Resources.Load("Goal3");
            Vector3 dL = DutchManager.location;
            GameObject goal = (GameObject)Instantiate(objG, new Vector3(Random.Range(dL.x - 5, dL.x + 5), 2f, Random.Range(dL.z - 5, dL.z + 5)), Quaternion.identity);
            goalFlag = false;
        }
        else if (PlayerController.numEnemy >= 1 && PlayerController.sceneCount == 2 && goalFlag)
        {
            GameObject objG = (GameObject)Resources.Load("Goal4");
            Vector3 jL = JapanManager.location;
            GameObject goal = (GameObject)Instantiate(objG, new Vector3(Random.Range(jL.x - 10, jL.x + 10), 5, Random.Range(jL.z - 10, jL.z + 10)), Quaternion.identity);
            goalFlag = false;
        }
        else if (PlayerController.numEnemy >= 1 && PlayerController.sceneCount == 3 && goalFlag)
        {
            GameObject objG = (GameObject)Resources.Load("Goal3");
            Vector3 cL = ChinaManager.location;
            GameObject goal = (GameObject)Instantiate(objG, new Vector3(Random.Range(cL.x - 15, cL.x + 15), -4, Random.Range(cL.z - 15, cL.z + 15)), Quaternion.identity);
            goalFlag = false;
        }

        if (!GameState.ammoFlag)
        {
            ammoTime += Time.deltaTime;
            if(ammoTime >= 0.1f)
            {
                GameState.ammoFlag = true;
                ammoTime = 0;
            }
        }
    }

    //public void CountVirus(int num)
    //{
       // virusNum += num;
    //}
}
