using UnityEngine;

/// <summary>
/// Author: Israel Anthony
/// Controls the Basket (Player)
/// </summary>
public class PlayerContoller : MonoBehaviour
{
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    /// <summary>
    /// Moves the player left and right
    /// </summary>
    void MovePlayer()
    {
        // Handles tilt controls with the accelerometer
        player.transform.Translate(Input.acceleration.x, 0.0f, 0.0f);

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