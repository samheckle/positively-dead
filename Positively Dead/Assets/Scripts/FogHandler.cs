using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    public GameObject player;
    public float solidDistance;

    private Color fogColor;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fogColor = this.GetComponent<SpriteRenderer>().color;
        solidDistance = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust opacity based on distance from palyer
        float distanceFromPlayer = Vector3.Distance(this.transform.position, player.transform.position);

        if (distanceFromPlayer < solidDistance)
        {
            if (distanceFromPlayer <= 1.25f)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, (distanceFromPlayer * .2f));
            }
        }

        // Adjust opacity based on distance from torches
        GameObject torch = GameObject.FindGameObjectWithTag("Torch");
        if (torch != null)
        {
            float distanceFromTorch = Vector3.Distance(this.transform.position, torch.transform.position);

            if (distanceFromTorch < 5.0f)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, (distanceFromTorch * .2f));
            }
        }
    }
}