//-----------------------------------------------------------------------
// <copyright file="Body.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data for Snake Body
/// </summary>
public class Body : MonoBehaviour
{
    private Vector3 previousLocation;

    /// <summary>
    /// Gets or sets previous location of body
    /// </summary>
    public Vector3 PreviousLocation
    {
        get
        {
            return this.previousLocation;
        }

        set
        {
            this.previousLocation = value;
        }
    }

    /// <summary>
    /// Gets Current Location of Game Object
    /// </summary>
    /// <returns>Position of current body location.</returns>
    public Vector3 CurrentLocation()
    {
        return this.transform.position;
    }

    /// <summary>
    /// Move Body to specified position
    /// </summary>
    /// <param name="position">position to move to.</param>
    public void MoveTo(Vector3 position)
    {
        if (position.x > Constants.MaxGridCoordinate)
        {
            position.x = -Constants.MaxGridCoordinate;
        }

        if (position.x < -Constants.MaxGridCoordinate)
        {
            position.x = Constants.MaxGridCoordinate;
        }

        if (position.y > Constants.MaxGridCoordinate)
        {
            position.y = -Constants.MaxGridCoordinate;
        }

        if (position.y < -Constants.MaxGridCoordinate)
        {
            position.y = Constants.MaxGridCoordinate;
        }

        this.transform.position = position;
    }
}
