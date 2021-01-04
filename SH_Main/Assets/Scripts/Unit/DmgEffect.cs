using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DmgEffect : MonoBehaviour
{
    private float lerpTime = 2.0f;

    public IEnumerator Dmg(float damage)
    {
        float time = 0.0f;

        transform.localPosition = Vector3.zero;
        gameObject.SetActive(true);
        GetComponent<TextMeshPro>().text = damage.ToString();
        while (gameObject.activeSelf)
        {
            time += Time.deltaTime;
            transform.position += new Vector3(0.0f, 0.02f, 0.0f); 
            GetComponent<TextMeshPro>().color = Color.Lerp(Color.white, new Color(1.0f, 1.0f, 1.0f, 0.0f), time / lerpTime);
            if (GetComponent<TextMeshPro>().color.a < 0.1f)
                gameObject.SetActive(false);
            yield return null;
        }
    }
}
