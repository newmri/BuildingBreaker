using UnityCoreLibrary;
using UnityEngine;
using TMPro;

public class UILoadingScene : UIScene
{
    enum Sliders
    {
        Loading_Slider
    }

    enum TextMeshProUGUIs
    {
        Loading_Text
    }

    private string _origninalLoadingText;

    public override void Init()
    {
        base.Init();

        Bind<UnityEngine.UI.Slider>(typeof(Sliders));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        _origninalLoadingText = this.GetTextMesh((int)TextMeshProUGUIs.Loading_Text).text;
    }

    private void Update()
    {
        float amount = CoreManagers.Scene.LoadingAmount * 100.0f;
        GetSlider((int)Sliders.Loading_Slider).value = amount;
        this.GetTextMesh((int)TextMeshProUGUIs.Loading_Text).text = _origninalLoadingText + Mathf.RoundToInt(amount) + "%";
    }
}
