using UnityEngine;

public class GameController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    Shape activeShape;

    float dropInterval = .25f;
    float timeToDrop;

    private void Start()
    {
        //gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        //spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
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

    private void Update()
    {
        if (!gameBoard || !spawner)
        {
            return;
        }

        if (Time.time > timeToDrop)
        {
            timeToDrop = Time.time + dropInterval;
            if (activeShape)
            {
                activeShape.MoveDown();
            }
        }
    }
}
