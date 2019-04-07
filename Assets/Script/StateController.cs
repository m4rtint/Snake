//-----------------------------------------------------------------------
// <copyright file="StateController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State Controller for the state of the game
/// </summary>
public class StateController : MonoBehaviour
{
    private static StateController instance;
    private State currentState = State.Play;

    /// <summary>
    /// Different States of the Game
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Pause the game
        /// </summary>
        Pause,

        /// <summary>
        /// Run the game
        /// </summary>
        Play
    }

    /// <summary>
    /// Gets Singleton reference of State Controller
    /// </summary>
    public static StateController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<StateController>();
            }

            return instance;
        }
    }

    /// <summary>
    /// Gets or sets the current state
    /// </summary>
    public State CurrentState
    {
        get
        {
            return this.currentState;
        }

        set
        {
            this.currentState = value;
        }
    }
}
