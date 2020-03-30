using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpTime : MonoBehaviour
{
    public GameObject powerUp;
    Image timer_bar;

    public GameObject hero;
    public float max_time = 5f;
    float time_left;
    public static bool power_up_on = false;

    // Start is called before the first frame update
    void Start()
    {
        timer_bar = GetComponent<Image>();
        time_left = max_time;
    }


    void TurnOffPowerUp(string t)
    {
        switch(t)
        {
            case "shield":
            {
                ShieldController.active = false;
                break;
            }

            case "minimizer":
            {
                Debug.Log("Eiiii");
                CatchPowerUp.hero.transform.localScale = new Vector3(0.14f, 0.14f, 0.14f);
                break;
            }

            case "score_multiplier":
            {
                GameController.score_multiplier = 1;
                break;
            }

            case "coins_multiplier":
            {
                CoinCatchScript.multiplier = 1;
                break;
            }

            case "slow_game_speed":
            {
                GameController.instance.gameSpeed = -1.5f;
                GameController.instance.objectsSpeed = -1.5f;
                break;
            }

            // Bads 
            case "increase_game_speed":
            {
                GameController.instance.gameSpeed = -1.5f;
                GameController.instance.objectsSpeed = -1.5f;
                break;
            }

            case "gravity":
            {
                hero.GetComponent<PlayerController>().DecreaseGravity();
                break;
            }

            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(power_up_on)
        {
            if(time_left > 0)
            {
                time_left -= Time.deltaTime;
                timer_bar.fillAmount = time_left / max_time;
            }
            else
            {
                power_up_on = false;
                time_left = max_time;
                TurnOffPowerUp(CatchPowerUp.power_up);
                powerUp.SetActive(false);
                // Time.timeScale = 0;
            }
        }
    }
}
