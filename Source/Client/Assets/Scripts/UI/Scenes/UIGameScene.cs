using System.Collections;
using System.Collections.Generic;
using UnityCoreLibrary;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIGameScene : UIScene
{
    enum Buttons
    {
        Setting_Button,
        Skill_Button_1,
        Skill_Button_2,
    }

    enum Images
    {
        SkillCoolTime_Image_1,
        SkillCoolTime_Image_2,
    }

    private List<byte> _skillSlot = new List<byte>();
    private Dictionary<byte, Images> _skillCoolTimeList = new Dictionary<byte, Images>();

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.Setting_Button).gameObject.BindEvent(OnClickSettingButton);

        GetButton((int)Buttons.Skill_Button_1).gameObject.BindEvent(OnClickSkillButton);
        GetButton((int)Buttons.Skill_Button_2).gameObject.BindEvent(OnClickSkillButton);
    }

    public void OnClickSettingButton(PointerEventData evt)
    {
        Managers.UI.ShowPopupUI<UISettingPopup>();
    }

    public void OnClickSkillButton(PointerEventData evt)
    {
        var name = evt.selectedObject.name;
        var slot = byte.Parse(name[name.Length - 1].ToString()) - 1;

        Managers.Object.Player.UseSkill(_skillSlot[slot]);
    }

    public void AddSkill(byte skillID)
    {
        _skillSlot.Add(skillID);

        var currCount = _skillCoolTimeList.Count;
        _skillCoolTimeList.Add(skillID, Images.SkillCoolTime_Image_1 + currCount);

        GetButton((int)Buttons.Skill_Button_1 + currCount).GetComponent<Image>().sprite = Managers.SkillData.GetSprite(skillID);
    }

    public void UpdateSkillCoolTime(byte skillID, float ratio)
    {
        ratio = 1.0f - ratio;
        ratio = ratio == 1.0f ? 0.0f : ratio;
   
        GetImage((int)_skillCoolTimeList[skillID]).fillAmount = ratio;
    }
}
