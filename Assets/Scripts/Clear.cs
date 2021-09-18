using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clear : MonoBehaviour
{
    private GameObject player;
    private GameObject gameManagerObject;
    private GameObject playerUI;
    private GameObject enemySound;

    float time;
    public Text clearTimeText;
    public Text virusNum;
    int min = 0;
    float second = 0;

    private void Awake()
    {
        player = GameObject.Find("Player");
        gameManagerObject = GameObject.Find("GameManager");
        playerUI = GameObject.Find("PlayerUI");
        //Destroy(gameManagerObject);
        enemySound = GameObject.Find("EnemySoundSource");
        Destroy(playerUI);
        //playerUI.SetActive(false);
        Destroy(player);
        Destroy(enemySound);

        time = GameManager.secondsClear;
        if(time >= 60)
        {
            min = (int) time / 60;
            time = time - (min * 60);
        }
        second = time;
        clearTimeText.text = min.ToString("00") + ":" + ((int) second).ToString("00");
        virusNum.text = GameManager.virusNum.ToString();
    }
    void Start()
    {
        Destroy(gameManagerObject);
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
