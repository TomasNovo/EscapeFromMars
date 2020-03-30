using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class OponentPool : MonoBehaviour
{
    public int oponentsPoolSize = 3;
    public GameObject asteroidPrefab;
    public float oponentmaxY = 2.5f;
    public float oponentminY = -2.5f;

    private GameObject[] oponents;
    private Vector2 asteroidPosition = new Vector2(-15f, -25f);
    private float lastAsteroidTime; 
    private float spawnXpos = 10f;
    private int currentOponent = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        oponents= new GameObject[oponentsPoolSize];
        lastAsteroidTime = Time.time;
        for (int i = 0; i < oponentsPoolSize; i++)
        {
            oponents[i] = (GameObject) Instantiate(asteroidPrefab, asteroidPosition, Quaternion.identity);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - lastAsteroidTime) >= GameController.instance.asteroidSpeed){
            lastAsteroidTime = Time.time;
            float spawnYpos = Random.Range(oponentminY, oponentmaxY);
            oponents [currentOponent].transform.position = new Vector3(spawnXpos, spawnYpos);
             oponents [currentOponent].transform.Rotate(0, 0, Random.Range(0f, 360f));  
            currentOponent++;
            if(currentOponent >= oponentsPoolSize){
                currentOponent = 0;
            }
        }

    }
}*/
