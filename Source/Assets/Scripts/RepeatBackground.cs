using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using SimpleJSON;   

public class RepeatBackground : MonoBehaviour
{
    private float backgroundLength;
    private BoxCollider2D backgroundCollider;
    public Sprite sprite1; 
    public Sprite sprite2; 
    public Sprite sprite3;
    // Start is called before the first frame update
    void Start()
    {
        backgroundCollider = GetComponent<BoxCollider2D>();
        backgroundLength = backgroundCollider.size.x;
        SelectBackground();
    }

    private void SelectBackground(){        
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);

        SettingsScript.SelectedItems it = JsonUtility.FromJson<SettingsScript.SelectedItems>(jsonString);

        for (int i = 0; i < it.item_entry_list.Count; i++)
        {
           // Debug.Log(it.item_entry_list[i].ToString());
            switch (it.item_entry_list[i].name)
            {
                case "b1":
                    GetComponent<SpriteRenderer>().sprite = sprite1;
                    break;
                case "b2":
                    GetComponent<SpriteRenderer>().sprite = sprite2;
                    break;
                case "b3":
                    GetComponent<SpriteRenderer>().sprite = sprite3;
                    break;
            }
        }
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
