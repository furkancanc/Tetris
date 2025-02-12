using UnityEngine;

public class GameController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    float dropInterval = .25f;
    float timeToDrop;

    float timeToNextKey;
    float keyRepeatRate = .25f;

    private void Start()
    {
        timeToNextKey = Time.time;

        gameBoard = GameObject.FindFirstObjectByType<Board>();
        spawner = GameObject.FindFirstObjectByType<Spawner>();

        if (spawner)
        {
            if (activeShape == null)
            {
                activeShape = spawner.SpawnShape();
            }

            spawner.transform.position = Vectorf.Round(spawner.transform.position);
        }

        if (!gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
    }

    void PlayerInput()
    {
        if ((Input.GetButtonDown("MoveRight")) && (Time.time > timeToNextKey) || (Input.GetButtonDown("MoveRight")))
        {
            activeShape.MoveRight();
            timeToNextKey = Time.time + keyRepeatRate;

            if (gameBoard.IsValidPosition(activeShape))
            {
                Debug.Log("Move right");
            }
            else
            {
                activeShape.MoveLeft();
                Debug.Log("Hit the right boundary");
            }
        }

        if (Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            if (activeShape)
            {
                activeShape.MoveDown();

                if (!gameBoard.IsValidPosition(activeShape))
                {
                    activeShape.MoveUp();
                    gameBoard.StoreShapeInGrid(activeShape);

                    if (spawner)
                    {
                        activeShape = spawner.SpawnShape();
                    }
                }
            }
        }
    }

    private void Update()
    {
        PlayerInput();
    }
}
