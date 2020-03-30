using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatchScript : MonoBehaviour
{
    public static int multiplier = 1;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(string.Equals("Hero", col.gameObject.name))
        {
            CoinsController.coins_value += 1 * multiplier;
            //Destroy(gameObject);
            FindObjectOfType<AudioManager>().Play("Coin");
           // Debug.Log(FindObjectOfType<AudioManager>().sounds[0].name);
           // Debug.Log(FindObjectOfType<AudioManager>().sounds[1].name);
           // Debug.Log(FindObjectOfType<AudioManager>().sounds[2].name);
           // Debug.Log(FindObjectOfType<AudioManager>().sounds[3].name);
            gameObject.SetActive(false);
        }
    }
}
