using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour {

    public int speed;
    public GameObject player;

	void Update()
    {
        transform.position += Vector3.right * Time.deltaTime * speed;

        if (player.transform.position.x > 2.5f && player.transform.position.x < 2.75f)
        {
            while (player.transform.position.y > 0.6f)
            {
                transform.position += Vector3.down * Time.deltaTime * 0.5f;
            }
        }
    }
}
