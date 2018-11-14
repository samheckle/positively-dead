using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogHandler : MonoBehaviour
{
    public GameObject player;
    public GameObject[] fogList;

    private List<int> clearedFogList;
    private Color fogColor;
    private Vector3 origin;
    private float solidDistance;
    private float distance;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        fogList = GameObject.FindGameObjectsWithTag("Fog");
        fogColor = this.GetComponent<SpriteRenderer>().color;
        solidDistance = 5f;
        clearedFogList = new List<int>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        ClearFog();
        
	}

    /// <summary>
    /// 
    /// </summary>
    void ClearFog()
    {
        distance = Vector3.Distance(this.transform.position, player.transform.position);

        for (int i = 0; i < fogList.Length; i++)
        {
            bool cleared = false;

            for(int j = 0; j < clearedFogList.Count; j++)
            {
                if (clearedFogList.Contains(i))
                {
                    cleared = true;
                }
            }

            if (distance < solidDistance)
            {
                this.GetComponent<SpriteRenderer>().color = new Color(fogColor.r, fogColor.g, fogColor.b, (distance * .5f));
            }
            else if (distance <= 0.1f)
            {
                clearedFogList.Add(i);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    void ResetFog()
    {

    }
}
