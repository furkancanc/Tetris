using UnityEngine;

public class GameController : MonoBehaviour
{
    Board gameBoard;
    Spawner spawner;

    private void Start()
    {
        //gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        //spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        gameBoard = GameObject.FindFirstObjectByType<Board>();
        spawner = GameObject.FindFirstObjectByType<Spawner>();

        if (spawner)
        {
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

}
