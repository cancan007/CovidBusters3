using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AudioSource weaponSource;
    public AudioClip fireSE, reloadSE, triggerSE;

    public static Weapon instance;

    public Transform shotDirection;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(shotDirection.position, shotDirection.transform.forward * 10, Color.green);
    }

    public void CanShoot()
    {
        GameState.canShoot = true;
    }

    public void FireSound()
    {
        weaponSource.clip = fireSE;
        weaponSource.Play();
    }

    public void ReloadSound()
    {
        weaponSource.clip = reloadSE;
        weaponSource.Play();
    }

    public void TriggerSound()
    {
        if (!weaponSource.isPlaying)
        {
            weaponSource.clip = triggerSE;
            weaponSource.Play();
        }
    }

    public void Shooting()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(shotDirection.transform.position, shotDirection.transform.forward, out hitInfo, 300))
        {
            if(hitInfo.collider.gameObject.tag == "Enemy")
            {
                Destroy(hitInfo.collider.gameObject);
                //PlayerController.numEnemy += 1;
                PlayerController.virusState = false;
            }
        }
    }
}
