using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingObject : MonoBehaviour
{
    private float _gravity = -9.8f;
    private float _fallSpeed = 0f;

    private float _YGround = 0f;

    private void Start()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        var backgroundBounds = GetComponentInParent<SpriteRenderer>().bounds;
        var newPosition = new Vector3(transform.position.x, bounds.size.y + backgroundBounds.size.y, transform.position.z);

        _YGround = Managers.Object.GroundPosition.y + bounds.extents.y;

        transform.position = newPosition;
    }

    private void Update()
    {
        if (transform.localPosition.y <= _YGround)
            return;

        _fallSpeed += _gravity * Time.deltaTime;

        var newPosition = transform.position;
        newPosition.y += _fallSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Player":
                Managers.Stage.GameOver = true;
                break;
            default:
                break;
        }
    }
}
