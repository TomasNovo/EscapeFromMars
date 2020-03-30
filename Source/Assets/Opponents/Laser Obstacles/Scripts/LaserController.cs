using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    // Lasers spawnam na horizontal, vertical ou oblíqua 
    float speed = 50.0f;
    bool movingLaser = false;

    void ChooseOrientation()
    {
        switch (Random.Range(1, 4 + 1))
        {
            case (1):
                SpawnVertical();
                break;
            case (2):
                SpawnObliqueRight();
                break;
            case (3):
                SpawnObliqueLeft();
                break;
            case (4):
                movingLaser = true;
                SpawnObliqueLeft();
                return;
            default:
                break;
        }
        movingLaser = false;
    }

    void SpawnVertical()
    {
        transform.Rotate(Vector3.forward, -90);
    }

    void SpawnObliqueRight()
    {
        transform.Rotate(Vector3.forward * speed);
    }


    void SpawnObliqueLeft()
    {
        transform.Rotate(-Vector3.forward * speed);
    }

    void Rotate()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {   
        if(movingLaser)
            Rotate();
    }

    void OnEnable()
    {
        transform.rotation = Quaternion.identity;
        ChooseOrientation();
    }
}
