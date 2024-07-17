using UnityEngine;

public class WeaponComponent : MonoBehaviour
{
    protected Weapon _weapon;

    protected virtual void Awake()
    {
        _weapon = GetComponent<Weapon>();
    }
}
