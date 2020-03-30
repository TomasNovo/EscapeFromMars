using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using SimpleJSON; 

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public Rigidbody2D rb; 
    public Sprite sprite1; 
    public Sprite sprite2; 
    public Sprite sprite3; 

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = bulletSpeed * transform.right;
        SelectBullet();
    }

    private void SelectBullet(){        
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);

        SettingsScript.SelectedItems it = JsonUtility.FromJson<SettingsScript.SelectedItems>(jsonString);

        for (int i = 0; i < it.item_entry_list.Count; i++)
        {
           // Debug.Log(it.item_entry_list[i].ToString());
            switch (it.item_entry_list[i].name)
            {
                case "bu1":
                    GetComponent<SpriteRenderer>().sprite = sprite1;
                    break;
                case "bu2":
                    GetComponent<SpriteRenderer>().sprite = sprite2;
                    break;
                case "bu3":
                    GetComponent<SpriteRenderer>().sprite = sprite3;
                    break;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D hit_info)
    {
        gameObject.SetActive(false);
    }

    void OnEnable() //when SetActive(true) is called
    {
        rb.velocity = bulletSpeed * transform.right;
    }

}
