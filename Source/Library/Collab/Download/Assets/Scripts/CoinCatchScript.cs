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
            gameObject.SetActive(false);
        }
    }
}
