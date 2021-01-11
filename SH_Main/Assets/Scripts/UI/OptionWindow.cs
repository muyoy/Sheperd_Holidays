using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionWindow : MonoBehaviour
{
    public Slider SFX_slider;
    public Slider BGM_slider;
    public Image SFX_Image;
    public Image BGM_Image;
    public Sprite soundEnable;
    public Sprite soundDisable;

    private void Start()
    {
        if (SFX_slider == null) SFX_slider = transform.Find("BackGround/Sound/SFX/Slider").GetComponent<Slider>();
        if (BGM_slider == null) BGM_slider = transform.Find("BackGround/Sound/BGM/Slider").GetComponent<Slider>();
        if (SFX_Image == null) SFX_Image = transform.Find("BackGround/Sound/SFX/Image").GetComponent<Image>();
        if (BGM_Image == null) BGM_Image = transform.Find("BackGround/Sound/BGM/Image").GetComponent<Image>();

        //TODO : load sound image
    }

    public void OnValueChanged_SFXSoundSlider(Slider slider)
    {
        SoundManager.Inst.SfxVolume(slider.value);
        ChangeSoundImage(true);
    }

    public void OnValueChanged_BGMSoundSlider(Slider slider)
    {
        SoundManager.Inst.BgmVolume(slider.value);
        ChangeSoundImage(false);
    }

    private void ChangeSoundImage(bool isSFX)
    {
        if(isSFX) SFX_Image.sprite = SFX_slider.value <= 0 ? soundDisable : soundEnable;
        else BGM_Image.sprite = BGM_slider.value <= 0 ? soundDisable : soundEnable;
    }

    public void OpenButton()
    {
        gameObject.SetActive(true);

        SFX_slider.value = SoundManager.Inst.sfxVolume;
        ChangeSoundImage(true);

        BGM_slider.value = SoundManager.Inst.bgmVolume;
        ChangeSoundImage(false);
    }

    public void CloseButton()
    {
        gameObject.SetActive(false);
    }

}
