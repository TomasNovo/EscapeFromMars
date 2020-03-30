using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredatorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Image bar;
    public float predatorHP = 10f;
    public float max_hp = 10f;

    private bool isDead = false;

    public float speed = 1f;
    public Rigidbody2D rb;
    public GameObject hero;

    public float idlePosition = 3f;
    private Animator anim;
    private bool idllePos = false;
    private float minDistance = 0.02f;
    private float delayBetweenShots = 3;
    private float lastShotTime = 0;


    private EnemyShot shootGenerator;

    // LIFE UI
    // Image life;
    // public float max_time = 5f; -> max_life 
    // float time_left; -> life left



    void Start()
    {        
        hero = GameObject.Find("Hero");
        anim = GetComponent<Animator>();
        rb.velocity = -speed * transform.right;
        shootGenerator = FindObjectOfType<EnemyShot>();
        lastShotTime = Time.time;
    }

    void EnemyShot()
    {
        if((Time.time - lastShotTime) >= delayBetweenShots)
        {
            lastShotTime = Time.time;
            shootGenerator.Shot(new Vector2(transform.position.x - 0.4f, transform.position.y));
        }
    }

    void MovePredator(){
        float playerPosY = hero.transform.position.y;

        EnemyShot();

        if(Mathf.Abs(playerPosY-transform.position.y) >= minDistance){
            if(playerPosY > transform.position.y){
                rb.velocity = speed * transform.up;
            }
            else{
                rb.velocity = -speed * transform.up;
            }
        }
        else {
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate(){
        if(idllePos && !isDead){
            MovePredator();
        }
        if(transform.position.x < idlePosition){
            rb.velocity = new Vector2(0, rb.velocity.y);
            idllePos = true;
        }

    }
    // Update is called once per frame
    void Update()
    {  
        if(isDead){
            Debug.Log("Destroy");
            GameController.instance.predatorDead = true;
            Destroy(gameObject, 1.5f);
        }

    }

    void OnTriggerEnter2D(Collider2D col) //enemy gets hurt by bullet
    {
        if(string.Equals("Bullet(Clone)", col.gameObject.name))
        {
            predatorHP--; 
            if(predatorHP > 0)
            {     
                bar.fillAmount = predatorHP / max_hp;
            }
            else if(predatorHP == 0)
            {
                bar.fillAmount = 0;
                anim.SetBool("0hp", true);
                isDead = true;
                rb.velocity = Vector2.zero;
            }

            
            //anim.SetBool("collided", true); hurt animation?
        }
        else if(string.Equals("Hero", col.gameObject.name)){
            // PlayerController.instance.anim.SetBool("dead_floor", true);
            // PlayerController.anim.SetBool("dead_flying", true);
        }
    }
}
