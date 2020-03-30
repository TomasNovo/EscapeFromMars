using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunGenerator : MonoBehaviour
{
    public ObjectPooler machineGunGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(Vector3 startPosition)
    {
        GameObject machineGunTemp = machineGunGenerator.GetObject();
        machineGunTemp.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z); //coloca os enemy
        machineGunTemp.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
