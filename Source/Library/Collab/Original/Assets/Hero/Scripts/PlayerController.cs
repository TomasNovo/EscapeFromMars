using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float move_speed = 3;
    public GameObject hero;
    private bool collision; 
    
    // Coins
    // public GameObject coin;

    //Bullet
    public GameObject bullet;
    public Transform firePoint;
    private Shoot shootGenerator;

    //TODO   
    private Rigidbody2D body;
    public float jetPackForce = 3f;
    public float rotationSpeed = 3f;

    private Animator anim;
    private float moveInput;
    private bool flying = false;
    private Collider2D[] isOnGround = new Collider2D[1];

    [SerializeField]
    private float rorateBackSpeed = 3f;
    //box
    [SerializeField] //can change on UI
    private float boxLength;
    [SerializeField]
    private float boxHeight; 
    [SerializeField]
    private Transform groundPos;
    [SerializeField]
    private LayerMask groundLayer;


    // private bool shoot;
    public float shoot_time;
    private float shoot_time_counter;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetBool("onFloor", true);
        shootGenerator = FindObjectOfType<Shoot>(); 
    }

    void Shoot()
    {
        shootGenerator.Shot(new Vector2(hero.transform.position.x + 0.4f, hero.transform.position.y));
        FindObjectOfType<AudioManager>().Play("Shoot");
    }

    void HeroDie()
    {
    if (anim.GetBool("onFloor"))
        {
            // gameObject.transform.Translate(0f,-0.5f, 0f);
            anim.SetBool("dead_floor", true);
            GameController.instance.game_over_menu.SetActive(true);
        }
       else
        {
            anim.SetBool("dead_flying", true);
            GameController.instance.game_over_menu.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(string.Equals("MiniExploder(Clone)", col.gameObject.name) ||
            string.Equals("EnemyBullet(Clone)", col.gameObject.name))
        {
            HeroDie();
        }
    }

    void FixedUpdate() //updates at physics speed
    {


        isOnGround[0] = null;
        Physics2D.OverlapBoxNonAlloc(groundPos.position, new Vector2(boxLength, boxHeight), 0, isOnGround, groundLayer);
        if (isOnGround[0] && !flying)
        {
            body.velocity = new Vector2(moveInput * move_speed, body.velocity.y);
            body.freezeRotation = true;
            body.rotation = 0;
        }
        else if (flying)
        {
            Vector3 rotation = new Vector3(0, 0, -moveInput * rotationSpeed);
            transform.Rotate(rotation);
            body.freezeRotation = false;

            //Debug.Log("Force");
            body.AddForce(transform.rotation * Vector2.up * jetPackForce);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * rorateBackSpeed);


        }
                      /*      //limit rotation to -8 / 8 degrees
                    Vector3 euler = transform.eulerAngles;
                    if (euler.z > 180) euler.z = euler.z - 360;
                    euler.z = Mathf.Clamp(euler.z, -8, 8);
                    transform.eulerAngles = euler;*/
                    
                    if(body.rotation > 8){
                        body.rotation = 8;
                    }
                    if(body.rotation <= -8){
                        body.rotation = -8;
                    }
                    Debug.Log(body.rotation);
    }
    // Update is called once per frame
    void Update()
    {
        // CatchCoin();

        if(!GameController.instance.gameOver)
        {
            moveInput = Input.GetAxis("Horizontal");
            flying = Input.GetKey(KeyCode.Space);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindObjectOfType<AudioManager>().Play("JetPackUp");
        }



        if(Input.GetKeyDown(KeyCode.X))
        {
            GameController.instance.MiniExploderWaveSpawn();
        }

        /*
        // Right and Left
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * move_speed * Time.deltaTime, 0f, 0f));
        }
        
        // Up and Down
        if(Input.GetAxisRaw("Vertical") > 0.5f )
        // || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * move_speed * Time.deltaTime, 0f));
        }
        */

        // TESTING DEATH
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("dead_flying", true); 
            GameController.instance.gameOver = true;
            GameController.instance.game_over_menu.SetActive(true);
        }
        
        // Shoot
        if (Input.GetKeyDown(KeyCode.J))
        {
            // shoot_time_counter = shoot_time;
            // shoot = true;   
            anim.SetBool("flying_shoot", true);
            anim.SetBool("floor_shoot", true);
            Shoot();
        }
        else 
        {
            anim.SetBool("flying_shoot", false);
            anim.SetBool("floor_shoot", false);
        }

        // Transition running <-> flying
        if(hero.transform.position.y <= -1) 
        {
            anim.SetBool("onFloor", true);
        }
        else 
        {
            anim.SetBool("onFloor", false);
        }

        anim.SetFloat("moveY", Input.GetAxisRaw("Vertical"));

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundPos.position, new Vector2(boxLength, boxHeight));
    }
}
