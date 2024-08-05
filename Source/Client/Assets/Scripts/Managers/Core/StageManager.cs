using UnityCoreLibrary;
using UnityEngine;

public class StageManager
{
    public GameObject StageObject { get; set; }

    private bool _gameOver = false;
    public bool GameOver
    {
        get { return _gameOver; }
        set
        {
            _gameOver = value;
            Managers.UI.ShowPopupUI<UIGameOverPopup>();
        }
    }

    public void Init()
    {
        StageObject = GameObject.FindGameObjectWithTag("Stage");

        Managers.Object.Init();
    }

    public void Clear()
    {

    }
}
