using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public int capturedFoodCount;
    public List<Sprite> foodSprites;
    public List<GameObject> foodObjects;
    public List<float> foodSpeeds;

    void Start ()
    {

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
        if(foodObjects.Count < 10)
        {
            GameObject newFood = new GameObject();

            // Give the food object a sprite
            newFood.AddComponent(typeof(SpriteRenderer));
            int randNum = Random.Range(0, foodSprites.Count);
            newFood.GetComponent<SpriteRenderer>().sprite = foodSprites[randNum];
            
            // Assign the food a random X value so that they scatter as they fall
            float randX = Random.Range(-20.0f, 20.0f);
            newFood.transform.position = new Vector3(randX, 9.0f, 0.0f);

            // Give the food object a collider to be able to increment score
            newFood.AddComponent(typeof(PolygonCollider2D));
            newFood.AddComponent(typeof(Rigidbody2D));

            // Give the food object a "Food" tag to use for scoring
            newFood.tag = "Food";

            // Give the food object a speed
            float randY = Random.Range(-20.0f, 0.0f);
            foodSpeeds.Add(randY);

            // Add the food object to the list of 'active' falling food objects
            foodObjects.Add(newFood);
        }

    }

    void MoveFoodObjects()
    {
        for(int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, foodSpeeds[i]));
        }
    }

    void CheckCollisions()
    {
        // Destroy food objects that fall to the floor and aren't caught
        for(int i = 0; i < foodObjects.Count; i++)
        {
            if(foodObjects[i].transform.position.y < -10.0f)
            {
                GameObject fallenFood = foodObjects[i];
                foodObjects.Remove(fallenFood);
                foodSpeeds.Remove(i);
                Destroy(fallenFood);
            }
        }
    }
}
