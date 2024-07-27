using UnityEngine;

public class GroundObject : MonoBehaviour
{
    [SerializeField]
    private float YOffset = 0f;


    void Start()
    {
        var groundPosition = Managers.Object.GroundPosition;

        var boxCollider = GetComponent<BoxCollider2D>();
        YOffset = boxCollider.bounds.extents.y;
        groundPosition.y += YOffset;

        transform.localPosition = groundPosition;

        YOffset /= 2;
    }

    public Vector3 GetPosition()
    {
        var position = transform.position;
        position.y -= YOffset;

        return position;
    }
}
