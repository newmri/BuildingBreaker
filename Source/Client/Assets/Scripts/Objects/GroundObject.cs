using UnityEngine;

public class GroundObject : MonoBehaviour
{
    void Start()
    {
        var groundPosition = Managers.Object.GroundPosition;

        var boxCollider = GetComponent<BoxCollider2D>();
        groundPosition.y += boxCollider.bounds.extents.y;

        transform.localPosition = groundPosition;
    }
}
