using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float solidDistance;

    private Color fogColor;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fogColor = this.GetComponent<SpriteRenderer>().color;
        solidDistance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (distance < solidDistance)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, (distance * .1f));
        }

        if (distance <= 0.2f)
        {
            Destroy(this.gameObject);
        }
    }
}