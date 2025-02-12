using UnityEngine;

public class GameController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    float dropInterval = .9f;
    float timeToDrop;

    float timeToNextKey;

    //[Range(.02f, 1f)]
    //float keyRepeatRate = .03f;

    float timeToNextKeyLeftRight;

    [Range(.02f, 1f)]
    public float keyRepeatRateLeftRight = .15f;

    float timeToNextKeyDown;

    [Range(.01f, 1f)]
    public float keyRepeatRateDown = .01f;

    float timeToNextKeyRotate;

    [Range(.02f, 1f)]
    public float keyRepeatRateRotate = .25f;

    private void Start()
    {
        timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;
        timeToNextKeyDown = Time.time + keyRepeatRateDown;
        timeToNextKeyRotate = Time.time + keyRepeatRateRotate;

        gameBoard = GameObject.FindFirstObjectByType<Board>();
        spawner = GameObject.FindFirstObjectByType<Spawner>();

        if (!gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
        else
        {
            spawner.transform.position = Vectorf.Round(spawner.transform.position);
            if (!activeShape)
            {
                activeShape = spawner.SpawnShape();
            }
        }
    }

    void PlayerInput()
    {
        if ((Input.GetButton("MoveRight")) && (Time.time > timeToNextKeyLeftRight) || (Input.GetButtonDown("MoveRight")))
        {
            activeShape.MoveRight();
            timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveLeft();
            }
        }

        else if ((Input.GetButton("MoveLeft")) && (Time.time > timeToNextKeyLeftRight) || (Input.GetButtonDown("MoveLeft")))
        {
            activeShape.MoveLeft();
            timeToNextKeyLeftRight = Time.time + keyRepeatRateLeftRight;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.MoveRight();
            }
        }

        else if ((Input.GetButtonDown("Rotate")) && (Time.time > timeToNextKeyRotate))
        {
            activeShape.RotateRight();
            timeToNextKeyRotate = Time.time + keyRepeatRateRotate;

            if (!gameBoard.IsValidPosition(activeShape))
            {
                activeShape.RotateLeft();
            }
        }
        else if (Input.GetButton("MoveDown") && (Time.time > timeToNextKeyDown) || Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            timeToNextKeyDown = Time.time + keyRepeatRateDown;
            activeShape.MoveDown();

            if (!gameBoard.IsValidPosition(activeShape))
            {
                LandShape();
            }
        }
    }

    private void LandShape()
    {
        timeToNextKeyLeftRight = Time.time;
        timeToNextKeyDown = Time.time;
        timeToNextKeyRotate = Time.time;

        activeShape.MoveUp();
        gameBoard.StoreShapeInGrid(activeShape);
        activeShape = spawner.SpawnShape();
    }

    private void Update()
    {
        if (!gameBoard || !spawner || !activeShape)
        {
            return;
        }

        PlayerInput();
    }
}
