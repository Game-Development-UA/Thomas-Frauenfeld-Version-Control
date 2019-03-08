using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsCounterUI : MonoBehaviour {

    public GameObject player;
    public Text coinsText; 

	void Update()
    {
        coinsText.text = "Coins : " + player.GetComponent<PlayerController>().coins;
    }
}
