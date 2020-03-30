using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaserGenerator : MonoBehaviour
{
    public ObjectPooler bigLasersPool;

    private int maxLaserNumber = 13;
    private float spawnYpos = -1.3f;
    public void Spawn()
    {
        List<Vector3> positions = new List<Vector3>();
        spawnYpos = -1.3f;
        int notspawn1 = Random.Range(0, 8 + 1);
        int notspawn2 = notspawn1 + 1; 
        int notspawn3 = notspawn2 + 1;
        int notspawn4 = notspawn3 + 1;
        int notspawn5 = notspawn4 + 1;
        int notspawn6 = notspawn5 + 1;
        for (int i = 0; i < maxLaserNumber; i++)
        {
            if ((i == notspawn1 || i == notspawn2 || i == notspawn3 || i == notspawn4 || i == notspawn5 || i == notspawn6))
            {
                spawnYpos += 0.25f;
                continue;
            }
            Vector3 laserPos = new Vector3(0, spawnYpos, transform.position.z);
            spawnYpos += 0.25f;
            
            //spawn
            GameObject bigLaserTemp = bigLasersPool.GetObject();
            bigLaserTemp.transform.position = laserPos; 
            bigLaserTemp.SetActive(true);
        }
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
