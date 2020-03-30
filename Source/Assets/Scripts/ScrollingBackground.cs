using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(GameController.instance.gameSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(GameController.instance.gameSpeed, 0);
    }
}
