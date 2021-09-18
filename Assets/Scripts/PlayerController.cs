using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    float x, z;
    float speed = 0.1f;

    public GameObject cam;  // public にするとUnityで設定できる
    Quaternion cameraRot, characterRot;
    float Xsensitivity = 3f, Ysensitivity = 3f;

    bool cursorLock = true;

    float minX = -90f, maxX = 90f;

    public Animator animator;

    int ammunition = 60, maxAmmunition = 60, ammoClip = 12, maxAmmoClip = 12;

    public static int numEnemy = 0;
    public static bool virusState = true;

    int playerHP = 100, maxPlayerHP = 100;
    public Slider hpBar;
    public Text ammoText;
    public Text enemyNumText;
    //public Canvas playerUI;
    public GameObject playerUI;
    public GameObject aimUI;

    public GameObject mainCamera, subCamera;

    public AudioSource playerFootStep;
    public AudioClip WalkFootSE, RunFootSE;

    public static int sceneCount = 0;

    public GameObject gameManagerObject;
    public static bool colliderBool = true; // 連続で当たり判定をさせないため

    private Rigidbody rb;

    private bool groundFlag = false;

    public GameObject enemySource;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        DontDestroyOnLoad(gameObject);

        GameState.canShoot = true;

        hpBar.value = playerHP;
        ammoText.text = ammoClip + "/" + ammunition;
        enemyNumText.text = "Virus: " + numEnemy;
        DontDestroyOnLoad(playerUI);
        DontDestroyOnLoad(gameManagerObject);

        rb = GetComponent<Rigidbody>();

        DontDestroyOnLoad(enemySource);
        //GameState.gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Ysensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Xsensitivity;

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        if(GameState.gameOver == false)
        {
            UpdateCursorLock();
        }

        //enemyNumText.text = "Virus: " + numEnemy;

        if (Input.GetMouseButton(0) && GameState.canShoot)
        {
            if(ammoClip > 0)
            {
                animator.SetTrigger("Fire");
                GameState.canShoot = false;
                ammoClip -= 1;
                ammoText.text = ammoClip + "/" + ammunition;
            }
            else
            {
                Debug.Log("No ammunition");
                Weapon.instance.TriggerSound();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            int needNum = maxAmmoClip - ammoClip;
            if(ammunition > needNum)
            {
                animator.SetTrigger("Reload");
                ammoClip += needNum;
                ammunition -= needNum;
                ammoText.text = ammoClip + "/" + ammunition;
            }
            else if(needNum >= ammunition)
            {
                animator.SetTrigger("Reload");
                ammoClip += ammunition;
                ammunition = 0;
                ammoText.text = ammoClip + "/" + ammunition;
            }
            
        }

        if(Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            if (!animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", true);
                PlayerWalkFootStep(WalkFootSE);
            }
        }
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
            StopFootStep();
        }

        if(z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            if (!animator.GetBool("Run"))
            {
                animator.SetBool("Run", true);
                speed = 0.3f;
                PlayerRunFootStep(RunFootSE);
            }
        }
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 0.1f;
            StopFootStep();
        }

        if (Input.GetMouseButton(1))
        {
            subCamera.SetActive(true);
            aimUI.SetActive(true);
            mainCamera.GetComponent<Camera>().enabled = false;
        }
        else if (subCamera.activeSelf)
        {
            subCamera.SetActive(false);
            aimUI.SetActive(false);
            mainCamera.GetComponent<Camera>().enabled = true;
        }

        if (Input.GetKeyDown("space"))
        {
            if (groundFlag)
            {
                groundFlag = false;
                rb.AddForce(new Vector3(0, 300f, 0));
            }
        }

        if(transform.position.y < -20)
        {
            GameState.gameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }
    }

    private void FixedUpdate()
    {
        x = 0;
        z = 0;

        //x = Input.GetAxisRaw("Horizontal") * speed;
        //z = Input.GetAxisRaw("Vertical") * speed;

        if (Input.GetKey(KeyCode.W))
        {
            z = 1 * speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            z = -1 * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x = -1 * speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            x = 1 * speed;
        }

        //transform.position += new Vector3(x, 0, z);
        transform.position += cam.transform.forward * z + cam.transform.right * x;  // forward: z軸, right: x軸

        if (!virusState)
        {
            numEnemy += 1;
            enemyNumText.text = "Virus: " + numEnemy;
            virusState = true;
        }
    }

    public void UpdateCursorLock()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);
        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);
        return q;
    }

    // tagがGoalのオブジェクトに接触したときに発生
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal" && sceneCount == 0 && colliderBool)
        {
            sceneCount += 1;
            SceneManager.LoadScene("Demo");
            colliderBool = false;
            GameManager.virusNum += numEnemy;
            //gameManagerObject.GetComponent<GameManager>().CountVirus(numEnemy);
            numEnemy = 0;
            enemyNumText.text = "Virus: " + numEnemy;
            GameManager.goalFlag = true;
        }
        else if(other.gameObject.tag == "Goal" && sceneCount == 1 && colliderBool)
        {
            sceneCount += 1;
            SceneManager.LoadScene("japan");
            colliderBool = false;
            GameManager.virusNum += numEnemy;
            numEnemy = 0;
            enemyNumText.text = "Virus: " + numEnemy;
            GameManager.goalFlag = true;
        }
        else if (other.gameObject.tag == "Goal" && sceneCount == 2 && colliderBool)
        {
            sceneCount += 1;
            SceneManager.LoadScene("China");
            colliderBool = false;
            GameManager.virusNum += numEnemy;
            numEnemy = 0;
            enemyNumText.text = "Virus: " + numEnemy;
            GameManager.goalFlag = true;
        }
        else if (other.gameObject.tag == "Goal" && sceneCount == 3 && colliderBool)
        {
            //sceneCount += 1;
            SceneManager.LoadScene("Clear");
            colliderBool = false;
            GameManager.virusNum += numEnemy;
            numEnemy = 0;
            //enemyNumText.text = "Virus: " + numEnemy;
            GameManager.goalFlag = true;
        }

        else if(other.gameObject.tag == "Ammo" && GameState.ammoFlag)
        {
            ammunition += Random.Range(5, 15);
            ammoText.text = ammoClip + "/" + ammunition;
            GameState.ammoFlag = false;
            Destroy(other.gameObject);
        }
    }

    public void OnCollisionEnter(Collision collision)  // IsTriggerをオンにしなくても大丈夫
    {
        if (collision.gameObject.tag == "Ground")
        {
            groundFlag = true;
        }

        if (collision.gameObject.tag == "Goal" && sceneCount == 2 && colliderBool)   // 物理演算を適応したGoal4でシーンを移動したいためOnTriggerEnterではなくOnColliderEnter
        {
            sceneCount += 1;
            SceneManager.LoadScene("China");
            colliderBool = false;
            GameManager.virusNum += numEnemy;
            numEnemy = 0;
            enemyNumText.text = "Virus: " + numEnemy;
            GameManager.goalFlag = true;
        }
    }

    public void PlayerWalkFootStep(AudioClip clip)
    {
        playerFootStep.loop = true;
        playerFootStep.pitch = 1f;
        playerFootStep.clip = clip;
        playerFootStep.Play();
    }

    public void PlayerRunFootStep(AudioClip clip)
    {
        playerFootStep.loop = true;
        playerFootStep.pitch = 1.3f;
        playerFootStep.clip = clip;
        playerFootStep.Play();
    }

    public void StopFootStep()
    {
        playerFootStep.Stop();
        playerFootStep.loop = false;
        playerFootStep.pitch = 1f;
    }

    public void TakeHit(float damage)
    {
        playerHP = (int)Mathf.Clamp(playerHP - damage, 0, maxPlayerHP);
        hpBar.value = playerHP;

        if(playerHP <= 0)
        {
            //Destroy(playerUI);
            GameState.gameOver = true;
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
