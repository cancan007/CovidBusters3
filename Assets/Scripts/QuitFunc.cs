using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitFunc : MonoBehaviour
{
    public void OnClick()
    {
        PlayerController.numEnemy = 0;
        PlayerController.sceneCount = 0;
        GameManager.secondsClear = 0;
        GameManager.goalFlag = true;
        SceneManager.LoadScene("TitleScene");
    }
}
