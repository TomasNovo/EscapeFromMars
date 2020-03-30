using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunShootGenerator : MonoBehaviour
{
    public ObjectPooler machineGunShoot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(Vector3 startPosition)
    {
        GameObject machineShootTemp = machineGunShoot.GetObject();
        machineShootTemp.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z); //coloca os enemy
        machineShootTemp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
