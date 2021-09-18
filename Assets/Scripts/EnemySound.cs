using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public AudioSource enemySource;
    public AudioClip enemyDeathClip, enemyAttackClip;

    private bool soundFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerController.virusState && soundFlag)
        {
            DeathSound();
            soundFlag = false;
        }
        else if (!soundFlag)
        {
            soundFlag = true;
        }

    }

    public void DeathSound()
    {
        enemySource.time = 0f;  // ����ւ��� AudioClip �̒������A���݂̍Đ��ʒu��菬�����ꍇ�A�G���[���o�Ă��܂��̂�h������
        enemySource.clip = enemyDeathClip;
        enemySource.loop = false;
        enemySource.pitch = 1.3f;
        enemySource.Play();
    }

    public void AttackSound()
    {
        enemySource.clip = enemyAttackClip;
        enemySource.loop = false;
        enemySource.pitch = 1f;
        enemySource.time = 0.2f;
        enemySource.Play();
    }
}
