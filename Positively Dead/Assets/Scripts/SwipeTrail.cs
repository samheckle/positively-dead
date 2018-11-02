using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeTrail : MonoBehaviour
{
    public List<GameObject> hitObjects;
    GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) || Input.GetMouseButton(0)))
        {
            this.GetComponent<TrailRenderer>().time = Mathf.Infinity;   // Keep the Trail on the screen for as long as the mouse/finger is clicking/touching the screen

            // Draw the trail of the click/touch
            // Code given by tutorial video for drawing lines to the screen in unity
            // Link: https://www.youtube.com/watch?v=cHVZ0SYIHkI
            Plane objPlane = new Plane(Camera.main.transform.forward * -1, this.transform.position);
            Ray mRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            if (objPlane.Raycast(mRay, out rayDistance))
                this.transform.position = mRay.GetPoint(rayDistance);

            // Raycast to collide with the tiles and create a list of tiles that the player has chosen to go over
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                // If this is the first tile in the list, check to see that it is where the player is
                if (hitObjects.Count == 0)
                {
                    // If they have the same Y (same row)
                    if (Mathf.Abs(hit.collider.gameObject.transform.position.y - player.transform.position.y) <= 0.1f)
                    {
                        if (Mathf.Abs(hit.collider.gameObject.transform.position.x - player.transform.position.x) <= 0.1f)
                        {
                            if (!hitObjects.Contains(hit.collider.gameObject))
                            {
                                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                                hitObjects.Add(hit.collider.gameObject);
                            }
                        }
                    }
                }
                else if (hitObjects.Count >= 1)// If there are other tiles already existing in the list, check to see if the next tile is adjacent to the previous
                {
                    // If they have the same Y (same row)
                    if (Mathf.Abs(hit.collider.gameObject.transform.position.y - hitObjects[hitObjects.Count - 1].transform.position.y) <= 0.1f)
                    {
                        if (Mathf.Abs(Mathf.Abs(hit.collider.gameObject.transform.position.x) - Mathf.Abs(hitObjects[hitObjects.Count - 1].transform.position.x)) <= 3.3f)
                        {
                            // If there are other tiles already existing in the list, check to see if the next tile is adjacent to the previous
                            if (!hitObjects.Contains(hit.collider.gameObject))
                            {
                                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                                hitObjects.Add(hit.collider.gameObject);
                            }
                        }
                    }
                    // If they have the same X (same column)
                    else if (Mathf.Abs(hit.collider.gameObject.transform.position.x - hitObjects[hitObjects.Count - 1].transform.position.x) <= 0.1f)
                    {
                        if (Mathf.Abs(Mathf.Abs(hit.collider.gameObject.transform.position.y) - Mathf.Abs(hitObjects[hitObjects.Count - 1].transform.position.y)) <= 3.3f)
                        {
                            // If there are other tiles already existing in the list, check to see if the next tile is adjacent to the previous
                            if (!hitObjects.Contains(hit.collider.gameObject))
                            {
                                Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
                                hitObjects.Add(hit.collider.gameObject);
                            }
                        }
                    }
                }

            }
        }
        else
        {
            // Clear the screen of the path, reset the hitObjects List and mvoe the player to the last valid tile they were holding their click on
            this.GetComponent<TrailRenderer>().time = 0;
            MovePlayer();
        }

    }

    /// <summary>
    /// Moves the player smoothly along the tiles that the player selected with their swipe
    /// </summary>
    void MovePlayer()
    {
        // Check if the player is not currently on the tile at the front of the list
        if (hitObjects.Count > 0)
        {
            if (player.transform.position != hitObjects[0].transform.position)
            {
                if (player.transform.position.x != hitObjects[0].transform.position.x)
                {
                    if (player.transform.position.x > hitObjects[0].transform.position.x)
                    {
                        player.transform.position -= new Vector3(2f * Time.deltaTime, 0.0f);
                    }
                    else
                    {
                        player.transform.position += new Vector3(2f * Time.deltaTime, 0.0f);
                    }
                }
                if (player.transform.position.y != hitObjects[0].transform.position.y)
                {
                    if (player.transform.position.y > hitObjects[0].transform.position.y)
                    {
                        player.transform.position -= new Vector3(0.0f, 2f * Time.deltaTime);
                    }
                    else
                    {
                        player.transform.position += new Vector3(0.0f, 2f * Time.deltaTime);
                    }
                }
            }

            // Remove the current front of the list whenever the player has reached that position.
            if (Vector3.Distance(player.transform.position,hitObjects[0].transform.position) <= 0.01f)
            {
                hitObjects.RemoveAt(0);
            }
        }
    }
}