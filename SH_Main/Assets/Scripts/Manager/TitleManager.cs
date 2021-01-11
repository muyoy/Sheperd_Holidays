using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public AnimationCurve ac_Logo;
    public SpriteRenderer logoRepresentSprite;
    public GameObject logo;

    public Transform buttons;
    public Transform tutorial_Button;
    public Transform exit_Button;


    public OptionWindow optionWindow;
    public HowtoPlay howtoPlayWindow;
    public AsyncSceneLoad scenetransition;

    void Start()
    {
        if (optionWindow == null) optionWindow = transform.Find("OptionWindow").GetComponent<OptionWindow>();
        if (howtoPlayWindow == null) howtoPlayWindow = transform.Find("HowtoPlayWindow").GetComponent<HowtoPlay>();
        if (logoRepresentSprite == null) logoRepresentSprite = transform.Find("Logo/SpriteMask/Sprite").GetComponent<SpriteRenderer>();
        if (logo == null) logo = transform.Find("Logo").gameObject;
        if (scenetransition == null) scenetransition = transform.Find("Scenetransition").GetComponent<AsyncSceneLoad>();
        logo.SetActive(false);
        scenetransition.gameObject.SetActive(false);

        if (buttons == null) buttons = transform.Find("Button");
        if (tutorial_Button == null) tutorial_Button = transform.Find("Button/Tutorial");
        if (exit_Button == null) exit_Button = transform.Find("Button/Exit");
        buttons.gameObject.SetActive(false);

        StartCoroutine(LogoRepresentation(1.0f, 1.0f));
        SoundManager.Inst.BGMPlayerDB(0);
        optionWindow.gameObject.SetActive(false);
        howtoPlayWindow.gameObject.SetActive(false);
    }

    private IEnumerator LogoRepresentation(float playTime, float delayTime)
    {
        Color color = Color.white;
        float time = 0.0f;

        yield return new WaitForSeconds(delayTime);
        logo.SetActive(true);

        while(time <= playTime)
        {
            time += Time.deltaTime;
            color.a = 1.0f - ac_Logo.Evaluate(time / playTime);
            logoRepresentSprite.color = color;
            yield return null;
        }

        buttons.gameObject.SetActive(true);
        StartCoroutine(ButtonRepresentation(tutorial_Button, new Vector3(240.0f, 550.0f), new Vector3(240.0f, 350.0f), 0.5f));
        StartCoroutine(ButtonRepresentation(exit_Button, new Vector3(240.0f, 550.0f), new Vector3(240.0f, 150.0f), 0.5f));
    }

    private IEnumerator ButtonRepresentation(Transform trans, Vector3 pos1, Vector3 pos2, float playTime)
    {
        float time = 0.0f;
        while(time <= playTime)
        {
            time += Time.deltaTime;
            trans.localPosition = Vector3.Lerp(pos1, pos2, time / playTime);
            yield return null;
        }
    }

    public void OptionButton()
    {
        optionWindow.OpenButton();
    }

    public void GameStartButton()
    {
        scenetransition.LoadSceneName("IntroScene");
        scenetransition.gameObject.SetActive(true);
        //TODO : SceneManager.LoadScene();
    }

    public void TutorialButton()
    {
        howtoPlayWindow.gameObject.SetActive(true);
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
