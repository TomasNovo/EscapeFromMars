using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticObjectsGenerator : MonoBehaviour
{
    public ObjectPooler staticObjectsPool;

    public void Spawn(Vector3 startPosition)
    {
        GameObject staticObjectTemp = staticObjectsPool.GetObject();
        staticObjectTemp.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z); //coloca os enemy
        staticObjectTemp.SetActive(true);
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
