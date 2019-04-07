//-----------------------------------------------------------------------
// <copyright file="Movement.cs" company="Martin">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handling movement for snake object
/// </summary>
public class Movement : MonoBehaviour
{
    [SerializeField]
    private Body head;
    private Direction currentDirection;
    [SerializeField]
    private float secondsPerMovement = 0.25f;
    private float countDownMovementTimer;
    private Growth growth;
    private GameController gameController;
    private StateController stateController;

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    /// <summary>
    /// Gets head of Snake GameObject
    /// </summary>
    public Body Head
    {
        get
        {
            return this.head;
        }

        private set
        {
            this.head = value;
        }
    }

    private void Awake()
    {
        this.currentDirection = Direction.Left;
        this.ResetCountDownTimer();
        this.growth = this.GetComponent<Growth>();
        this.gameController = GameController.Instance;
        this.stateController = StateController.Instance;
    }

    private void Update()
    {
        this.InputController();
    }

    private void InputController()
    {
        if (Input.GetKeyDown(KeyCode.W) && this.IsDirectionAllowed(KeyCode.W))
        {
            this.currentDirection = Direction.Up;
        }
        else if (Input.GetKeyDown(KeyCode.S) && this.IsDirectionAllowed(KeyCode.S))
        {
            this.currentDirection = Direction.Down;
        }
        else if (Input.GetKeyDown(KeyCode.D) && this.IsDirectionAllowed(KeyCode.D))
        {
            this.currentDirection = Direction.Right;
        }
        else if (Input.GetKeyDown(KeyCode.A) && this.IsDirectionAllowed(KeyCode.A))
        {
            this.currentDirection = Direction.Left;
        }
    }

    private void FixedUpdate()
    {
        if (this.IsGameRunning())
        {
            this.countDownMovementTimer -= Time.fixedDeltaTime;
            if (this.countDownMovementTimer < 0)
            {
                this.MoveHead();
                this.GrowIfNeeded();
                this.MoveBody();
                this.ResetCountDownTimer();
            }
        }
    }

    private void MoveHead()
    {
        Vector3 location = this.NextLocationWith(this.currentDirection);
        if (!this.HasSnakeHeadHitBodyAt(location))
        {
            this.head.PreviousLocation = this.head.transform.position;
            this.head.MoveTo(location);
        }
    }

    private void MoveBody()
    {
        LinkedList<Body> body = this.ListOfBody();

        LinkedListNode<Body> previousNode = body.First;
        LinkedListNode<Body> currentNode = body.First.Next;
        while (currentNode != null)
        {
            Body currentBodyToMove = currentNode.Value;
            currentBodyToMove.PreviousLocation = currentBodyToMove.CurrentLocation();
            currentBodyToMove.MoveTo(previousNode.Value.PreviousLocation);

            previousNode = previousNode.Next;
            currentNode = currentNode.Next;
        } 
    }

    private LinkedList<Body> ListOfBody()
    {
        return this.growth.ListofBodies;
    }

    private void ResetCountDownTimer()
    {
        this.countDownMovementTimer = this.secondsPerMovement;
    }

    private Vector3 ConvertDirectionToVector(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3.up;
            case Direction.Down:
                return Vector3.down;
            case Direction.Left:
                return Vector3.left;
            case Direction.Right:
                return Vector3.right;
            default:
                return Vector3.up;
        }  
    }

    private void GrowIfNeeded()
    {
        if (this.gameController.HasSnakeEatenFood(this.head.CurrentLocation(), this.growth.ListofBodies))
        {
            this.growth.AddToBody();
        }
    }

    private bool HasSnakeHeadHitBodyAt(Vector3 location)
    {
        bool hasHit = this.growth.CheckIfPositionHitBody(location);
        if (hasHit)
        {
            this.stateController.CurrentState = StateController.State.Pause;
            GameController.Instance.GameHasEnded();
        }

        return hasHit;
    }

    private bool IsListOfBodiesEmpty()
    {
        return this.growth.ListofBodies.Count == 1;
    }

    private bool IsGameRunning()
    {
        return this.stateController.CurrentState == StateController.State.Play;
    }

    private Vector3 NextLocationWith(Direction direction)
    {
        return this.ConvertDirectionToVector(direction) + this.head.transform.position;
    }

    private bool IsDirectionAllowed(KeyCode code)
    {
        bool isAllowed = this.IsListOfBodiesEmpty();

        if (!isAllowed)
        {
            Direction direction = Direction.Up;
            switch (code)
            {
                case KeyCode.W:
                    direction = Direction.Up;
                    break;
                case KeyCode.A:
                    direction = Direction.Left;
                    break;
                case KeyCode.S:
                    direction = Direction.Down;
                    break;
                case KeyCode.D:
                    direction = Direction.Right;
                    break;
            }

            isAllowed = this.NextLocationWith(direction) != this.growth.LocationOfFirstBody();
        }

        return isAllowed;
    }
}