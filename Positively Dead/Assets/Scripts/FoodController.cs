using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public List<Sprite> foodSprites; // the list of possible food objects that are available

    public List<GameObject> foodObjects; // the list of food objects that are currently falling on screen
    public List<float> foodSpeeds;

    public Dictionary<string, int> requiredFoodObjects; // the foods the player needs to catch to complete the round
    public int requiredScore;
    public int level;
    public int currentScore;


    void Start ()
    {
        level = 1; // start the game at level 1
        currentScore = 0;
        requiredScore = 0;
        requiredFoodObjects = new Dictionary<string, int>();
        SetObjective(); // set level 1's objective
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
            newFood.transform.position = new Vector3(randX, 12.0f, 0.0f);

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
        // Check the score to see if the player has completed the objective
        if(currentScore == requiredScore)
        {
            IncrementLevel();
        }


        // Destroy food objects that fall to the floor and aren't caught
        for(int i = 0; i < foodObjects.Count; i++)
        {
            if (foodObjects[i].transform.position.y < -9.0f)
            {
                GameObject fallenFood = foodObjects[i];
                foodObjects.Remove(fallenFood);
                foodSpeeds.Remove(i);
                Destroy(fallenFood);
            }
        }

        Text scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
    }

    void IncrementLevel()
    {
        level++;
        requiredFoodObjects.Clear(); // clear any objectives currently stored in the dictionary

        // Delete all falling food objects
        for (int i = 0; i < foodObjects.Count; i++)
        {
            GameObject food = foodObjects[i];
            foodObjects.Remove(food);
            foodSpeeds.Remove(i);
            Destroy(food);
        }

        // Re-center the player's basket
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0.0f, -8.5f, 0.0f);

        // Redetermine the objective to be harder (more food)
        SetObjective();
    }

    void SetObjective()
    {
        if (level == 1) // first level will have 2 food types
        {
            int firstFoodType = Random.Range(0, foodSprites.Count);
            int secondFoodType = Random.Range(0, foodSprites.Count);
            int firstFoodAmnt = Random.Range(2, 5);
            int secondFoodAmnt = Random.Range(2, 5);
            requiredScore = firstFoodAmnt + secondFoodAmnt;

            Sprite firstFood = foodSprites[firstFoodType];
            Sprite secondFood = foodSprites[secondFoodType];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "Objective: Collect " + firstFoodAmnt + " " + firstFood.name + "\nCollect " + secondFoodAmnt + " " + secondFood.name;
        }
        else if (level == 2) // second level will have 3 food types
        {
            int firstFoodType = Random.Range(0, foodSprites.Count);
            int secondFoodType = Random.Range(0, foodSprites.Count);
            int thirdFoodType = Random.Range(0, foodSprites.Count);
            int firstFoodAmnt = Random.Range(3, 6);
            int secondFoodAmnt = Random.Range(3, 6);
            int thirdFoodAmnt = Random.Range(3, 6);
            requiredScore = firstFoodAmnt + secondFoodAmnt + thirdFoodAmnt;

            Sprite firstFood = foodSprites[firstFoodType];
            Sprite secondFood = foodSprites[secondFoodType];
            Sprite thirdFood = foodSprites[thirdFoodType];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);
            requiredFoodObjects.Add(thirdFood.name, thirdFoodAmnt);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "Objective: Collect " + firstFoodAmnt + " " + firstFood.name + "\nCollect " + secondFoodAmnt + " " + secondFood.name + "\nCollect " + thirdFoodAmnt + " " + thirdFood.name;
        }
        else if (level == 3) // third level will have 4 food types
        {
            int firstFoodType = Random.Range(0, foodSprites.Count);
            int secondFoodType = Random.Range(0, foodSprites.Count);
            int thirdFoodType = Random.Range(0, foodSprites.Count);
            int fourthFoodType = Random.Range(0, foodSprites.Count);
            int firstFoodAmnt = Random.Range(5, 8);
            int secondFoodAmnt = Random.Range(5, 8);
            int thirdFoodAmnt = Random.Range(5, 8);
            int fourthFoodAmnt = Random.Range(5, 8);
            requiredScore = firstFoodAmnt + secondFoodAmnt + thirdFoodAmnt + fourthFoodAmnt;

            Sprite firstFood = foodSprites[firstFoodType];
            Sprite secondFood = foodSprites[secondFoodType];
            Sprite thirdFood = foodSprites[thirdFoodType];
            Sprite fourthFood = foodSprites[fourthFoodType];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);
            requiredFoodObjects.Add(thirdFood.name, thirdFoodAmnt);
            requiredFoodObjects.Add(fourthFood.name, fourthFoodAmnt);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "Objective: Collect " + firstFoodAmnt + " " + firstFood.name + "\nCollect " + secondFoodAmnt + " " + secondFood.name + "\nCollect " + thirdFoodAmnt + " " + thirdFood.name + "\nCollect " + fourthFoodAmnt + " " + fourthFood.name;
        }
    }
}
