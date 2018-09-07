using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> foodObjects;
    public int foodCount;
    
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        SpawnFoodObjects();
        MoveFoodObjects();
        CheckCollisions();
	}

    void SpawnFoodObjects()
    {
        if(foodCount < 5)
        {
            GameObject newObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            newObj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
            foodObjects.Add(newObj);
            foodCount++;
        }
    }

    void MoveFoodObjects()
    {
        for(int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].transform.position += new Vector3(0.0f, -0.025f, 0.0f);
        }
    }

    void CheckCollisions()
    {
        // Destroy food objects that fall to the floor and aren't caught
        for(int i = 0; i < foodObjects.Count; i++)
        {
            if(foodObjects[i].transform.position.y < -5.0f)
            {
                GameObject fallenFood = foodObjects[i];
                foodObjects.Remove(fallenFood);
                Destroy(fallenFood);
            }
        }

        // "Collect" the food pieces in the basket
    }
}
