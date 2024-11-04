using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    //public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    //public GameObject bulletObjA;
    //public GameObject bulletObjB;
    //public GameObject bulletObjC;
    public GameObject bulletObjBoss;

    //public GameObject itemCoin;
    //public GameObject itemPower;
    //public GameObject itemBoom;

    public GameObject player;
    public ObjectManager objectManager;

    SpriteRenderer spriteRenderer;
    Animator anim;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (enemyName == "BossA")
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "BossA":
                health = 5000;
                Invoke("Stop", 3);
                break;

            //case "S":
                //health = 5;
                //break;

            //case "M":
                //health = 25;
                //break;

            //case "L":
                //health = 50;
                //break;
        }
    }

    void Stop()
    {
        if (gameObject.activeSelf)
            return;

        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Invoke("Think", 2);
    }

    void Think()
    {
        patternIndex = patternIndex == 3 ? 0 : patternIndex + 1;
        curPatternCount = 0;

        switch (patternIndex)
        {
            case 0:
                FireForward();
                break;

            case 1:
                FireShot();
                break;

            case 2:
                FireArc();
                break;

            case 3:
                FireAround();
                break;
        }
    }

    void FireForward()
    {
        //Fire 4 Bullet Foward
        GameObject bulletL = objectManager.MakeObj("BulletBoss");
        bulletL.transform.position = transform.position + Vector3.left * 0.3f;
        GameObject bulletLL = objectManager.MakeObj("BulletBoss");
        bulletL.transform.position = transform.position + Vector3.left * 0.45f;
        GameObject bulletR = objectManager.MakeObj("BulletBoss");
        bulletR.transform.position = transform.position + Vector3.right * 0.3f;
        GameObject bulletRR = objectManager.MakeObj("BulletBoss");
        bulletR.transform.position = transform.position + Vector3.right * 0.45f;

        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();

        Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
        Vector3 dirVecLL = player.transform.position - (transform.position + Vector3.left * 0.45f);
        Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
        Vector3 dirVecRR = player.transform.position - (transform.position + Vector3.right * 0.45f);

        rigidL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.down * 8, ForceMode2D.Impulse);

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireForward", 2);
        else
            Invoke("Think", 3);
    }

    void FireShot()
    {
        //Fire Bullet Shot
        for (int index = 0; index < 5; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBoss");
            bullet.transform.position = transform.position;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }

    void FireArc()
    {
        //Fire Arc Continue Fire
        GameObject bullet = objectManager.MakeObj("BulletBoss");
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
        Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 10 * curPatternCount/ maxPatternCount[patternIndex]), -1);
        rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

        
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 1.5f);
        else
            Invoke("Think", 3);
    }

    void FireAround()
    {
        //Fire Around
        int roundNumA = 100;
        int roundNumB = 50;
        int roundNum = curPatternCount%2==0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNumA; index++)
        {
            GameObject bullet = objectManager.MakeObj("BulletBoss");
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum)
                                        ,Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);
        }

        //Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
        else
            Invoke("Think", 3);
    }

    void Update()
    {
        if (enemyName == "BossA")
            return;

        Fire();
        Reload();
    }

    void Fire()
    { 
        if (curShotDelay > maxShotDelay)
            return;

        //if (enemyName == "S")
        //{
            //GameObject bulletA = objectManager.MakeObj("BulletEnemyA");
            //bulletA.transform.position = transform.position;

            //Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
            //Vector3 dirVecA = player.transform.position - transform.position;
            //rigidA.AddForce(dirVecA.normalized * 3, ForceMode2D.Impulse);
        //}
        //else if (enemyName == "M")
        //{
            //GameObject bulletB = objectManager.MakeObj("BulletEnemyB");
            //bulletB.transform.position = transform.position;

            //Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
            //Vector3 dirVecB = player.transform.position - transform.position;
            //rigidB.AddForce(dirVecB.normalized * 7, ForceMode2D.Impulse);
        //}
        //else if (enemyName == "L")
        //{
            //GameObject bulletL = objectManager.MakeObj("BulletEnemyC");
            //bulletL.transform.position = transform.position + Vector3.left * 0.3f;
            //GameObject bulletR = objectManager.MakeObj("BulletEnemyC");
            //bulletR.transform.position = transform.position + Vector3.right * 0.3f;

            //Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            //Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            //Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            //Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            //rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);
            //rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
        //}

        curShotDelay = 0;
    }

    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }

    public void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            if (health <= 0)
                return;

            health -= dmg;
            if(enemyName == "BossA")
            {
                anim.SetTrigger("OnHit");
            }
            else
            {
                spriteRenderer.sprite = sprites[1];
                Invoke("ReturnSprite", 1.0f);
            }

            Player playerLogic = player.GetComponent<Player>();
            //playerLogic.score += enemyScore;

            //Random Ratio Item Drop
            //int ran = enemyName == "BossA" ? 0 : Random.Range(0, 10);
            //if(ran < 3) // Not Item 30%
            //{
                //Debug.Log("Not Item");
            //}
            //else if (ran < 6) // 30%
            //{
                //Coin
                //GameObject itemCoin = objectManager.MakeObj("ItemCoin");
                //itemCoin.transform.position = transform.position;
            //}
            //else if (ran < 8) // 20%
            //{
                //Power
                //GameObject itemPower = objectManager.MakeObj("ItemPower");
                //itemPower.transform.position = transform.position;
            //}
            //else if (ran < 10) // 20%
            //{
                //Boom
                //GameObject itemBoom = objectManager.MakeObj("ItemBoom");
                //itemBoom.transform.position = transform.position;
            //}
            //gameObject.SetActive(false);
            //transform.rotation = Quaternion.identity;
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet" && enemyName == "BossA")
        {
            gameObject.SetActive(false);
            transform.rotation = Quaternion.identity;
        }
            
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            gameObject.SetActive(false);
        }
    }
}
