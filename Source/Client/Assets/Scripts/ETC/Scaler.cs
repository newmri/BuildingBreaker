using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField]
    private bool _isAspectRatio = true;
    [SerializeField]
    private bool _isBottomPosition = true;

    private void Start()
    {
        var topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        var worldSpaceWidth = topRightCorner.x * 2;
        var worldSpaceHeight = topRightCorner.y * 2;

        var spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        var localScale = new Vector3(worldSpaceWidth / spriteSize.x, worldSpaceHeight / spriteSize.y, 1.0f);

        if(_isAspectRatio)
        {
            if(localScale.x > localScale.y)
                localScale.y = localScale.x;
            else
                localScale.x = localScale.y;

            if (_isBottomPosition)
            {
                var spriteHeightAfterScaling = spriteSize.y * localScale.y;
                var bottomPositionY = -topRightCorner.y + (spriteHeightAfterScaling / 2);
                transform.position = new Vector3(transform.position.x, bottomPositionY, transform.position.z);
            }
        }

        gameObject.transform.localScale = localScale;

    }
}
