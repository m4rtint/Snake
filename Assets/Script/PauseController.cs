//-----------------------------------------------------------------------
// <copyright file="PauseController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handling pause functionality
/// </summary>
public class PauseController : MonoBehaviour
{
    private Color visibleColor = new Color(50 / 255, 50 / 255, 50 / 255);
    [SerializeField]
    private Text pauseTitle, resumeButton, quitButton;

    private void Awake()
    {
        this.resumeButton.GetComponent<Button>().onClick.AddListener(delegate { this.PlayGame(); });
        this.quitButton.GetComponent<Button>().onClick.AddListener(delegate { this.QuitGame(); });
        this.HidePauseScreen();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            this.ChangeGameState();
        }
    }

    private void ChangeGameState()
    {
        if (StateController.Instance.CurrentState == StateController.State.Pause)
        {
            this.PlayGame();
        }
        else
        {
            this.PauseGame();
        }
    }

    private void ShowPauseScreen()
    {
        this.pauseTitle.color = this.visibleColor;
        this.resumeButton.color = this.visibleColor;
        this.quitButton.color = this.visibleColor;
    }

    private void HidePauseScreen()
    {
        this.pauseTitle.color = Color.clear;
        this.resumeButton.color = Color.clear;
        this.quitButton.color = Color.clear;
    }

    private void PauseGame()
    {
        StateController.Instance.CurrentState = StateController.State.Pause;
        this.ShowPauseScreen();
    }

    private void PlayGame()
    {
        StateController.Instance.CurrentState = StateController.State.Play;
        this.HidePauseScreen();
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
