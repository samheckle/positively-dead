using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{
    public GameObject player;

    // Use this for initialization
    void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        MovePlayer();
	}

    void MovePlayer()
    {
        if (Input.GetButton("Left"))
        {
            if (player.transform.position.x < -20.0f)
            {
                player.transform.position = new Vector3(-20.0f, 0.0f, 0.0f);
            }
            else
            {
                player.transform.position += new Vector3(-10.0f * Time.deltaTime, 0.0f, 0.0f);

            }
        }

        if (Input.GetButton("Right"))
        {
            if (player.transform.position.x > 20.0f)
            {
                player.transform.position = new Vector3(20.0f, 0.0f, 0.0f);
            }
            else
            {
                player.transform.position += new Vector3(10.0f * Time.deltaTime, 0.0f, 0.0f);
            }
        }

    }
}
