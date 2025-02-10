using UnityEngine;

public class Board : MonoBehaviour
{
    public Transform emptySprite;
    public int height = 30;
    public int width = 10;
    public int header = 8;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawEmptyCells()
    {
        if (emptySprite != null)
        {
            for (int y = 0; y < height - header; ++y)
            {
                for (int x = 0; x < width; ++x)
                {
                    Transform clone;
                    clone = Instantiate(emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                    clone.name = "Board Space ( x = " + x.ToString() + ", y = " + y.ToString() + " )";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("WARNING! Please assing the emptySprite object!");
        }
    }
}
