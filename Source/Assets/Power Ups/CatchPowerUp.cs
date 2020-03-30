using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatchPowerUp : MonoBehaviour
{
    public static GameObject hero;
    public GameObject powerUp;
    public GameObject type;
    public Text text;
    private Sprite good, bad;

    public static string power_up = "";

    string[] goods_array = {"minimizer", "shield", "score_multiplier", "coins_multiplier", "slow_game_speed"};
    string[] bads_array = {"increase_game_speed", "gravity", "spawn_mini_exploders"};
    public static bool reverse_minimizer = false;

    /*
    Power ups:
    - ficar gigante/encolher
    - imortalidade temporária -> shield
    - mais 1 vida quando o jogador morrer
    - multiplicador de pontos temporário // score
    - aumento/diminuição da velocidade do jogo
    
    
    - apanhar balas para poder disparar contra oponentes
    */

    private int GetRandomNumber(int minimum, int maximum)
    { 
        return Random.Range(minimum, maximum);
    }

    void Start()
    {
        hero = GameObject.Find("Hero");
        GameObject info = GameObject.Find("Info");
        powerUp = info.transform.Find("PowerUp").gameObject;
        type = powerUp.transform.Find("PowerImage").gameObject;
        text = powerUp.GetComponent<Text>();

    }

    private void SwitchPowerUp(string pw)
    {
        // GameController.instance.MiniExploderWaveSpawn();
        switch(pw)
        {
            // Goods 
            case "shield":
            {
                ShieldController.active = true;
                text.text = "Shield";
                break;
            }

            case "minimizer":
            {
                hero.transform.localScale = new Vector3(0.05f, 0.05f, 1f);
                text.text = "Minimizer";
                break;
            }

            case "score_multiplier":
            {
                GameController.score_multiplier = 2;
                text.text = "Score x2";
                break;
            }

            case "coins_multiplier":
            {
                CoinCatchScript.multiplier = 2;
                text.text = "Coins x2";
                break;
            }

            case "slow_game_speed":
            {
                
                GameController.instance.objectsSpeed = -0.1f;
                GameController.instance.gameSpeed = -0.1f;
                text.text = "Speed x0.5";
                break;
            }

            // Bads 
            case "increase_game_speed":       //incrementar velocidade asteroides e oponentes ?
            {
                GameController.instance.objectsSpeed = -3f;
                GameController.instance.gameSpeed = -3f;
                text.text = "Speed x2";
                break;
            }

            
            case "gravity":
            {
                hero.GetComponent<PlayerController>().Increasegravity();
                text.text = "Gravity";
                break;
            }

            case "spawn_mini_exploders":       //incrementar velocidade asteroides e oponentes ?
            {
                GameController.instance.MiniExploderWaveSpawn();
                text.text = "Exploders Wave";
                break;
            }

            default:
                break;
        }
    }

    //Function that randomizes the power_up
    void goods()
    {
        Sprite[] array = Resources.LoadAll<Sprite>("good_resource") ;
        type.GetComponent<Image>().sprite = array[2];

        int index = GetRandomNumber(0, goods_array.Length - 1); 

        power_up = goods_array[index];

        SwitchPowerUp(power_up);
    }

    void bads()
    {
        Sprite[] array = Resources.LoadAll<Sprite>("bad_resource") ;
        type.GetComponent<Image>().sprite = array[2];

        int index = GetRandomNumber(0, bads_array.Length - 1); 

        power_up = bads_array[index];

        SwitchPowerUp(power_up);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(string.Equals("Hero", col.gameObject.name))
        {
            
            PowerUpTime.power_up_on = true;
            
            
            powerUp.SetActive(true);
            if(string.Equals(gameObject.name, "good(Clone)")) goods();
            else if(string.Equals(gameObject.name, "bad(Clone)")) bads();
            

            Destroy(gameObject);
        }
    }

    // void Update()
    // {
    //     if(reverse_minimizer)
    //     {
    //         Debug.Log("Reverse")
    //         hero.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
    //         reverse_minimizer = false;    
    //     }
    // }
}
