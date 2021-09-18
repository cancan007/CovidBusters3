using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HellFunc : MonoBehaviour
{
    public void OnClick()
    {
        Enemy.enemySpeed = 10f;
        Enemy.attackDamage = 20f;

        GameManager.firstInterval = 2f;
        GameManager.secondInterval = 1f;
        GameManager.thirdInterval = 0.8f;
        GameManager.fourthInterval = 0.5f;

        SceneManager.LoadScene("Indy");
    }
}
