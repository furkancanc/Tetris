using UnityEngine;

using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    public Shape[] allShapes;
    public Transform[] queuedXForms = new Transform[3];

    Shape[] queuedShapes = new Shape[3];
    float queueScale = .5f;
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
        //Shape shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity);
        Shape shape = GetQueuedShape();
        shape.transform.position = transform.position;
        shape.transform.localScale = Vector3.one;

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
                queuedShapes[i].transform.position = queuedXForms[i].position + queuedShapes[i].queueOffset;
                queuedShapes[i].transform.localScale = new Vector3(queueScale, queueScale, queueScale);
            }
        }
    }

    private void Awake()
    {
        InitQueue();
    }

    Shape GetQueuedShape()
    {
        Shape firstShape = null;

        if (queuedShapes[0])
        {
            firstShape = queuedShapes[0];
        }

        for (int i = 1; i < queuedShapes.Length; ++i)
        {
            queuedShapes[i - 1] = queuedShapes[i];
            queuedShapes[i - 1].transform.position = queuedXForms[i - 1].position + queuedShapes[i].queueOffset;
        }

        queuedShapes[queuedShapes.Length - 1] = null;

        FillQueue();

        return firstShape;
    }
}
