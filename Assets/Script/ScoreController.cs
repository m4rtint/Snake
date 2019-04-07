//-----------------------------------------------------------------------
// <copyright file="ScoreController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handling Scoring for the game
/// </summary>
public class ScoreController : MonoBehaviour
{
    private const string HighscoreKey = "HIGHSCORE";
    private int score;
    [SerializeField]
    private Text scoreUI;
    [SerializeField]
    private Text highscoreUI;

    /// <summary>
    /// Add 1 point to the score
    /// </summary>
    public void IncrementScore()
    {
        this.score++;
        this.UpdateScoreUI();
    }

    /// <summary>
    /// Reset score all the way back to 0
    /// </summary>
    public void ResetScore()
    {
        this.score = 0;
        this.UpdateScoreUI();
    }

    /// <summary>
    /// Save the high score if score is higher than current high score
    /// </summary>
    public void SaveHighScore()
    {
        int hs = PlayerPrefs.GetInt(HighscoreKey, 0);
        if (this.score > hs)
        {
            PlayerPrefs.SetInt(HighscoreKey, this.score);
            this.UpdateScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        this.scoreUI.text = Constants.Score + this.score;
        this.highscoreUI.text = Constants.Highscore + PlayerPrefs.GetInt(HighscoreKey, 0);
    }

    private void Awake()
    {
        this.ResetScore();
    }
}
