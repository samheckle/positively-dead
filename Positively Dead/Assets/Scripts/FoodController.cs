using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Author: Israel Anthony and JaJuan Webster
/// Food Controller for Norse Minigame
/// </summary>
public class FoodController : MonoBehaviour
{
    private Text scoreTxt, levelTxt;

    // Images of food
    private List<Image> foodImages;

    // Levels of Minigame
    private List<GameObject> levelObjects;

    // BASKET
    public GameObject player;

    // The list of possible food objects that are available
    public List<Sprite> foodSprites;

    // The list of food objects that are currently falling on screen
    public List<GameObject> foodObjects;

    public List<GameObject> levelOverObjects;

    public List<float> foodSpeeds;

    // The foods the player needs to catch to complete the round
    public Dictionary<string, int> requiredFoodObjects;

    // The foods the player needs to catch to complete the round
    public Dictionary<string, int> collectedFoodObjects;
    public int requiredScore;
    public int currentScore;
    public int level;

    void Start()
    {
        level = 0;
        currentScore = 0;
        requiredScore = 0;
        requiredFoodObjects = new Dictionary<string, int>();
        collectedFoodObjects = new Dictionary<string, int>();
        foodImages = new List<Image>();
        levelObjects = new List<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player");
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();

        for (int i = 1; i < 10; i++)
        {
            foodImages.Add(GameObject.FindGameObjectWithTag("Image" + i).GetComponent<Image>());
        }
        for (int i = 1; i < 4; i++)
        {
            levelObjects.Add(GameObject.FindGameObjectWithTag("Level" + i + "Images"));
        }

        levelTxt = levelOverObjects[0].GetComponent<Text>();

        // Start the game
        IncrementLevel();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFoodObjects();
        MoveFoodObjects();
        CheckCollisions();
        DisplayScore();

        // Check the score to see if the player has completed the objective
        if (level > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (currentScore == requiredScore)
        {
            IncrementLevel();
        }

        if (timer != 0)
            foreach (GameObject g in levelOverObjects)
                g.SetActive(true);
        else
            foreach (GameObject g in levelOverObjects)
                g.SetActive(false);
    }

    /// <summary>
    /// Spawns food objects at the top of screen
    /// </summary>
    void SpawnFoodObjects()
    {
        if (foodObjects.Count < 5)
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
            float randY = Random.Range(-250.0f, 0.0f);
            foodSpeeds.Add(randY);

            // Add the food object to the list of 'active' falling food objects
            foodObjects.Add(newFood);
        }
    }

    /// <summary>
    /// Adds forces to speed up the food that is falling down.
    /// </summary>
    void MoveFoodObjects()
    {
        for (int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, foodSpeeds[i]) * Time.deltaTime);
        }
    }

    /// <summary>
    /// Checks collisions between the food, basket and the floor
    /// </summary>
    void CheckCollisions()
    {
        // Destroy food objects that fall to the floor and aren't caught
        for (int i = 0; i < foodObjects.Count; i++)
        {
            if (foodObjects[i].transform.position.y < -10.0f)
            {
                GameObject fallenFood = foodObjects[i];
                foodObjects.Remove(fallenFood);
                foodSpeeds.Remove(i);
                Destroy(fallenFood);
            }
        }

        // Check player collision's against require food objects to update score
        for (int i = 0; i < foodObjects.Count; i++)
        {
            List<string> keyList = new List<string>(requiredFoodObjects.Keys);
            for (int j = 0; j < keyList.Count; j++)
            {
                if (foodObjects[i].GetComponent<PolygonCollider2D>().IsTouching(player.GetComponent<PolygonCollider2D>()))
                {
                    if (foodObjects[i].GetComponent<SpriteRenderer>().sprite.name.Equals(keyList[j]) && collectedFoodObjects[keyList[j]] < requiredFoodObjects[keyList[j]])
                    {
                        GameObject fallenFood = foodObjects[i];
                        foodObjects.Remove(fallenFood);
                        foodSpeeds.Remove(i);
                        Destroy(fallenFood);
                        collectedFoodObjects[keyList[j]] += 1;
                        currentScore++;
                    } else if(!keyList.Contains(foodObjects[i].GetComponent<SpriteRenderer>().sprite.name))
                    {
                        GameObject fallenFood = foodObjects[i];
                        foodObjects.Remove(fallenFood);
                        foodSpeeds.Remove(i);
                        Destroy(fallenFood);
                        if(currentScore > 0) currentScore--;
                        ClearCollection();
                    }
                }
            }
        }
    }

    void ClearCollection()
    {
        List<string> keyList = new List<string>(requiredFoodObjects.Keys);

        for (int i = 0; i < keyList.Count; i++)
        {
            collectedFoodObjects[keyList[i]] = 0;
        }
    }

    /// <summary>
    /// Displays current score to player
    /// </summary>
    void DisplayScore()
    {
        List<string> keyList = new List<string>(collectedFoodObjects.Keys);

        scoreTxt.text = "SCORE\n";

        for (int i = 0; i <= level; i++)
        {
            if (level > 3)
            {
                break;
            }

            // Changes the color of the required food objects once they are completed
            if (collectedFoodObjects[keyList[i]] == requiredFoodObjects[keyList[i]])
            {
                scoreTxt.text += "<color=#00C0FF>" + keyList[i] + ": " + collectedFoodObjects[keyList[i]] + "/" + requiredFoodObjects[keyList[i]] + "</color>";
            }
            else
            {
                scoreTxt.text += keyList[i] + ": " + collectedFoodObjects[keyList[i]] + "/" + requiredFoodObjects[keyList[i]];
            }

            if (i < level)
            {
                scoreTxt.text += " | ";
            }
        }
    }

    /// <summary>
    /// Clears the objectives and goes to the next level
    /// </summary>
    void IncrementLevel()
    {
        level++;

        if (level > 1)
        {
            // Freeze game for input
            StartCoroutine(WaitForKeyDown(KeyCode.KeypadEnter));
        }

        // clear any objective information currently stored in the dictionaries
        requiredFoodObjects.Clear();
        collectedFoodObjects.Clear();

        // Delete all current food objects
        for (int i = 0; i < foodObjects.Count; i++)
        {
            GameObject food = foodObjects[i];
            foodObjects.Remove(food);
            foodSpeeds.Remove(i);
            Destroy(food);
        }

        // Redetermine the objective to be harder (more food)
        SetObjective();
    }

    /// <summary>
    /// Sets the objective for the player depending on the current level that they are on
    /// </summary>
    void SetObjective()
    {
        if (level == 1) // first level will have 2 food types
        {
            levelObjects[1].SetActive(false);
            levelObjects[2].SetActive(false);

            List<int> foodTypes = RandomFoodListGenerator(2);

            int firstFoodAmnt = Random.Range(2, 5);
            int secondFoodAmnt = Random.Range(2, 5);
            requiredScore = firstFoodAmnt + secondFoodAmnt;

            Sprite firstFood = foodSprites[foodTypes[0]];
            Sprite secondFood = foodSprites[foodTypes[1]];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);
            collectedFoodObjects.Add(firstFood.name, 0);
            collectedFoodObjects.Add(secondFood.name, 0);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "OBJECTIVE\nCollect " + firstFoodAmnt + "       " + " | Collect " + secondFoodAmnt + "        ";

            switch (firstFood.name)
            {
                case "Apple":
                    foodImages[0].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[0].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[0].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[0].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[0].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[0].sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    foodImages[1].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[1].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[1].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[1].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[1].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[1].sprite = foodSprites[2];
                    break;
            }
        }
        else if (level == 2) // second level will have 3 food types
        {
            levelObjects[0].SetActive(false);
            levelObjects[1].SetActive(true);

            List<int> foodTypes = RandomFoodListGenerator(3);

            int firstFoodAmnt = Random.Range(3, 6);
            int secondFoodAmnt = Random.Range(3, 6);
            int thirdFoodAmnt = Random.Range(3, 6);
            requiredScore = (firstFoodAmnt + secondFoodAmnt + thirdFoodAmnt) + currentScore;

            Sprite firstFood = foodSprites[foodTypes[0]];
            Sprite secondFood = foodSprites[foodTypes[1]];
            Sprite thirdFood = foodSprites[foodTypes[2]];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);
            requiredFoodObjects.Add(thirdFood.name, thirdFoodAmnt);
            collectedFoodObjects.Add(firstFood.name, 0);
            collectedFoodObjects.Add(secondFood.name, 0);
            collectedFoodObjects.Add(thirdFood.name, 0);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "OBJECTIVE\nCollect " + firstFoodAmnt + "     " + " | Collect " + secondFoodAmnt + "     " + " | Collect " + thirdFoodAmnt + "          ";

            switch (firstFood.name)
            {
                case "Apple":
                    foodImages[2].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[2].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[2].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[2].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[2].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[2].sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    foodImages[3].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[3].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[3].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[3].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[3].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[3].sprite = foodSprites[2];
                    break;
            }

            switch (thirdFood.name)
            {
                case "Apple":
                    foodImages[4].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[4].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[4].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[4].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[4].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[4].sprite = foodSprites[2];
                    break;
            }
        }
        else if (level == 3) // third level will have 4 food types
        {
            levelObjects[1].SetActive(false);
            levelObjects[2].SetActive(true);

            List<int> foodTypes = RandomFoodListGenerator(4);

            int firstFoodAmnt = Random.Range(5, 8);
            int secondFoodAmnt = Random.Range(5, 8);
            int thirdFoodAmnt = Random.Range(5, 8);
            int fourthFoodAmnt = Random.Range(5, 8);
            requiredScore = (firstFoodAmnt + secondFoodAmnt + thirdFoodAmnt + fourthFoodAmnt) + currentScore;

            Sprite firstFood = foodSprites[foodTypes[0]];
            Sprite secondFood = foodSprites[foodTypes[1]];
            Sprite thirdFood = foodSprites[foodTypes[2]];
            Sprite fourthFood = foodSprites[foodTypes[3]];

            requiredFoodObjects.Add(firstFood.name, firstFoodAmnt);
            requiredFoodObjects.Add(secondFood.name, secondFoodAmnt);
            requiredFoodObjects.Add(thirdFood.name, thirdFoodAmnt);
            requiredFoodObjects.Add(fourthFood.name, fourthFoodAmnt);
            collectedFoodObjects.Add(firstFood.name, 0);
            collectedFoodObjects.Add(secondFood.name, 0);
            collectedFoodObjects.Add(thirdFood.name, 0);
            collectedFoodObjects.Add(fourthFood.name, 0);

            Text objectiveTxt = GameObject.FindGameObjectWithTag("ObjectiveTxt").GetComponent<Text>();
            objectiveTxt.text = "OBJECTIVE\nCollect " + firstFoodAmnt + "     " + " | Collect " + secondFoodAmnt + "     " + "| Collect " + thirdFoodAmnt + "     " + " | Collect " + fourthFoodAmnt + "     ";

            switch (firstFood.name)
            {
                case "Apple":
                    foodImages[5].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[5].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[5].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[5].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[5].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[5].sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    foodImages[6].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[6].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[6].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[6].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[6].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[6].sprite = foodSprites[2];
                    break;
            }

            switch (thirdFood.name)
            {
                case "Apple":
                    foodImages[7].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[7].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[7].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[7].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[7].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[7].sprite = foodSprites[2];
                    break;
            }

            switch (fourthFood.name)
            {
                case "Apple":
                    foodImages[8].sprite = foodSprites[0];
                    break;
                case "Fish":
                    foodImages[8].sprite = foodSprites[1];
                    break;
                case "Pig":
                    foodImages[8].sprite = foodSprites[3];
                    break;
                case "Roast":
                    foodImages[8].sprite = foodSprites[4];
                    break;
                case "Roll":
                    foodImages[8].sprite = foodSprites[5];
                    break;
                case "Stew":
                    foodImages[8].sprite = foodSprites[2];
                    break;
            }
        }
    }

    void PopUpText()
    {
        levelTxt.text = "Tap to Continue";

        if (level > 3)
            levelTxt.text = "";
    }

    List<int> RandomFoodListGenerator(int listSize)
    {
        List<int> validNumbers = new List<int>();
        List<int> levelNumbers = new List<int>(listSize);

        for (int i = 0; i < foodSprites.Count; i++)
        {
            validNumbers.Add(i);
        }

        for (int i = 0; i < levelNumbers.Capacity; i++)
        {
            int index = Random.Range(0, validNumbers.Count);
            levelNumbers.Add(validNumbers[index]);
            validNumbers.RemoveAt(index);
        }

        return levelNumbers;
    }

    // https://forum.unity.com/threads/solved-slow-everything-but-the-player.323965/
    float timer = 0.0f;
    IEnumerator WaitForKeyDown(KeyCode keyCode)
    {
        while (!Input.GetKeyDown(keyCode) || !Input.GetMouseButtonDown(0))
        {
            yield return null;
            timer += Time.unscaledDeltaTime;
            Time.timeScale = 0.000001f;
            PopUpText();
            if (Input.touchCount > 0 || Input.GetKeyDown(keyCode) || Input.GetMouseButtonDown(0))
            {
                //Resume Game
                Time.timeScale = 1;
                timer = 0;
                break;
            }
        }
    }
}