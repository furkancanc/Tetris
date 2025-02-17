using UnityEngine;

public class Holder : MonoBehaviour
{
    public Transform holderXForm;
    public Shape heldShape = null;
    float scale = .5f;
    public bool canRelease = false;

    public void Catch(Shape shape)
    {
        if (heldShape)
        {
            Debug.LogWarning("HOLDER WARNING! Release a shape before trying to hold!");
            return;
        }

        if (!shape)
        {
            Debug.LogWarning("HOLDER WARNING! Invalid shape!");
            return;
        }

        if (holderXForm)
        {
            shape.transform.position = holderXForm.position + shape.queueOffset;
            shape.transform.localScale = new Vector3(scale, scale, scale);
            heldShape = shape;
            shape.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogWarning("HOLDER WARNING! Holder has no transform assigned!");
        }
    }

    public Shape Release()
    {
        heldShape.transform.localScale = Vector3.one;
        Shape shape = heldShape;
        heldShape = null;
        canRelease = false;

        return shape;
    }
}
