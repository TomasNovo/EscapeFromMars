using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    public GameObject shield;

    public static bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        shield.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.LeftShift))
        // {
        //     active = true;

        if(active)
        {
            shield.SetActive(true);
        }
        else
        {
            shield.SetActive(false);
            active = false;
        }

        // }
    }

    public bool ActivateShield
    {
        get
        {
            return active;
        }
        set
        {
            active = value;
        }
    }
}
