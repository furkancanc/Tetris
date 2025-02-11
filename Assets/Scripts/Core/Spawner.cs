using UnityEngine;

using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    public Shape[] allShapes;

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
}
