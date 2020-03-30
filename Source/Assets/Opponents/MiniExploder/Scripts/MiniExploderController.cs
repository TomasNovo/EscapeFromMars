using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class MiniExploderController : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rb;
    private int counter;
    private bool collision;
    bool r = false, g = false, b = false;
    private Animator anim;
    private Vector3 memorySize;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        collision = false;
        anim = GetComponent<Animator>();
        GetColour();
        rb.velocity = -speed * transform.right;
        memorySize = gameObject.transform.localScale;
    }

    void GetColour()
    {
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        SettingsScript.SelectedItems it = JsonUtility.FromJson<SettingsScript.SelectedItems>(jsonString);

        //Debug.Log(it.item_entry_list.Count);
        for (int i = 0; i < it.item_entry_list.Count; i++)
        {
            switch (it.item_entry_list[i].name)
            {
                case "me1":
                    //  Debug.Log("RED");
                    r = true;
                    break;

                case "me2": //blue
                    //Debug.Log("BLUE");
                    b = true;
                    break;

                case "me3": //green
                    //Debug.Log("GREEN");
                    g = true;
                    break;

                default: break;
            }
        }
        checkBooleans();
    }

    void checkBooleans()
    {
        if (r)
        {
            anim.SetBool("blue", false);
            anim.SetBool("green", false);
        }
        else if (b)
        {
            anim.SetBool("blue", true);
            anim.SetBool("green", false);
        }
        else if (g)
        {
            anim.SetBool("blue", false);
            anim.SetBool("green", true);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (string.Equals("Hero", col.gameObject.name))
        {
            speed = 0;
            anim.SetBool("collided", true);
            gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 1f);
            collision = true;
            active = true;
        }
    }

    void Update()
    {
        if (collision)
        {
            counter++;
            if (counter == 40)
            {
                gameObject.SetActive(false);
            }
            else if (counter >= 40)
            {
                counter = 0;
                collision = false;
            }
        }
    }

    void OnEnable()
    {
        checkBooleans();
        if (active)
        {
            gameObject.transform.localScale = memorySize;
        }
        collision = false;
        speed = 3f;
        rb.velocity = -speed * transform.right;
    }
}
