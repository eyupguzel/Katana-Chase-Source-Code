using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public void LoadSceneAsync(int sceneIndex)
    {
        StartCoroutine(LoadSceneCoroutine(sceneIndex));
    }

    private IEnumerator LoadSceneCoroutine(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            yield return null;
        }
    }
}
