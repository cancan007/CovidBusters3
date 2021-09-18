using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CasualFunc : MonoBehaviour
{
    public void OnClick()
    {
        Enemy.enemySpeed = 3.5f;
        Enemy.attackDamage = 8f;

        GameManager.firstInterval = 7;
        GameManager.secondInterval = 6;
        GameManager.thirdInterval = 5;
        GameManager.fourthInterval = 4;

        SceneManager.LoadScene("Indy");
    }
}
