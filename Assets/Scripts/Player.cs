using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchLeft;
    public bool isTouchRight;

    public int life;
    public int score;
    public float speed;
    public int maxPower;
    public int power;
    public int maxBoom;
    public int boom;
    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject boomEffect;
    public GameObject[] follwers;

    public GameManager gameManager;
    public ObjectManager objectManager;
    
    public bool isHit;
    public bool isBoomTime;

    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        Boom();
        Reload();
    }

    void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if ((isTouchLeft && h == -1) || (isTouchRight && h == 1))
            h = 0;

        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchTop && v == 1) || (isTouchBottom && v == -1))
            v = 0;

        Vector3 curPos = transform.position;
        Vector3 nextPos = new Vector3(h, v, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;

        if (Input.GetButtonDown("Vertical") ||
           Input.GetButtonUp("Vertical"))
        {
            anim.SetInteger("Input", (int)v);
        }
    }

    void Fire()
    {
        if (!Input.GetKeyDown(KeyCode.Z))
            return;

        if (curShotDelay > maxShotDelay)
            return;

        switch (power)
        {
            case 0:
                GameObject bullet = objectManager.MakeObj("BulletPlayerA");
                bullet.transform.position = transform.position;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 1:
                GameObject bulletF = objectManager.MakeObj("BulletPlayerA");
                bulletF.transform.position = transform.position;

                Rigidbody2D rigidF = bulletF.GetComponent<Rigidbody2D>();
                rigidF.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 2:
                GameObject bulletL = objectManager.MakeObj("BulletPlayerA");
                bulletL.transform.position = transform.position + Vector3.left * 0.1f;
                GameObject bulletR = objectManager.MakeObj("BulletPlayerA");
                bulletR.transform.position = transform.position + Vector3.right * 0.1f;

                Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
                rigidL.AddForce(Vector2.up * 10, ForceMode2D.Impulse); 
                rigidR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 3:
                GameObject bulletLL = objectManager.MakeObj("BulletPlayerA");
                bulletLL.transform.position = transform.position  + Vector3.left * 0.2f;
                GameObject bulletCC = objectManager.MakeObj("BulletPlayerA");
                bulletCC.transform.position = transform.position;
                GameObject bulletRR = objectManager.MakeObj("BulletPlayerA");
                bulletRR.transform.position = transform.position + Vector3.right * 0.2f;

                Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCC = bulletCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
                rigidLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
            
            case 4:
                GameObject bulletLLL = objectManager.MakeObj("BulletPlayerA");
                bulletLLL.transform.position = transform.position + Vector3.left * 0.2f;
                GameObject bulletCCC = objectManager.MakeObj("BulletPlayerB");
                bulletCCC.transform.position = transform.position;
                GameObject bulletRRR = objectManager.MakeObj("BulletPlayerA");
                bulletRRR.transform.position = transform.position + Vector3.right * 0.2f;

                Rigidbody2D rigidLLL = bulletLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCC = bulletCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRR = bulletRRR.GetComponent<Rigidbody2D>();
                rigidLLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;

            case 5:
                GameObject bulletLLLL = objectManager.MakeObj("BulletPlayerB");
                bulletLLLL.transform.position = transform.position + Vector3.left * 0.2f;
                GameObject bulletCCCC = objectManager.MakeObj("BulletPlayerB");
                bulletCCCC.transform.position = transform.position;
                GameObject bulletRRRR = objectManager.MakeObj("BulletPlayerB");
                bulletRRRR.transform.position = transform.position + Vector3.right * 0.2f;

                Rigidbody2D rigidLLLL = bulletLLLL.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidCCCC = bulletCCCC.GetComponent<Rigidbody2D>();
                Rigidbody2D rigidRRRR = bulletRRRR.GetComponent<Rigidbody2D>();
                rigidLLLL.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidCCCC.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                rigidRRRR.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
                break;
        }

    curShotDelay = 0;
    
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    void Boom()
    {
        if (!Input.GetKeyDown(KeyCode.X))
            return;

        if (isBoomTime)
            return;

        if (boom == 0)
            return;
        
        boom--;
        isBoomTime = true;
        gameManager.UpdateBoomIcon(boom);

        //#1.Effect visible
        boomEffect.SetActive(true);
        Invoke("OffBoomEffect", 4f);

        //#2.Remove Enemy
        GameObject[] enemiesS = objectManager.GetPool("EnemyS");
        GameObject[] enemiesM = objectManager.GetPool("EnemyM");
        GameObject[] enemiesL = objectManager.GetPool("EnemyL");

        for (int index = 0; index < enemiesS.Length; index++)
        {
            if (enemiesS[index].activeSelf)
            {
                Enemy enemyLogic = enemiesS[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }

        for (int index = 0; index < enemiesM.Length; index++)
        {
            if (enemiesM[index].activeSelf)
            {
                Enemy enemyLogic = enemiesM[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }

        for (int index = 0; index < enemiesL.Length; index++)
        {
            if (enemiesL[index].activeSelf)
            {
                Enemy enemyLogic = enemiesL[index].GetComponent<Enemy>();
                enemyLogic.OnHit(1000);
            }
        }

        //#3.Remove Enemy Bullet
        GameObject[] bulletsA = objectManager.GetPool("BulletEnemyA");
        GameObject[] bulletsB = objectManager.GetPool("BulletEnemyB");
        GameObject[] bulletsC = objectManager.GetPool("BulletEnemyC");

        for (int index = 0; index < bulletsA.Length; index++)
        {
            if (bulletsA[index].activeSelf)
            {
                bulletsA[index].SetActive(false);
            }
        }

        for (int index = 0; index < bulletsB.Length; index++)
        {
            if (bulletsB[index].activeSelf)
            {
                bulletsB[index].SetActive(false);
            }
        }

        for (int index = 0; index < bulletsC.Length; index++)
        {
            if (bulletsC[index].activeSelf)
            {
                bulletsC[index].SetActive(false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Border")
        {
            switch(collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = true;
                    break;
                case "Bottom":
                    isTouchBottom = true;
                    break;
                case "Right":
                    isTouchRight = true;
                    break;
                case "Left":
                    isTouchLeft = true;
                    break;
            }
        }

        else if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBullet")
        {
            if (isHit)
                return;

            isHit = true;
            life--;
            gameManager.UpdateLifeIcon(life);

            if(life == 0)
            {
                gameManager.GameOver();
            }
            else
            {
                gameManager.RespawnPlayer();
            }
            gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "Item")
        {
            Item item = collision.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Coin":
                    score += 1000;
                    break;

                case "Power":
                    if (power == maxPower)
                        score += 500;
                    else
                    {
                        power++;
                        AddFollower();
                    }
                    break;

                case "Boom":
                    if (boom == maxBoom)
                        score += 500;
                    else
                    {
                        boom++;
                        gameManager.UpdateBoomIcon(boom);
                    }
                        boom++;
                    break;
            }
            gameObject.SetActive(false);
        }
    }

    void AddFollower()
    {
        if (power == 0)
            follwers[0].SetActive(true);
        else if (power == 1)
            follwers[2].SetActive(true);
        else if (power == 2)
            follwers[2].SetActive(true);
        else if (power == 3)
            follwers[2].SetActive(true);
        else if (power == 4)
            follwers[2].SetActive(true);
        else if (power == 5)
            follwers[2].SetActive(true);
    }

    void OffBoomEffect()
    {
        boomEffect.SetActive(false);
        isBoomTime = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Border")
        {
            switch (collision.gameObject.name)
            {
                case "Top":
                    isTouchTop = false;
                    break;
                case "Bottom":
                    isTouchBottom = false;
                    break;
                case "Right":
                    isTouchRight = false;
                    break;
                case "Left":
                    isTouchLeft = false;
                    break;
            }
        }
    }
}
