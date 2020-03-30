using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatFloor : MonoBehaviour
{
    private float backgroundLength;
    private BoxCollider2D backgroundCollider;
    // Start is called before the first frame update
    void Start()
    {
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundLength = backgroundCollider.size.x;
    }

    private void MoveBackground(){
        Vector2 offset = new Vector2(backgroundLength * 2f, 0);
        //move
        transform.position = (Vector2)transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        //if one background is totally behind the scene
     if(transform.position.x < -backgroundLength){
         MoveBackground();
     }   

    }
}
