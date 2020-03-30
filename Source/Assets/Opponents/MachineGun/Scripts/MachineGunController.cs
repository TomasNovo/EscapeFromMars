using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using SimpleJSON;   

public class MachineGunController : MonoBehaviour
{

    private MachineGunShootGenerator shootGenerator;
    private float shotTimeDelta = 2;
    private float lastShotTime = 0;
    public Sprite sprite1; 
    public Sprite sprite2; 
    private float machineShotPos = -1f;
    public GameObject hero;
    // Start is called before the first frame update
    void Start()
    {
        hero = GameObject.Find("Hero");
        lastShotTime = shotTimeDelta;
        shootGenerator = FindObjectOfType<MachineGunShootGenerator>();
        SelectMachineGun();
    }

    private void SelectMachineGun(){
        string path = Application.dataPath + "/Data/SelectedItems.json";
        string jsonString = File.ReadAllText(path);

        SettingsScript.SelectedItems it = JsonUtility.FromJson<SettingsScript.SelectedItems>(jsonString);

        for (int i = 0; i < it.item_entry_list.Count; i++)
        {
           // Debug.Log(it.item_entry_list[i].ToString());
            switch (it.item_entry_list[i].name)
            {
                case "mg1":
                    GetComponent<SpriteRenderer>().sprite = sprite1;
                    break;
                case "mg2":
                    GetComponent<SpriteRenderer>().sprite = sprite2;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - lastShotTime) >= shotTimeDelta){
            if(hero.transform.position.y <= machineShotPos && hero.transform.position.x <= transform.position.x){
                FindObjectOfType<AudioManager>().Play("Cannon");
                shootGenerator.Spawn(new Vector2(transform.position.x - 0.3f, transform.position.y + 0.1f));
                lastShotTime = Time.time;
            }
        }
    }
}
