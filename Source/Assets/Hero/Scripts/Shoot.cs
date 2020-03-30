using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public ObjectPooler shotPool;

    public void Shot(Vector2 startPosition)
    {
        GameObject shot = shotPool.GetObject();
        shot.transform.position = new Vector2(startPosition.x, startPosition.y); //coloca as coins
        shot.SetActive(true);
        FindObjectOfType<AudioManager>().Play("Shoot");
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
