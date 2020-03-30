using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class BoughtItemsScript : MonoBehaviour
{
    private List<BoughtItemEntry> bought_entry_list;
    public static bool bought = false;


    public static void AddEntry(string name)
    {
        BoughtItemEntry ie = new BoughtItemEntry {name = name}; 
        
        string path = Application.dataPath + "/Data/BoughtItems.json";
        string jsonString = File.ReadAllText(path);

        BoughtItems i = JsonUtility.FromJson<BoughtItems>(jsonString);
        i.bought_entry_list.Add(ie);

        string json = JsonUtility.ToJson(i);
        File.WriteAllText(path, json);
        bought = true;
    }

    private void RemoveEntry(int index)
    {
        string path = Application.dataPath + "/Data/BoughtItems.json";
        string jsonString = File.ReadAllText(path);
        
        BoughtItems it = JsonUtility.FromJson<BoughtItems>(jsonString);   

        for(int i = 0; i < it.bought_entry_list.Count; i++)
        {
            if(i == index)
            {
                it.bought_entry_list.Remove(it.bought_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(it);
        File.WriteAllText(path, json);
    }

    public static void RemoveEntryByName(string name)
    {
        string path = Application.dataPath + "/Data/BoughtItems.json";
        string jsonString = File.ReadAllText(path);

        BoughtItems it = JsonUtility.FromJson<BoughtItems>(jsonString);   

        for(int i = 0; i < it.bought_entry_list.Count; i++)
        {
            if(it.bought_entry_list[i].name == name)
            {
                it.bought_entry_list.Remove(it.bought_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(it);
        File.WriteAllText(path, json);
    }

    public class BoughtItems
    {
        public List<BoughtItemEntry> bought_entry_list;
    }

    [System.Serializable]
    public class BoughtItemEntry
    {
        public string name;
    }
}
