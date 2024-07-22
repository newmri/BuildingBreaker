using UnityEngine;
using UnityEngine.UIElements;

public class FallingObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _background;

    private float _gravity = -9.8f;
    private float _fallSpeed = 0f;

    private bool _isFalling = true;

    private void Start()
    {
        var backgroundRenderer = _background.GetComponent<SpriteRenderer>();

        float backgroundTop = backgroundRenderer.bounds.max.y;
        var newPosition = new Vector3(transform.position.x, backgroundTop, transform.position.z);

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
        switch (collision.gameObject.name)
        {
            case "Player":
                _isFalling = false;
                break;
            default:
                break;
        }
    }
}
