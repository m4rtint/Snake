//-----------------------------------------------------------------------
// <copyright file="GameController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game controller handles the flow of the game
/// </summary>
public class GameController : MonoBehaviour
{
    private static GameController instance;
    private FoodController foodController;
    private ScoreController scoreController;
    [SerializeField]
    private GameOverController gameOverController;

    /// <summary>
    /// Gets Singleton reference of GameController
    /// </summary>
    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameController>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Checks if the Snake has eaten the food
    /// </summary>
    /// <param name="position">position of head.</param>
    /// <param name="listOfBodies">List of snake body.</param>
    /// <returns>has snake eaten the food</returns>
    public bool HasSnakeEatenFood(Vector3 position, LinkedList<Body> listOfBodies)
    {
        bool hasEaten = this.foodController.ApplePosition() == position;
        if (hasEaten)
        {
            this.foodController.RepositionApple(listOfBodies);
            this.scoreController.IncrementScore();
        }

        return hasEaten;
    }

    /// <summary>
    /// Set the game has ended on the game over controller.
    /// </summary>
    public void GameHasEnded()
    {
        this.gameOverController.StartGameOverDisplay();
        this.scoreController.SaveHighScore();
    }

    private void Awake()
    {
        this.foodController = this.GetComponent<FoodController>();
        this.scoreController = this.GetComponent<ScoreController>();
    }
}
