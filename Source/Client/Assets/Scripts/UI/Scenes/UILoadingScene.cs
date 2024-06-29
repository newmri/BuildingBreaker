using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

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

    private UnityEngine.UI.Slider slider;
    private TextMeshProUGUI loadingText;
    private string origninalLoadingText;

    public override void Init()
    {
        base.Init();

        Bind<UnityEngine.UI.Slider>(typeof(Sliders));
        Bind<TextMeshProUGUI>(typeof(TextMeshProUGUIs));

        slider = GetSlider((int)Sliders.Loading_Slider);
        loadingText = this.GetTextMesh((int)TextMeshProUGUIs.Loading_Text);
        origninalLoadingText = loadingText.text;
    }

    private void Update()
    {
        float amount = CoreManagers.Scene.LoadingAmount * 100.0f;
        slider.value = amount;
        loadingText.text = origninalLoadingText + Mathf.RoundToInt(amount) + "%";
    }
}
