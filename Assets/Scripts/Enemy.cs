using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject bulletObjA;
    public GameObject bulletObjB;
    public GameObject bulletObjC;

    public GameObject itemCoin;
    public GameObject itemPower;
    public GameObject itemBoom;

    public GameObject player;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Fire();
        Reload();
    }

    void Fire()
    { 
        if (curShotDelay > maxShotDelay)
            return;

        if (enemyName == "S")
        {
            GameObject bulletA = Instantiate(bulletObjA, transform.position, transform.rotation);
            Rigidbody2D rigidA = bulletA.GetComponent<Rigidbody2D>();
            Vector3 dirVecA = player.transform.position - transform.position;
            rigidA.AddForce(dirVecA.normalized * 3, ForceMode2D.Impulse);
        }
        else if (enemyName == "M")
        {
            GameObject bulletB = Instantiate(bulletObjB, transform.position, transform.rotation);
            Rigidbody2D rigidB = bulletB.GetComponent<Rigidbody2D>();
            Vector3 dirVecB = player.transform.position - transform.position;
            rigidB.AddForce(dirVecB.normalized * 7, ForceMode2D.Impulse);
        }
        else if (enemyName == "L")
        {
            GameObject bulletL = Instantiate(bulletObjC, transform.position + Vector3.left * 0.3f, transform.rotation);
            GameObject bulletR = Instantiate(bulletObjC, transform.position + Vector3.right * 0.3f, transform.rotation);
            Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
            Vector3 dirVecL = player.transform.position - (transform.position + Vector3.left * 0.3f);
            Vector3 dirVecR = player.transform.position - (transform.position + Vector3.right * 0.3f);
            rigidL.AddForce(dirVecL.normalized * 5, ForceMode2D.Impulse);
            rigidR.AddForce(dirVecR.normalized * 5, ForceMode2D.Impulse);
        }

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
            Player playerLogic = player.GetComponent<Player>();
            playerLogic.score += enemyScore;

            //#.Random Ratio Item Drop
            int ran = Random.Range(0, 10);
            if(ran < 3) // Not Item 30%
            {
                Debug.Log("Not Item");
            }
            else if (ran < 6) // 30%
            {
                //Coin
                Instantiate(itemCoin, transform.position, itemCoin.transform.rotation);
            }
            else if (ran < 8) // 20%
            {
                //Power
                Instantiate(itemPower, transform.position, itemPower.transform.rotation);
            }
            else if (ran < 10) // 20%
            {
                //Boom
                Instantiate(itemBoom, transform.position, itemBoom.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);
        }
    }
}
