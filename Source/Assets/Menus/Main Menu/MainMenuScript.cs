using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject lb;
    public GameObject shop;
    public GameObject options;

    // Start is called before the first frame update
    void Start()
    {
        lb.SetActive(false);
        shop.SetActive(false);
        options.SetActive(false);
        //Debug.Log("I: " + PlayerPrefs.GetString("items"));
       // Debug.Log("B: " + PlayerPrefs.GetString("bought"));
        
        FindObjectOfType<AudioManager>().Play("Stargazing");
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = SettingsScript.GetSoundEntry();
    }

    void Update () 
    {
    

        if (Input.GetMouseButtonDown(0)) 
        {
            RaycastHit  hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                  
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.name == "START" ) 
                {   
                    FindObjectOfType<AudioManager>().Stop("Stargazing");
                    Destroy(FindObjectOfType<AudioManager>());
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                else if (hit.transform.name == "SHOP" )
                {
                    if(!shop.active)
                    {
                        shop.SetActive(true);
                        lb.SetActive(false);
                        options.SetActive(false);
                    }
                    else
                    {
                        shop.SetActive(false);
                    }
                }
                else if (hit.transform.name == "LB" )
                {
                    if(!lb.active)
                    {
                        lb.SetActive(true);
                        shop.SetActive(false);
                        options.SetActive(false);
                    }
                    else
                    {
                        lb.SetActive(false);
                    }
                }
                else if (hit.transform.name == "SETTINGS" )
                {
                    if(!options.active)
                    {
                        options.SetActive(true);
                        shop.SetActive(false);
                        lb.SetActive(false);
                    }
                    else
                    {
                        options.SetActive(false);
                    }
                }
                else if (hit.transform.name == "EXIT" ) Application.Quit();
            }
        
        }
    }
}
