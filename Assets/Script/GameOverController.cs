//-----------------------------------------------------------------------
// <copyright file="GameOverController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Game Over controller handles the flow of the game
/// </summary>
public class GameOverController : MonoBehaviour
{
    private Color visibleColor = new Color(50 / 255, 50 / 255, 50 / 255);
    [SerializeField]
    private Text gameOver;
    [SerializeField]
    private Text tryAgain;
    private bool canReset;
    
    /// <summary>
    /// Start the display game over animation screen
    /// </summary>
    public void StartGameOverDisplay()
    {
        this.Invoke("ShowGameOverScreen", 1.0f);
        this.Invoke("ShowPressAnyKey", 2.0f);
        this.Invoke("SetAbleToReset", 2.0f);
    }

    private void Awake()
    {
        this.gameOver.color = Color.clear;
        this.tryAgain.color = Color.clear;
        this.canReset = false;
    }

    private void Update()
    {
        if (this.canReset && Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void ShowGameOverScreen()
    {
        this.gameOver.color = this.visibleColor;
    }

    private void ShowPressAnyKey()
    {
        this.tryAgain.color = this.visibleColor;
    }

    private void SetAbleToReset()
    {
        this.canReset = true;
    }
}
