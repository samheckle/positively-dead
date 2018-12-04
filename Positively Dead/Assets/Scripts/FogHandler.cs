using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    public GameObject player;
    public float solidDistance;

    private Color fogColor;
    private Rigidbody2D fogRB;

    private Vector3 UP = new Vector3(0f, 1f);
    private Vector3 DOWN = new Vector3(0f, -1f);
    private Vector3 LEFT = new Vector3(-1f, 0f);
    private Vector3 RIGHT = new Vector3(1f, 0f);

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fogColor = this.GetComponent<SpriteRenderer>().color;
        fogRB = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust opacity based on distance from palyer
        float distanceFromPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceFromPlayer < 2.25f)
        {
            if(player.transform.up == UP)
            {
                fogRB.AddForce(new Vector2(0.5f, 15f));
            }
            else if (player.transform.up == DOWN)
            {
                fogRB.AddForce(new Vector2(-0.5f, -15f));
            }
            else if(player.transform.up == LEFT)
            {
                fogRB.AddForce(new Vector2(-15f, -0.5f));
            }
            else if (player.transform.up == RIGHT)
            {
                fogRB.AddForce(new Vector2(15f, 0.5f));
            }
        }

        // Adjust opacity based on distance from torches
        GameObject torch = GameObject.FindGameObjectWithTag("Torch");
        if (torch != null)
        {
            float distanceFromTorch = Vector3.Distance(this.transform.position, torch.transform.position);

            if (distanceFromTorch < 5f)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, 0.5f);
            }
            else
            {
                this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, 1f);
            }
        }
    }
}