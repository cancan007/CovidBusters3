using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StandardFunc : MonoBehaviour
{
    public void OnClick()
    {
        Enemy.enemySpeed = 5f;
        Enemy.attackDamage = 15f;

        GameManager.firstInterval = 5;
        GameManager.secondInterval = 4;
        GameManager.thirdInterval = 3;
        GameManager.fourthInterval = 2;

        SceneManager.LoadScene("Indy");
    }
}
