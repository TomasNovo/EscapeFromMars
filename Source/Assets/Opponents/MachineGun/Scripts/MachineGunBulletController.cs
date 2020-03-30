using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGunBulletController : MonoBehaviour
{
    public float bulletSpeed = -5f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = GameController.instance.objectsSpeed * 3.5f;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.right; //transform.left
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col) //destroys bullet
    {
        if(string.Equals("Hero", col.gameObject.name))
        {
          gameObject.SetActive(false);
        }
    }

    void OnEnable() //when SetActive(true) is called
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpeed = GameController.instance.objectsSpeed * 3.5f;
        rb.velocity = bulletSpeed * transform.right; //transform.left
    }
}
