using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodController : MonoBehaviour
{
    public GameObject player;
    public List<Sprite> foodSprites; // the list of possible food objects that are available

    public List<GameObject> foodObjects; // the list of food objects that are currently falling on screen
    public List<float> foodSpeeds;

    public Dictionary<string, int> requiredFoodObjects; // the foods the player needs to catch to complete the round
    public Dictionary<string, int> collectedFoodObjects; // the foods the player needs to catch to complete the round
    public int requiredScore;
    public int currentScore;
    private Text scoreTxt;
    public int level;
    private Image img1;
    private Image img2;
    private Image img3;
    private Image img4;
    private Image img5;
    private Image img6;
    private Image img7;
    private Image img8;
    private Image img9;
    private GameObject lvl1;
    private GameObject lvl2;
    private GameObject lvl3;

    void Start()
    {
        level = 0;
        currentScore = 0;
        requiredScore = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        requiredFoodObjects = new Dictionary<string, int>();
        collectedFoodObjects = new Dictionary<string, int>();
        scoreTxt = GameObject.FindGameObjectWithTag("ScoreTxt").GetComponent<Text>();
        img1 = GameObject.FindGameObjectWithTag("Image1").GetComponent<Image>();
        img2 = GameObject.FindGameObjectWithTag("Image2").GetComponent<Image>();
        img3 = GameObject.FindGameObjectWithTag("Image3").GetComponent<Image>();
        img4 = GameObject.FindGameObjectWithTag("Image4").GetComponent<Image>();
        img5 = GameObject.FindGameObjectWithTag("Image5").GetComponent<Image>();
        img6 = GameObject.FindGameObjectWithTag("Image6").GetComponent<Image>();
        img7 = GameObject.FindGameObjectWithTag("Image7").GetComponent<Image>();
        img8 = GameObject.FindGameObjectWithTag("Image8").GetComponent<Image>();
        img9 = GameObject.FindGameObjectWithTag("Image9").GetComponent<Image>();
        lvl1 = GameObject.FindGameObjectWithTag("Level1Images");
        lvl2 = GameObject.FindGameObjectWithTag("Level2Images");
        lvl3 = GameObject.FindGameObjectWithTag("Level3Images");
        IncrementLevel(); // Start the game
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFoodObjects();
        MoveFoodObjects();
        CheckCollisions();
    }

    void SpawnFoodObjects()
    {
        if (foodObjects.Count < 15)
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

    void MoveFoodObjects()
    {
        for (int i = 0; i < foodObjects.Count; i++)
        {
            foodObjects[i].GetComponent<Rigidbody2D>().AddForce(new Vector2(0.0f, foodSpeeds[i]) * Time.deltaTime);
        }
    }

    void CheckCollisions()
    {
        // Check the score to see if the player has completed the objective
        if (currentScore == requiredScore)
        {
            IncrementLevel();
        }


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
                if (foodObjects[i].GetComponent<SpriteRenderer>().sprite.name.Equals(keyList[j]) && collectedFoodObjects[keyList[j]] < requiredFoodObjects[keyList[j]])
                {
                    if (foodObjects[i].GetComponent<PolygonCollider2D>().IsTouching(player.GetComponent<PolygonCollider2D>()))
                    {
                        collectedFoodObjects[keyList[j]] += 1;
                        GameObject fallenFood = foodObjects[i];
                        foodObjects.Remove(fallenFood);
                        foodSpeeds.Remove(i);
                        Destroy(fallenFood);
                        currentScore++;
                    }
                }
            }
        }

        DisplayScore();
    }

    void DisplayScore()
    {
        List<string> keyList = new List<string>(collectedFoodObjects.Keys);

        if (level == 1)
        {
            scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]];

            CheckCollected();
        }
        else if (level == 2)
        {
            scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]];

            CheckCollected();
        }
        else if (level == 3)
        {
            scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];

            CheckCollected();
        }
    }

    // Checks which objectives have been completed by the player
    void CheckCollected()
    {
        List<string> keyList = new List<string>(collectedFoodObjects.Keys);

        if (level == 1)
        {
            // Changes the color of the required food objects once they are completed
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]];
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] + "</color>";
            }

            // Both Objectives Completed
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] + "</color>";
            }
        }
        else if (level == 2)
        {
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]];
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]];
            }
            else if (collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | <color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] + "</color>";
            }

            // Two Completed Objectives
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]];
            }
            else if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | <color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] + "</color>";
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] + "</color>";
            }

            // All Objectives Completed
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                             " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                             " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] + "</color>";
            }
        }
        else if (level == 3)
        {
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "\n" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            "\n<color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            "</color>\n" + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | <color=#00C0FF>" + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }

            // Two Completed Objectives
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | <color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            "</color> | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | <color=#00C0FF>" + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            "</color> | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | <color=#00C0FF>" + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }
            else if (collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | <color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }

            // Three Completed Objectives
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            "</color> | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]];
            }
            else if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            "</color> | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | <color=#00C0FF>" + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }
            else if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            "</color> | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | <color=#00C0FF>" + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }
            else if (collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | <color=#00C0FF>" + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }

            // All Objectives Completed
            if (collectedFoodObjects[keyList[0]] == requiredFoodObjects[keyList[0]] && collectedFoodObjects[keyList[1]] == requiredFoodObjects[keyList[1]] && collectedFoodObjects[keyList[2]] == requiredFoodObjects[keyList[2]] && collectedFoodObjects[keyList[3]] == requiredFoodObjects[keyList[3]])
            {
                scoreTxt.text = "SCORE\n<color=#00C0FF>" + keyList[0] + ": " + collectedFoodObjects[keyList[0]] + "/" + requiredFoodObjects[keyList[0]] +
                            " | " + keyList[1] + ": " + collectedFoodObjects[keyList[1]] + "/" + requiredFoodObjects[keyList[1]] +
                            " | " + keyList[2] + ": " + collectedFoodObjects[keyList[2]] + "/" + requiredFoodObjects[keyList[2]] +
                            " | " + keyList[3] + ": " + collectedFoodObjects[keyList[3]] + "/" + requiredFoodObjects[keyList[3]] + "</color>";
            }
        }
    }

    void IncrementLevel()
    {
        level++;

        if (level > 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

        // Re-center the player's basket
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(0.0f, -8.5f, 0.0f);

        // Redetermine the objective to be harder (more food)
        SetObjective();

        // Freeze game for 3 seconds and countdown

    }

    void SetObjective()
    {
        if (level == 1) // first level will have 2 food types
        {
            lvl2.SetActive(false);
            lvl3.SetActive(false);

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
                    img1.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img1.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img1.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img1.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img1.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img1.sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    img2.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img2.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img2.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img2.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img2.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img2.sprite = foodSprites[2];
                    break;
            }
        }
        else if (level == 2) // second level will have 3 food types
        {
            lvl1.SetActive(false);
            lvl2.SetActive(true);

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
                    img3.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img3.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img3.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img3.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img3.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img3.sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    img4.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img4.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img4.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img4.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img4.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img4.sprite = foodSprites[2];
                    break;
            }

            switch (thirdFood.name)
            {
                case "Apple":
                    img5.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img5.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img5.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img5.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img5.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img5.sprite = foodSprites[2];
                    break;
            }
        }
        else if (level == 3) // third level will have 4 food types
        {
            lvl2.SetActive(false);
            lvl3.SetActive(true);

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
            objectiveTxt.text = "OBJECTIVE\nCollect " + firstFoodAmnt + "     " + " | Collect " + secondFoodAmnt + "      " + " | Collect " + thirdFoodAmnt + "      " + " | Collect " + fourthFoodAmnt + "      ";

            switch (firstFood.name)
            {
                case "Apple":
                    img6.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img6.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img6.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img6.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img6.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img6.sprite = foodSprites[2];
                    break;
            }

            switch (secondFood.name)
            {
                case "Apple":
                    img7.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img7.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img7.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img7.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img7.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img7.sprite = foodSprites[2];
                    break;
            }

            switch (thirdFood.name)
            {
                case "Apple":
                    img8.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img8.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img8.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img8.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img8.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img8.sprite = foodSprites[2];
                    break;
            }

            switch (fourthFood.name)
            {
                case "Apple":
                    img9.sprite = foodSprites[0];
                    break;
                case "Fish":
                    img9.sprite = foodSprites[1];
                    break;
                case "Pig":
                    img9.sprite = foodSprites[3];
                    break;
                case "Roast":
                    img9.sprite = foodSprites[4];
                    break;
                case "Roll":
                    img9.sprite = foodSprites[5];
                    break;
                case "Stew":
                    img9.sprite = foodSprites[2];
                    break;
            }
        }
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
    float timer = 0;
    IEnumerator ResumeAfterNSeconds(float timePeriod)
    {
        yield return new WaitForEndOfFrame();
        timer += Time.unscaledDeltaTime;
        if (timer < timePeriod)
            StartCoroutine(ResumeAfterNSeconds(3.0f));
        else
        {
            Time.timeScale = 1;                //Resume
            timer = 0;
        }
    }
}