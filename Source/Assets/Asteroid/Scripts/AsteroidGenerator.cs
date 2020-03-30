using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{
    public ObjectPooler asteroidGenerator;

    public void Spawn(Vector3 startPosition)
    {
        GameObject asteroidTemp = asteroidGenerator.GetObject();
        asteroidTemp.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z); //coloca os enemy
        asteroidTemp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
