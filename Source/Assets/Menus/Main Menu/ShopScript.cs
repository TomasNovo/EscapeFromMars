using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SimpleJSON;


public class ShopScript : MonoBehaviour
{
    private Transform container;
    private Transform template;

    private List<Transform> item_transform_list;

    public TextMeshProUGUI coins;
    private int coins_serializable_value;

    // Items
    public Sprite machine_gun;
    public Sprite background_1;
    public Sprite background_2;
    public Sprite mini_exploder_blue;
    public Sprite mini_exploder_green;
    public Sprite bullet_blue;
    public Sprite bullet_black;
 

    // Start is called before the first frame update
    void Awake()
    {
        GetCoins();

        coins.text = coins_serializable_value.ToString();

        container = transform.Find("Container");
        template = container.Find("Template");

        template.gameObject.SetActive(false);

        // AddEntry(1,"Test");
        // RemoveEntryByName("test2");

        
        string path = Application.dataPath + "/Data/ShopItems.json";
        string jsonString = File.ReadAllText(path);
        
        Items it = JsonUtility.FromJson<Items>(jsonString);


        // Sort by price
        for(int i = 0; i < it.item_entry_list.Count; i++)
        {
            for(int j = i +1; j < it.item_entry_list.Count; j++)
            {
                if(it.item_entry_list[j].price > it.item_entry_list[i].price)
                {
                    ItemEntry temp = it.item_entry_list[i];
                    it.item_entry_list[i] = it.item_entry_list[j];
                    it.item_entry_list[j] = temp;
                }
            }
        }

        item_transform_list = new List<Transform>();
        foreach (ItemEntry ie in it.item_entry_list)
        {
            CreateItemEntry(ie, container, item_transform_list);
        }

    }

    public void Buy()
    {
        // Button name
        string name = EventSystem.current.currentSelectedGameObject.name;
        
        // Items
        string path = Application.dataPath + "/Data/ShopItems.json";
        string jsonString = File.ReadAllText(path);
        Items it = JsonUtility.FromJson<Items>(jsonString);

        // Coins
        string path2 = Application.dataPath + "/Data/Coins.json";
        string jsonString2 = File.ReadAllText(path2);
        CoinsController.Coins c = JsonUtility.FromJson<CoinsController.Coins>(jsonString2);    
        
        // Buy item
        for(int i = 0; i < it.item_entry_list.Count; i++)
        {
            if(name == it.item_entry_list[i].name)
            {
                if(coins_serializable_value >= it.item_entry_list[i].price)
                {
                    // Update coins_serializable_value
                    int value = -it.item_entry_list[i].price; 
                    CoinsController.CoinsEntry ce = new CoinsController.CoinsEntry {value = value}; 
                    c.coins_list.Add(ce);
                    
                    string json = JsonUtility.ToJson(c);
                    File.WriteAllText(path2, json);

                    // Update interface
                    GetCoins();
                    coins.text = coins_serializable_value.ToString();

                    // Update button
                    EventSystem.current.currentSelectedGameObject.SetActive(false);

                    // Serialize item 
                    BoughtItemsScript.AddEntry(name);

                    string jsonString3 = File.ReadAllText(Application.dataPath + "/Data/BoughtItems.json");
                    

                    //Debug.Log("Updated B: " + jsonString3);
                }
                else 
                {
                    //Debug.Log("NO money");
                    EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "No money";
                    
                    Invoke("changeButton",1);       
                }
            }
        }
    }
    
    public void changeButton()
    {
        EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Buy";
    }

    private void CreateItemEntry(ItemEntry ie, Transform c, List<Transform> tl)
    {
        float template_height = 100f;
        Transform t = Instantiate(template, c);
        RectTransform rt = t.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -template_height * tl.Count);
        t.gameObject.SetActive(true);

        int position = tl.Count+1;
        Sprite s;
        switch (position)
        {
            case 1: 
                s = machine_gun;
                break;

            case 2: 
                s = background_1;
                break;

            case 3: 
                s = background_2;
                break;

            case 4: 
                s = mini_exploder_blue;
                // s.transform.localScale = new Vector2(0.5f, 0.5f);
                break;

            case 5: 
                s = mini_exploder_green;
                break;

            case 6: 
                s = bullet_blue;
                break;

            case 7: 
                s = bullet_black;
                break;

            // case 3: 
            //     rankString = "3RD";
            //     break;

            default:
                s = machine_gun;
                break;
        }

        t.Find("i").GetComponent<Image>().sprite = s;
        
        int price = ie.price;
        
        t.Find("s").GetComponent<Text>().text = price.ToString();
        
        string name = ie.name;
        t.Find("n").GetComponent<Text>().text = name;

        t.Find("Button").GetComponent<Button>().name = name;
        // Debug.Log(t.Find("Button").GetComponent<Button>());

        // Dont draw button is item is already bought
        string path = Application.dataPath + "/Data/BoughtItems.json";
        string jsonString = File.ReadAllText(path);
        BoughtItemsScript.BoughtItems it = JsonUtility.FromJson<BoughtItemsScript.BoughtItems>(jsonString);   

        

        // if(it.bought_entry_list.Contains(name))

        for(int i = 0; i < it.bought_entry_list.Count; i++)
        {
            if(name == it.bought_entry_list[i].name)
            {
                t.Find(it.bought_entry_list[i].name).GetComponent<Button>().gameObject.SetActive(false);
            }
        }

        tl.Add(t);
    }

    private void GetCoins()
    {
        string path = Application.dataPath + "/Data/Coins.json";
        string jsonString = File.ReadAllText(path);
        CoinsController.Coins c = JsonUtility.FromJson<CoinsController.Coins>(jsonString);   
        int v = 0;

        for(int i = 0; i < c.coins_list.Count; i++)
        {
            v +=  c.coins_list[i].value;
        }
        
        coins_serializable_value = v;
    }


    private void AddEntry(int price, string name)
    {
        ItemEntry ie = new ItemEntry {price = price, name = name}; 
        
        string path = Application.dataPath + "/Data/ShopItems.json";
        string jsonString = File.ReadAllText(path);
        
        Items i = JsonUtility.FromJson<Items>(jsonString);
        
        i.item_entry_list.Add(ie);

        string json = JsonUtility.ToJson(i);
        File.WriteAllText(path, json);
        
    }

    private void RemoveEntry(int index)
    {
        string path = Application.dataPath + "/Data/ShopItems.json";
        string jsonString = File.ReadAllText(path);
        
        Items it = JsonUtility.FromJson<Items>(jsonString);   

        for(int i = 0; i < it.item_entry_list.Count; i++)
        {
            if(i == index)
            {
                it.item_entry_list.Remove(it.item_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(it);
        File.WriteAllText(path, json);
    }

    private void RemoveEntryByName(string name)
    {
        string path = Application.dataPath + "/Data/ShopItems.json";
        string jsonString = File.ReadAllText(path);
        
        Items it = JsonUtility.FromJson<Items>(jsonString);   

        for(int i = 0; i < it.item_entry_list.Count; i++)
        {
            if(it.item_entry_list[i].name == name)
            {
                it.item_entry_list.Remove(it.item_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(it);
        File.WriteAllText(path, json);
    }

    private class Items
    {
        public List<ItemEntry> item_entry_list;
    }

    [System.Serializable]
    private class ItemEntry
    {
        //image 
        public int price;
        public string name;
    }
}
