using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartFunc : MonoBehaviour
{
    public GameObject startUI;
    public GameObject diffSettingUI;

    public void OnClick()
    {
        //SceneManager.LoadScene("Indy");
        startUI.SetActive(false);
        diffSettingUI.SetActive(true);
    }
}
