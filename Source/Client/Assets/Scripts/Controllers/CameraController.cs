using TMPro;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _targetRigidbody;
    public Vector2 Offset;
    public float Speed = 3.0f;
    private float _yThreshold;

    private void Start()
    {
        _yThreshold = Camera.main.orthographicSize / 2;
        _target = Managers.Object.Player.transform;
        _targetRigidbody = Managers.Object.Player.GetComponent<PlayerController>().GetComponent<Rigidbody2D>(); 
    }

    private void FixedUpdate()
    {
        var targetPosition = _target.position;
        var cameraPosition = transform.position;

        var yDiff = Mathf.Abs(cameraPosition.y + Offset.y - targetPosition.y);

        var newPosition = transform.position;

        if (yDiff >= _yThreshold)
            newPosition.y = targetPosition.y;

        newPosition.y = Mathf.Max(0.0f, newPosition.y);

        var moveSpeed = Mathf.Max(_targetRigidbody.velocity.magnitude, Speed);
        transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
    }
}