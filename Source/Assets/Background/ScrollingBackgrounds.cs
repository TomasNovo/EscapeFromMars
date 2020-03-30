using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackgrounds : MonoBehaviour
{

    float scrollSpeed = -5f;
    Vector2 startpos;

    private void Awake()
    {
        startpos = transform.position;
    }

    void Start()
    {
        //offset = new Vector2(velocity_x, velocity_y);
    }
    
    void Update()
    {
        float newpos = Mathf.Repeat(Time.time * scrollSpeed, 5);
        transform.position = startpos + Vector2.right * newpos;
    }
}
