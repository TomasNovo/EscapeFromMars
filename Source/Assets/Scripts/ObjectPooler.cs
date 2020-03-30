using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject poolObject;
    public int objectCount;
    List<GameObject> pool;

    // Start is called before the first frame update
    void Start()
    {
        pool = new List<GameObject>();
        for (int i = 0; i < objectCount; i++)
        {
            GameObject obj = (GameObject)Instantiate(poolObject);
            obj.SetActive(false); //not active on the scene
            pool.Add(obj); //add to the pool
        }

    }

    public GameObject GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy) //if the object is active
            {
                return pool[i];
            }
        }

        //default (all active)
        GameObject obj = (GameObject)Instantiate(poolObject);
        obj.SetActive(false); //not active on the scene
        pool.Add(obj); //add to the pool
        return obj;
    }

}
