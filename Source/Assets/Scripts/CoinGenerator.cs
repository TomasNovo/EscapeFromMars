using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{
    public ObjectPooler coinPool;
    private float distanceCoins = 0.25f;
    // int coinAmount;


    void SpawnLine(Vector3 startPosition)
    {
        for (int i = 0; i <= 6; i++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x + distanceCoins * i, startPosition.y, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
    }

    void SpawnSquare(Vector3 startPosition)
    {
        for (int i = 0; i <= 6; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                GameObject coin = coinPool.GetObject();
                coin.transform.position = new Vector3(startPosition.x + distanceCoins * i, startPosition.y - distanceCoins * j, startPosition.z); //coloca as coins
                coin.SetActive(true);
            }
        }
    }

    void SpawnF(Vector3 startPosition)
    {
        for (int i = 0; i <= 2; i++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x + distanceCoins * i, startPosition.y, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
        for (int i = 1; i <= 4; i++)
        {
            if (i == 2)
            {
                GameObject coin2 = coinPool.GetObject();
                coin2.transform.position = new Vector3(startPosition.x + distanceCoins, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
                coin2.SetActive(true);
            }
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
    }

    void SpawnE(Vector3 startPosition)
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 0 || i == 2 || i == 4)
            {
                for (int j = 1; j <= 2; j++)
                {
                    GameObject coin2 = coinPool.GetObject();
                    coin2.transform.position = new Vector3(startPosition.x + distanceCoins * j, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
                    coin2.SetActive(true);
                }
            }
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
    }

    void SpawnU(Vector3 startPosition)
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }

        int j;
        i--;
        for (j = 1; j < 2; j++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x + distanceCoins * j, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }

        int z;
        for (z = 0; z < 5; z++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x + distanceCoins * j, startPosition.y + distanceCoins * z - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
    }

    void SpawnP(Vector3 startPosition)
    {
        for (int i = 0; i <= 2; i++)
        {
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x + distanceCoins * i, startPosition.y, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }
        for (int i = 1; i <= 4; i++)
        {
            if (i == 2)
            {
                GameObject coin2 = coinPool.GetObject();
                coin2.transform.position = new Vector3(startPosition.x + distanceCoins, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
                coin2.SetActive(true);

                GameObject coin3 = coinPool.GetObject();
                coin3.transform.position = new Vector3(startPosition.x + 2 * distanceCoins, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
                coin3.SetActive(true);
            }
            GameObject coin = coinPool.GetObject();
            coin.transform.position = new Vector3(startPosition.x, startPosition.y - distanceCoins * i, startPosition.z); //coloca as coins
            coin.SetActive(true);
        }

        GameObject coin4 = coinPool.GetObject();
        coin4.transform.position = new Vector3(startPosition.x + 2 * distanceCoins, startPosition.y - distanceCoins * 1, startPosition.z); //coloca as coins
        coin4.SetActive(true);
    }

    public void Spawn(Vector3 startPosition)
    {
        int coinType = Random.Range(1, 6 + 1);
        switch (coinType)
        {
            case (1):
                SpawnLine(startPosition);
                break;
            case (2):
                SpawnSquare(startPosition);
                break;
            case (3):
                SpawnF(startPosition);
                break;
            case (4):
                SpawnSquare(startPosition);
                break;
            case (5):
                SpawnU(startPosition);
                break;
            case (6):
                SpawnP(startPosition);
                break;

        }
    }

    public void InitialSpawn()
    {
        SpawnF(new Vector3(2, 1, 0));
        SpawnE(new Vector3(3, 1, 0));
        SpawnU(new Vector3(4, 1, 0));
        SpawnP(new Vector3(5, 1, 0));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
