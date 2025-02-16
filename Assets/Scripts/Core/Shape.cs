using UnityEngine;

public class Shape : MonoBehaviour
{
    public bool canRotate = true;
    public Vector3 queueOffset;

    GameObject[] glowSquareFx;
    public string glowSquareTag = "LandShapeFx";
    private void Start()
    {
        if (glowSquareTag != null)
        {
            glowSquareFx = GameObject.FindGameObjectsWithTag(glowSquareTag);
        }
    }

    public void LandShapeFx()
    {
        int i = 0;

        foreach (Transform child in gameObject.transform)
        {
            if (glowSquareFx[i])
            {
                glowSquareFx[i].transform.position = new Vector3(child.position.x, child.position.y, -2f);
                ParticlePlayer particlePlayer = glowSquareFx[i].GetComponent<ParticlePlayer>();

                if (particlePlayer)
                {
                    particlePlayer.Play();
                }
            }

            ++i;
        }
    }

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }


    public void MoveLeft()
    {
        Move(new Vector3(-1, 0, 0));
    }

    public void MoveRight()
    {
        Move(new Vector3(1, 0, 0));
    }

    public void MoveDown()
    {
        Move(new Vector3(0, -1, 0));
    }

    public void MoveUp()
    {
        Move(new Vector3(0, 1, 0));
    }

    public void RotateRight()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, -90);
        }
    }

    public void RotateLeft()
    {
        if (canRotate)
        {
            transform.Rotate(0, 0, 90);
        }
    }
    
    public void RotateClockwise(bool clockwise)
    {
        if (clockwise)
        {
            RotateRight();
        }
        else
        {
            RotateLeft();
        }
    }
}
