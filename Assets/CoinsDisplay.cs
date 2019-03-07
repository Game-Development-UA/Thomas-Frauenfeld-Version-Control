using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinsDisplay : MonoBehaviour {

    private int coins = 0;
    public Text coinsText;
	
	void Update () {
        coinsText.text = "COINS : " + coins;
	}
}
