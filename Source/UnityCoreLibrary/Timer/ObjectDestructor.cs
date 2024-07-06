using UnityEngine;
using System.Collections;

public class ObjectDestructor : MonoBehaviour
{
    [SerializeField]
    private float _destructionDelay = 0.0f;

    void Start()
    {
        Destroy(gameObject, _destructionDelay);
    }

    void AccTime(float time)
    {
        _destructionDelay += time;
    }
}

