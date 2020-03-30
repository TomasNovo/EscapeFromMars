using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using SimpleJSON;

public class LeaderBoardTable : MonoBehaviour
{
    private Transform container;
    private Transform template;

    private List<Transform> highscores_transform_list;
    
    void Awake()
    {
        container = transform.Find("Container");
        template = container.Find("Template");

        template.gameObject.SetActive(false);

        string path = Application.dataPath + "/Data/Highscores.json";
        string jsonString = File.ReadAllText(path);
        Highscores h = JsonUtility.FromJson<Highscores>(jsonString);

        SortHighscores(h);

        // Create JSON file
        // string path = Application.persistentDataPath + "/Highscores.json";
        // File.WriteAllText("Assets/Resources/Highscores.json", jsonString);


        highscores_transform_list = new List<Transform>();
        foreach (HighscoreEntry hs in h.highscores_entry_list)
        {
            CreateHighScoreEntry(hs, container, highscores_transform_list);
        }
    }

    private void CreateHighScoreEntry(HighscoreEntry he, Transform c, List<Transform> tl)
    {
        float template_height = 40f;
        Transform t = Instantiate(template, c);
        RectTransform rt = t.GetComponent<RectTransform>();
        rt.anchoredPosition = new Vector2(0, -template_height * tl.Count);
        t.gameObject.SetActive(true);

        int rank = tl.Count+1;
        string rankString;
        switch (rank)
        {
            case 1: 
                rankString = "1ST";
                break;

            case 2: 
                rankString = "2ND";
                break;

            case 3: 
                rankString = "3RD";
                break;

            default:
                rankString = rank + "TH";
                break;
        }

        t.Find("p").GetComponent<Text>().text = rankString;
        
        int score = he.score;
        
        t.Find("s").GetComponent<Text>().text = score.ToString();
        
        string name = he.name;
        t.Find("n").GetComponent<Text>().text = name;

        t.Find("background").gameObject.SetActive(rank % 2 == 1);

        if(rank == 1)
        {
            t.Find("p").GetComponent<Text>().color = Color.green;
            t.Find("s").GetComponent<Text>().color = Color.green;
            t.Find("n").GetComponent<Text>().color = Color.green;
        }

        tl.Add(t);

    }

    public static void SortHighscores(Highscores h)
    {

        string path = Application.dataPath + "/Data/Highscores.json";
        Debug.Log(h.highscores_entry_list.Count);
        for(int i = 0; i < h.highscores_entry_list.Count; i++)
        {
            for(int j = i +1; j < h.highscores_entry_list.Count; j++)
            {
                if(h.highscores_entry_list[j].score > h.highscores_entry_list[i].score)
                {
                    HighscoreEntry temp = h.highscores_entry_list[i];
                    h.highscores_entry_list[i] = h.highscores_entry_list[j];
                    h.highscores_entry_list[j] = temp;
                }
            }
        }

        string json = JsonUtility.ToJson(h);
        File.WriteAllText(path, json);
    }

    public static void AddEntry(int score, string name)
    {
        HighscoreEntry hse = new HighscoreEntry {score = score, name = name}; 

        string path = Application.dataPath + "/Data/Highscores.json";
        string jsonString = File.ReadAllText(path);
        Highscores h = JsonUtility.FromJson<Highscores>(jsonString);

        h.highscores_entry_list.Add(hse);

        string json = JsonUtility.ToJson(h);

        File.WriteAllText(path, json);
    }

    public static void RemoveEntry(int index)
    {
        string path = Application.dataPath + "/Data/Highscores.json";
        string jsonString = File.ReadAllText(path);

        Highscores h = JsonUtility.FromJson<Highscores>(jsonString);   

        for(int i = 0; i < h.highscores_entry_list.Count; i++)
        {
            if(i == index)
            {
                h.highscores_entry_list.Remove(h.highscores_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(h);
        File.WriteAllText(path, json);
    }

    public class Highscores
    {
        public List<HighscoreEntry> highscores_entry_list;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public int score;
        public string name;
    }

}
