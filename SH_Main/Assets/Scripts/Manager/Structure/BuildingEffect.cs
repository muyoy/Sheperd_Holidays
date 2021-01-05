using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEffect : MonoBehaviour
{
    public SpriteRenderer progress;
    public Animator effect;
    public Animator baseImage;
    public Animator cautionImage;
    public Animator hammer;

    private float buildTime = 0.0f;
    public bool isBuildProgressEnd = false;

    private void Start()
    {
        //PlayBuildingEffect(5.0f);
    }

    public void PlayBuildingEffect(float _buildTime)
    {
        buildTime = _buildTime;
        isBuildProgressEnd = false;
        StartCoroutine(BuildProgress());
    }

    IEnumerator BuildProgress()
    {
        gameObject.SetActive(true);
        progress.transform.parent.gameObject.SetActive(true);
        cautionImage.gameObject.SetActive(true);
        hammer.gameObject.SetActive(true);
        baseImage.Play("BuildingBaseImage");
        cautionImage.Play("BuildingCautionImage");
        hammer.Play("BuildingHammerImage");

        Vector2 progressSize = progress.size;
        Vector2 progressSizeZero = new Vector2(0.0f, progress.size.y);

        float time = 0.0f;
        while (time <= buildTime)
        {
            time += Time.deltaTime;
            progress.size = Vector2.Lerp(progressSizeZero, progressSize, time / buildTime);
            yield return null;
        }

        progress.transform.parent.gameObject.SetActive(false);
        cautionImage.gameObject.SetActive(false);
        hammer.gameObject.SetActive(false);
        effect.Play("BuildingEffect");
        yield return null;
        isBuildProgressEnd = true;
    }

    public void EffectEndEvent()
    {
        gameObject.SetActive(false);
    }
}
