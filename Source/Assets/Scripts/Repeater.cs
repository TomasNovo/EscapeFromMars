using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll: MonoBehaviour
{

    Material material;
    Vector2 offset;

    public int velocity_x, velocity_y;

    private void Awake()
    {
       material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
	offset = new Vector2(velocity_x, velocity_y);        
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += offset * Time.deltaTime;
    }
}
