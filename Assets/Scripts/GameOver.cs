using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private GameObject gameManagerObject;
    private GameObject playerUI;
    private GameObject enemySound;

    private void Awake()
    {
        player = GameObject.Find("Player");
        gameManagerObject = GameObject.Find("GameManager");
        playerUI = GameObject.Find("PlayerUI");
        enemySound = GameObject.Find("EnemySoundSource");
        Destroy(gameManagerObject);
        Destroy(playerUI);
        Destroy(enemySound);
        //playerUI.SetActive(false);
        Destroy(player);
    }
    void Start()
    {
        GameState.gameOver = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
