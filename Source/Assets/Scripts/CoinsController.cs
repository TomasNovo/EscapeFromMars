using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using SimpleJSON;

public class CoinsController : MonoBehaviour
{
    public static int coins_value = 0;
    

    private List<CoinsEntry> coins_list;

    // Text coins;
    TextMeshProUGUI coins;
    bool updated = false;

     // Start is called before the first frame update
    void Start()
    {
        coins = GetComponent<TextMeshProUGUI>();
    }

    void UpdateSerializable()
    {
        AddEntry(coins_value);
        updated = true;
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = "" + coins_value;

        if(!updated)
        {
            if(GameController.instance.gameOver)
            {
                UpdateSerializable();
            }
        }
        
    }


    private void AddEntry(int value)
    {
        CoinsEntry ce = new CoinsEntry {value = value}; 

        string path = Application.dataPath + "/Data/Coins.json";
        string jsonString = File.ReadAllText(path);
        
        Coins c = JsonUtility.FromJson<Coins>(jsonString);
        
        c.coins_list.Add(ce);

        string json = JsonUtility.ToJson(c);

        File.WriteAllText(path, json);
    }

    public class Coins
    {
        public List<CoinsEntry> coins_list;
    }

    [System.Serializable]
    public class CoinsEntry
    {
        //image 
        public int value;
    }
}
