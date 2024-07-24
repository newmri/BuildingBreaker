using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.UIElements;

public class FallingObject : MonoBehaviour
{
    private float _gravity = -9.8f;
    private float _fallSpeed = 0f;

    private bool _isFalling = true;

    private void Start()
    {
        var bounds = GetComponent<SpriteRenderer>().bounds;
        var backgroundBounds = GetComponentInParent<SpriteRenderer>().bounds;

        var newPosition = new Vector3(transform.position.x, bounds.size.y + backgroundBounds.size.y, transform.position.z);

        transform.position = newPosition;
    }

    private void Update()
    {
        if (false == _isFalling)
            return;

        _fallSpeed += _gravity * Time.deltaTime;

        var newPosition = transform.position;
        newPosition.y += _fallSpeed * Time.deltaTime;

        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        switch (collision.gameObject.name)
        {
            case "Player":
                break;
            default:
                break;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
    }
}
