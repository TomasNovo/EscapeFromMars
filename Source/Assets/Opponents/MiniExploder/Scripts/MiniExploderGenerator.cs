using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniExploderGenerator : MonoBehaviour
{
    public ObjectPooler miniExploderPool;

    public void Spawn(List<Vector3> startPosition, int amount)
    {

        for (int i = 0; i < amount; i++)
        {
            GameObject miniExploder = miniExploderPool.GetObject();
            miniExploder.transform.position = new Vector3(startPosition[i].x, startPosition[i].y, startPosition[i].z); //coloca os enemy
            miniExploder.SetActive(true);
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
