using System;
using UnityEngine;

public class WeaponSprite : WeaponComponent
{
    [SerializeField]
    public Vector3[] _animationPos;

    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _weaponSpriteRenderer;
    private int _currentWeaponSpriteIndex = 0;

    private bool _attack = false;
    public bool Attack
    {
        get
        {
            return _attack;
        }
        set
        {
            _attack = value;
            if (true == _attack)
                _playerSpriteRenderer.RegisterSpriteChangeCallback(HandlePlayerSpriteChange);
            else
            {
                _playerSpriteRenderer.UnregisterSpriteChangeCallback(HandlePlayerSpriteChange);
                _currentWeaponSpriteIndex = 0;
                UpdateAnimation();
            }

        }
    }

    [SerializeField]
    private WeaponSprites _sprite;

    protected override void Awake()
    {
        base.Awake();

        _playerSpriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
        _weaponSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandlePlayerSpriteChange(SpriteRenderer sr)
    {
        ++_currentWeaponSpriteIndex;
        if(_currentWeaponSpriteIndex >= _sprite.Sprites.Length)
        {
            _currentWeaponSpriteIndex = 0;
            return;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _weaponSpriteRenderer.sprite = _sprite.Sprites[_currentWeaponSpriteIndex];
        transform.localPosition = _animationPos[_currentWeaponSpriteIndex];
    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Building":
                if(Attack)
                    Debug.Log("Attack Building");
                break;
            default:
                break;
        }
    }
}
