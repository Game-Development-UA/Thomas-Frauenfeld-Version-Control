using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesCounterUI : MonoBehaviour {

    public GameObject player;
    public Text livesText;

    void Update()
    {
        livesText.text = "Lives : " + player.GetComponent<PlayerController>().lives;
    }
}
