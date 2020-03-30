using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public Rigidbody2D rb;

    private Animator anim;
    private bool exploded = false;
    private double counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        bulletSpeed = GameController.instance.objectsSpeed * 3.5f;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = bulletSpeed * transform.right; //transform.left
    }

    void OnTriggerEnter2D(Collider2D hit_info)
    {

        if(string.Equals("Hero", hit_info.gameObject.name))
        {
            anim.SetBool("hit", true);
            exploded = true;
        }
    }

    void OnEnable() //when SetActive(true) is called
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpeed = GameController.instance.objectsSpeed * 3.5f;
        rb.velocity = bulletSpeed * transform.right; //transform.left
    }

    void Update()
    {
        if(exploded)
        {
            counter += 0.3;

            if(counter >= 0.5)
            {   
                anim.SetBool("exploded", true);
                Debug.Log("Delete enemy bullet");
                gameObject.SetActive(false);
            }
        }
        
        counter = 0;
        exploded = false;
        
    }
}
