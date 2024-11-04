using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject bossAPrefab;
    //public GameObject enemySPrefab;
    //public GameObject enemyMPrefab;
    //public GameObject enemyLPrefab;

    //public GameObject itemCoinPrefab;
    //public GameObject itemPowerPrefab;
    //public GameObject itemBoomPrefab;

    public GameObject bulletPlayerAPrefab;
    public GameObject bulletPlayerBPrefab;
    public GameObject bulletFollowerPrefab;
    //public GameObject bulletEnemyAPrefab;
    //public GameObject bulletEnemyBPrefab;
    //public GameObject bulletEnemyCPrefab;
    public GameObject bulletBossPrefab;


    GameObject[] bossA;
    //GameObject[] enemyS;
    //GameObject[] enemyM;
    //GameObject[] enemyL;

    //GameObject[] itemCoin;
    //GameObject[] itemPower;
    //GameObject[] itemBoom;

    GameObject[] bulletPlayerA;
    GameObject[] bulletPlayerB;
    GameObject[] bulletFollower;
    //GameObject[] bulletEnemyA;
    //GameObject[] bulletEnemyB;
    //GameObject[] bulletEnemyC;
    GameObject[] bulletBoss;

    GameObject[] targetPool;

    void Awake()
    {
        bossA = new GameObject[1];
        //enemyS = new GameObject[20];
        //enemyM = new GameObject[10];
        //enemyL = new GameObject[10];

        //itemCoin = new GameObject[20];
        //itemPower = new GameObject[10];
        //itemBoom = new GameObject[10];

        bulletPlayerA = new GameObject[100];
        bulletPlayerB = new GameObject[100];
        bulletFollower = new GameObject[100];
        //bulletEnemyA = new GameObject[100];
        //bulletEnemyB = new GameObject[100];
        //bulletEnemyC = new GameObject[100];
        bulletBoss = new GameObject[1000];

        Generate();
    }

    void Generate()
    {
        //Enemy
        for (int index = 0; index < bossA.Length; index++)
        {
            bossA[index] = Instantiate(bossAPrefab);
            bossA[index].SetActive(false);
        }

        //for (int index = 0; index < enemyS.Length; index++)
        //{
            //enemyS[index] = Instantiate(enemySPrefab);
            //enemyS[index].SetActive(false);
        //}

        //for (int index = 0; index < enemyM.Length; index++)
        //{
            //enemyM[index] = Instantiate(enemyMPrefab);
            //enemyM[index].SetActive(false);
        //}

        //for (int index = 0; index < enemyL.Length; index++)
        //{
            //enemyL[index] = Instantiate(enemyLPrefab);
            //enemyL[index].SetActive(false);
        //}

        //Item
        //for (int index = 0; index < itemCoin.Length; index++)
        //{
            //itemCoin[index] = Instantiate(itemCoinPrefab);
            //itemCoin[index].SetActive(false);
        //}

        //for (int index = 0; index < itemPower.Length; index++)
        //{
            //itemPower[index] = Instantiate(itemPowerPrefab);
            //itemPower[index].SetActive(false);
        //}

        //for (int index = 0; index < itemBoom.Length; index++)
        //{
            //itemBoom[index] = Instantiate(itemBoomPrefab);
            //itemBoom[index].SetActive(false);
        //}

        //Bullet
        for (int index = 0; index < bulletPlayerA.Length; index++)
        {
            bulletPlayerA[index] = Instantiate(bulletPlayerAPrefab);
            bulletPlayerA[index].SetActive(false);
        }

        for (int index = 0; index < bulletPlayerB.Length; index++)
        {
            bulletPlayerB[index] = Instantiate(bulletPlayerBPrefab);
            bulletPlayerB[index].SetActive(false);
        }

        for (int index = 0; index < bulletFollower.Length; index++)
        {
            bulletFollower[index] = Instantiate(bulletFollowerPrefab);
            bulletFollower[index].SetActive(false);
        }

        //for (int index = 0; index < bulletEnemyA.Length; index++)
        //{
            //bulletEnemyA[index] = Instantiate(bulletEnemyAPrefab);
            //bulletEnemyA[index].SetActive(false);
        //}

        //for (int index = 0; index < bulletEnemyB.Length; index++)
        //{
            //bulletEnemyB[index] = Instantiate(bulletEnemyBPrefab);
            //bulletEnemyB[index].SetActive(false);
        //}

        //for (int index = 0; index < bulletEnemyC.Length; index++)
        //{
            //bulletEnemyC[index] = Instantiate(bulletEnemyCPrefab);
            //bulletEnemyC[index].SetActive(false);
        //}

        //for (int index = 0; index < bulletBoss.Length; index++)
        //{
            //bulletBoss[index] = Instantiate(bulletBossPrefab);
            //bulletBoss[index].SetActive(false);
        //}
    }

    public GameObject MakeObj(string type)
    {
        switch(type)
        {
            case "BossA":
                targetPool = bossA;
                break;

            case "BulletBoss":
                targetPool = bulletBoss;
                break;

            //case "EnemyS":
                //targetPool = enemyS;
                //break;

            //case "EnemyM":
                //targetPool = enemyM;
                //break;

            //case "EnemyL":
                //targetPool = enemyL;
                //break;

            //case "ItemCoin":
                //targetPool = itemCoin;
                //break;

            //case "ItemPower":
                //targetPool = itemPower;
                //break;

            //case "ItemBoom":
                //targetPool = itemBoom;
                //break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;

            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;

            case "BulletFollower":
                targetPool = bulletFollower;
                break;

            //case "BulletEnemyA":
                //targetPool = bulletEnemyA;
                //break;

            //case "BulletEnemyB":
                //targetPool = bulletEnemyB;
                //break;
                
            //case "BulletEnemyC":
                //targetPool = bulletEnemyC;
                //break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].gameObject.activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }

    public GameObject[] GetPool(string type)
    {
        switch (type)
        {
            case "BossA":
                targetPool = bossA;
                break;

            case "BulletBoss":
                targetPool = bulletBoss;
                break;

            //case "EnemyS":
                //targetPool = enemyS;
                //break;

            //case "EnemyM":
                //targetPool = enemyM;
                //break;

            //case "EnemyL":
                //targetPool = enemyL;
                //break;

            //case "ItemCoin":
                //targetPool = itemCoin;
                //break;

            //case "ItemPower":
                //targetPool = itemPower;
                //break;

            //case "ItemBoom":
                //targetPool = itemBoom;
                //break;

            case "BulletPlayerA":
                targetPool = bulletPlayerA;
                break;

            case "BulletPlayerB":
                targetPool = bulletPlayerB;
                break;

            case "BulletFollower":
                targetPool = bulletFollower;
                break;

            //case "BulletEnemyA":
                //targetPool = bulletEnemyA;
                //break;

            //case "BulletEnemyB":
                //targetPool = bulletEnemyB;
                //break;

            //case "BulletEnemyC":
                //targetPool = bulletEnemyC;
                //break;
        }
        return targetPool;
    }
}