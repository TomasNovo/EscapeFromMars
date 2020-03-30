using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigLaserController : MonoBehaviour
{
    // Start is called before the first frame update
    private int timeToDestroy = 2;
    private int timeToActive = 2;
    private bool firstTime = true;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        //NoactiveTime();
    }

    void NoactiveTime(){
        GetComponent<Collider2D>().enabled = false;
        Invoke("ActiveLaser", timeToActive);
    }

    void ActiveLaser(){
        animator.SetBool("active", true);
        GetComponent<Collider2D>().enabled = true;
        Invoke("DestroyLaser", timeToDestroy);
    }

    void DestroyLaser(){
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }      
    void OnEnable()
    {   
        if(!firstTime){
            animator = GetComponent<Animator>();
            NoactiveTime();
        }
        else{
            firstTime = false;
        }
        
    }
}
