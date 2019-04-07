//-----------------------------------------------------------------------
// <copyright file="Growth.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Growing the snake
/// </summary>
public class Growth : MonoBehaviour
{
    [SerializeField]
    private GameObject snakeBodyResource;
    private LinkedList<Body> listOfBodies = new LinkedList<Body>();
    private Movement movement;

    /// <summary>
    /// Gets List Of Snake Bodies
    /// </summary>
    public LinkedList<Body> ListofBodies
    {
        get
        {
            return this.listOfBodies;
        }

        private set
        {
            this.listOfBodies = value;
        }
    }

    /// <summary>
    /// Push Body to the tail end of the Snake
    /// </summary>
    public void AddToBody()
    {
        GameObject snakeBodyToAdd = Instantiate(this.snakeBodyResource, this.TailPosition(), Quaternion.identity, this.gameObject.transform);
        this.listOfBodies.AddLast(snakeBodyToAdd.GetComponent<Body>());
    }
    
    /// <summary>
    /// Location of the first body the snake gets.
    /// </summary>
    /// <returns>Vector location of first body</returns>
    public Vector3 LocationOfFirstBody()
    {
        LinkedListNode<Body> body = this.listOfBodies.First.Next;
        return body.Value.CurrentLocation();
    }

    /// <summary>
    /// Check if Head position Hit the body
    /// </summary>
    /// <param name="position">position of head</param>
    /// <returns>Checks if position hits the head</returns>
    public bool CheckIfPositionHitBody(Vector3 position)
    {
        LinkedListNode<Body> node = this.listOfBodies.First.Next;
        while (node != null)
        {
            Body snakeBody = node.Value;
            if (snakeBody.CurrentLocation() == position)
            {
                return true;
            }

            node = node.Next;
        }

        return false;
    }

    private Vector3 TailPosition()
    {
        LinkedListNode<Body> lastNode = this.listOfBodies.Last;
        Body snakeTail = lastNode.Value;
        return snakeTail.transform.position;
    }

    private void Awake()
    {
        this.movement = this.GetComponent<Movement>();
    }

    private void Start()
    {
        // Add Head to the list of body first
        this.listOfBodies.AddLast(this.movement.Head);
    }
}
