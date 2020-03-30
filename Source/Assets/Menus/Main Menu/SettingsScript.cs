using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.IO;
using SimpleJSON;

using UnityEngine.Audio;

public class SettingsScript : MonoBehaviour
{
    public Slider music_volume;

    private Transform container;
    private Transform template;

    private List<GameObject> checks;
    private List<GameObject> locks;
    private float soundValue;

    //Music functions
    void OnEnable()
    {
        music_volume.onValueChanged.AddListener(delegate {OnMusicVolumeChange();});
        UpdateSlider();
    }

    public void OnMusicVolumeChange()
    {
       SaveSound();
       UpdateSound();
    }

    void UpdateSound(){
        GameObject.Find("Main Camera").GetComponent<AudioSource>().volume = SettingsScript.GetSoundEntry();
    }
    // Start is called before the first frame update
    void Awake()
    {
        container = transform.Find("Container");
        template = container.Find("Template");
        soundValue = 0.5f;
        GetChecks();
        GetLocks();
        UpdateSlider();
        DisableChecksNotDefault();
        GetSelectedChecks();
        RemoveLock();
    }

    void Update()
    {
        if(BoughtItemsScript.bought) RemoveLock();
        BoughtItemsScript.bought = false;
    }

    void SaveSound(){
//        Debug.Log(music_volume.value);
        AddSoundEntry(music_volume.value);
        RemoveSoundEntry(0);
    }

    void UpdateSlider(){
        music_volume.value = GetSoundEntry();
    }

    void GetSelectedChecks()
    {
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        
        SelectedItems i = JsonUtility.FromJson<SelectedItems>(jsonString);

        for(int j = 0; j < i.item_entry_list.Count; j++)
        {
            switch(i.item_entry_list[j].name)
            {
                case "b1":
                    checks[0].SetActive(true);
                    checks[1].SetActive(false);
                    checks[2].SetActive(false);
                    break;
                
                case "b2":
                    checks[0].SetActive(false);
                    checks[1].SetActive(true);
                    checks[2].SetActive(false);
                    break;

                case "b3":
                    checks[0].SetActive(false);
                    checks[1].SetActive(false);
                    checks[2].SetActive(true);
                    break;

                case "m1":
                    checks[3].SetActive(true);
                    checks[4].SetActive(false);
                    checks[5].SetActive(false);
                    break;
                
                case "m2":
                    checks[3].SetActive(false);
                    checks[4].SetActive(true);
                    checks[5].SetActive(false);
                    break;

                case "m3":
                    checks[3].SetActive(false);
                    checks[4].SetActive(false);
                    checks[5].SetActive(true);
                    break;

                case "bu1":
                    checks[6].SetActive(true);
                    checks[7].SetActive(false);
                    checks[8].SetActive(false);
                    break;
                
                case "bu2":
                    checks[6].SetActive(false);
                    checks[7].SetActive(true);
                    checks[8].SetActive(false);
                    break;

                case "bu3":
                    checks[6].SetActive(false);
                    checks[7].SetActive(false);
                    checks[8].SetActive(true);
                    break;

                case "mg1":
                    checks[9].SetActive(true);
                    checks[10].SetActive(false);
                    break;

                case "mg2":
                    checks[10].SetActive(true);
                    checks[9].SetActive(false);
                    break;

                default: break;
            }
        }
    }

    void RemoveLock()
    {
       string path = Application.dataPath + "/Data/BoughtItems.json";
       string jsonString = File.ReadAllText(path);

       BoughtItemsScript.BoughtItems it = JsonUtility.FromJson<BoughtItemsScript.BoughtItems>(jsonString);   
      
       for(int i = 0; i < it.bought_entry_list.Count; i++)
       {

           switch(it.bought_entry_list[i].name)
           {
                case "Background 1": 
                    locks[0].SetActive(false);
                    break;

                case "Background 2": 
                    locks[1].SetActive(false);
                    break;

                 case "Blue MiniExploders": 
                    locks[2].SetActive(false);
                    break;

                case "Green MiniExploders": 
                    locks[3].SetActive(false);
                    break;
                
                case "Black Bullet": 
                    locks[4].SetActive(false);
                    break;

                case "Blue Bullet": 
                    Debug.Log("COMPROU BB");
                    locks[5].SetActive(false);
                    break;
                
                case "Laser Machine Gun": 
                    locks[6].SetActive(false);
                    break;

                default:
                    break;
           }
       }

    //    BoughtItemsScript.bought = false;

    }

    void GetChecks()
    {
        checks = new List<GameObject>();
        
        checks.Add(GameObject.Find("check_b1"));
        checks.Add(GameObject.Find("check_b2"));
        checks.Add(GameObject.Find("check_b3"));
        
        checks.Add(GameObject.Find("check_me1"));
        checks.Add(GameObject.Find("check_me2"));
        checks.Add(GameObject.Find("check_me3"));

        checks.Add(GameObject.Find("check_bu1"));
        checks.Add(GameObject.Find("check_bu2"));
        checks.Add(GameObject.Find("check_bu3"));

        checks.Add(GameObject.Find("check_mg1"));
        checks.Add(GameObject.Find("check_mg2"));
    }

    void GetLocks()
    {
        locks = new List<GameObject>();
       
        locks.Add(GameObject.Find("pl_b2"));
        locks.Add(GameObject.Find("pl_b3"));
        locks.Add(GameObject.Find("pl_me2"));
        locks.Add(GameObject.Find("pl_me3"));
        locks.Add(GameObject.Find("pl_bu2")); 
        locks.Add(GameObject.Find("pl_bu3"));
        locks.Add(GameObject.Find("pl_mg2"));
        
    }

    void DisableChecksNotDefault()
    {
        for(int i = 0; i < checks.Count; i++)
        {
            string name = checks[i].name;

            if(name[name.Length - 1] == '2' || name[name.Length - 1] == '3')
            {
                // Debug.Log("Disabled " + checks[i]);
                checks[i].SetActive(false);
            }
        }
    }

    private void AddEntry(string name)
    {
        SelectedItemEntry ie = new SelectedItemEntry {name = name}; 
        
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        
        SelectedItems i = JsonUtility.FromJson<SelectedItems>(jsonString);

        
        i.item_entry_list.Add(ie);

        string json = JsonUtility.ToJson(i);
        File.WriteAllText(path, json);
    }

    private void RemoveEntry(int index)
    {
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        
        SelectedItems it = JsonUtility.FromJson<SelectedItems>(jsonString);   

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
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        
        SelectedItems it = JsonUtility.FromJson<SelectedItems>(jsonString);   

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

    private bool CheckIfEntryExists(string name)
    {
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);
        
        SelectedItems it = JsonUtility.FromJson<SelectedItems>(jsonString);   

        for(int i = 0; i < it.item_entry_list.Count; i++)
        {
            if(it.item_entry_list[i].name == name)
            {
                return true;
            }
        }

        return false;
    }

    public class SelectedItems
    {
        public List<SelectedItemEntry> item_entry_list;
    }

    [System.Serializable]
    public class SelectedItemEntry
    {
        public string name;
    }



    // Check Buttons
    public void SelectBackground()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        switch(name)
        {
            case "button_b1":
                checks[0].SetActive(true);
                checks[1].SetActive(false);
                checks[2].SetActive(false);
                
                if(!CheckIfEntryExists("b1")) AddEntry("b1");
                RemoveEntryByName("b2");
                RemoveEntryByName("b3");
                break;

            case "button_b2":
                checks[0].SetActive(false);
                checks[1].SetActive(true);
                checks[2].SetActive(false);
                if(!CheckIfEntryExists("b2")) AddEntry("b2");
                RemoveEntryByName("b1");
                RemoveEntryByName("b3");
                break;

            case "button_b3":
                checks[0].SetActive(false);
                checks[1].SetActive(false);
                checks[2].SetActive(true);
                if(!CheckIfEntryExists("b3")) AddEntry("b3");
                RemoveEntryByName("b1");
                RemoveEntryByName("b2");
                break;

            default: break;
        }
    }

    public void SelectMiniExploders()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        switch(name)
        {
            case "button_me1":
                checks[3].SetActive(true);
                checks[4].SetActive(false);
                checks[5].SetActive(false);
                if(!CheckIfEntryExists("me1")) AddEntry("me1");
                RemoveEntryByName("me2");
                RemoveEntryByName("me3");
                break;

            case "button_me2":
                checks[3].SetActive(false);
                checks[4].SetActive(true);
                checks[5].SetActive(false);
                 if(!CheckIfEntryExists("me2")) AddEntry("me2");
                RemoveEntryByName("me1");
                RemoveEntryByName("me3");
                break;

            case "button_me3":
                checks[3].SetActive(false);
                checks[4].SetActive(false);
                checks[5].SetActive(true);
                if(!CheckIfEntryExists("me3")) AddEntry("me3");
                RemoveEntryByName("me1");
                RemoveEntryByName("me2");
                break;

            default: break;
        }
    }

    public void SelectBullets()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        switch(name)
        {
            case "button_bu1":
                checks[6].SetActive(true);
                checks[7].SetActive(false);
                checks[8].SetActive(false);
                if(!CheckIfEntryExists("bu1")) AddEntry("bu1");
                RemoveEntryByName("bu2");
                RemoveEntryByName("bu3");
                break;

            case "button_bu2":
                checks[6].SetActive(false);
                checks[7].SetActive(true);
                checks[8].SetActive(false);
                if(!CheckIfEntryExists("bu2")) AddEntry("bu2");
                RemoveEntryByName("bu1");
                RemoveEntryByName("bu3");
                break;

            case "button_bu3":
                checks[6].SetActive(false);
                checks[7].SetActive(false);
                checks[8].SetActive(true);
                if(!CheckIfEntryExists("bu3")) AddEntry("bu3");
                RemoveEntryByName("bu1");
                RemoveEntryByName("bu2");
                break;

            default: break;
        }
    }

    public void SelectMachineGun()
    {
        string name = EventSystem.current.currentSelectedGameObject.name;

        switch(name)
        {
            case "button_mg1":
                checks[9].SetActive(true);
                checks[10].SetActive(false);
                if(!CheckIfEntryExists("mg1")) AddEntry("mg1");
                RemoveEntryByName("mg2");
                break;

            case "button_mg2":
                checks[9].SetActive(false);
                checks[10].SetActive(true);
                if(!CheckIfEntryExists("mg2")) AddEntry("mg2");
                RemoveEntryByName("mg1");
                break;
            default: break;
        }
    }



    // Sound
    public class Sound
    {
        public List<SoundEntry> sound_entry_list;
    }

    [System.Serializable]
    public class SoundEntry
    {
        public float value;
    }

    private void AddSoundEntry(float value)
    {
        SoundEntry se = new SoundEntry {value = value}; 
        
        string path = Application.dataPath + "/Data/Sound.json";
        string jsonString = File.ReadAllText(path);
        
        Sound s = JsonUtility.FromJson<Sound>(jsonString);

        
        s.sound_entry_list.Add(se);

        string json = JsonUtility.ToJson(s);
        File.WriteAllText(path, json);
    }


    public static float GetSoundEntry()
    {
        string path = Application.dataPath + "/Data/Sound.json";
        string jsonString = File.ReadAllText(path);
        
        Sound s = JsonUtility.FromJson<Sound>(jsonString);  

        return s.sound_entry_list[0].value;
    }

    private void RemoveSoundEntry(int index)
    {
        string path = Application.dataPath + "/Data/Sound.json";
        string jsonString = File.ReadAllText(path);
        
        Sound s = JsonUtility.FromJson<Sound>(jsonString);  

        for(int i = 0; i < s.sound_entry_list.Count; i++)
        {
            if(i == index)
            {
                s.sound_entry_list.Remove(s.sound_entry_list[i]);
            }
        }

        string json = JsonUtility.ToJson(s);
        File.WriteAllText(path, json);
    }
}
