using System;
using UnityEngine;

public class WeaponSprite : WeaponComponent
{
    [SerializeField]
    public Vector3[] _animationPos;

    private SpriteRenderer _playerSpriteRenderer;
    private SpriteRenderer _weaponSpriteRenderer;
    private int _currentWeaponSpriteIndex = 0;

    public bool Attack { get; set; }

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
        if (false == Attack)
        {
            _currentWeaponSpriteIndex = 0;
            UpdateAnimation();
            return;
        }

        _currentWeaponSpriteIndex = (_currentWeaponSpriteIndex + 1) % _sprite.Sprites.Length;
        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        _weaponSpriteRenderer.sprite = _sprite.Sprites[_currentWeaponSpriteIndex];
        transform.localPosition = _animationPos[_currentWeaponSpriteIndex];
    }

    private void OnEnable()
    {
        _playerSpriteRenderer.RegisterSpriteChangeCallback(HandlePlayerSpriteChange);
    }

    [Serializable]
    public class WeaponSprites
    {
        [field: SerializeField]
        public Sprite[] Sprites { get; private set; }
    }
}
