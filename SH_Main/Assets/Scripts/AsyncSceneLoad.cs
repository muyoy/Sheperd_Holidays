using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncSceneLoad : MonoBehaviour
{
    private string sceneName = null;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    public void LoadSceneName(string name)
    {
        sceneName = name;
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        while (!op.isDone)
        {
            if (op.progress >= 0.9f)
            {
                yield return new WaitForSeconds(1.0f);
                op.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}