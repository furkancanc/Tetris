using UnityEngine;

using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    public Shape[] allShapes;
    public Transform[] queuedXForms = new Transform[3];

    Shape[] queuedShapes = new Shape[3];

    Shape GetRandomShape()
    {
        int randomIndex = Random.Range(0, allShapes.Length);
        
        if (allShapes[randomIndex])
        {
            return allShapes[randomIndex];
        }
        else
        {
            Debug.LogWarning("WARNING! Invalid shape!");
            return null;
        }
    }

    public Shape SpawnShape()
    {
        Shape shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity);

        if (shape)
        {
            return shape;
        }
        else
        {
            Debug.LogWarning("WARNING! Invalid shape in spawner!");    
            return null;
        }

    }

    void InitQueue()
    {
        for (int i = 0; i < queuedShapes.Length; ++i)
        {
            queuedShapes[i] = null;
        }

        FillQueue();
    }

    void FillQueue()
    {
        for (int i = 0; i < queuedShapes.Length; ++i)
        {
            if (!queuedShapes[i])
            {
                queuedShapes[i] = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;
                queuedShapes[i].transform.position = queuedXForms[i].position;
            }
        }
    }

    private void Awake()
    {
        InitQueue();
    }
}
