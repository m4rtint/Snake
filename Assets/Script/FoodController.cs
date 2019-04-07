//-----------------------------------------------------------------------
// <copyright file="FoodController.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller that randomly places the food around the grid.
/// </summary>
public class FoodController : MonoBehaviour
{
    [SerializeField]
    private GameObject apple;

    /// <summary>
    /// Randomly Reposition apple
    /// </summary>
    /// <param name="listOfBodies">list of body position</param>
    public void RepositionApple(LinkedList<Body> listOfBodies = null)
    {
        Vector3 relocatingPosition = Vector3.zero;
        do
        {
            int positionX = Random.Range(-Constants.MaxGridCoordinate, Constants.MaxGridCoordinate);
            int positionY = Random.Range(-Constants.MaxGridCoordinate, Constants.MaxGridCoordinate);
            relocatingPosition = new Vector3(positionX, positionY, this.apple.transform.position.z);
        }
        while (this.IsAppleOverlappingSnake(relocatingPosition, listOfBodies));

        this.apple.transform.position = relocatingPosition;
    }

    /// <summary>
    /// Get Position of the Apple
    /// </summary>
    /// <returns>Apple Position.</returns>
    public Vector3 ApplePosition()
    {
        return this.apple.transform.position;
    }

    private void Start()
    {
        this.RepositionApple();
    }

    private bool IsAppleOverlappingSnake(Vector3 position, LinkedList<Body> listOfBodies)
    {
        if (listOfBodies == null)
        {
            return position == Vector3.zero;
        }

        LinkedListNode<Body> node = listOfBodies.First;
        while (node != null)
        {
            Body nodePosition = node.Value;
            if (nodePosition.CurrentLocation() == position)
            {
                return true;
            }

            node = node.Next;
        }

        return false;
    }
}
