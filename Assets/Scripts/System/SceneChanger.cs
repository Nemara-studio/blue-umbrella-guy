using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger singleton;

    public GameObject loadingCanvas;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName, System.Action onStartAnimationCompleted = null)
    {
        StartCoroutine(SceneLoaded(sceneName, onStartAnimationCompleted));
    }

    private IEnumerator SceneLoaded(string sceneName, System.Action onStartAnimationCompleted = null)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        yield return new WaitForSecondsRealtime(loadingCanvas.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);

        onStartAnimationCompleted?.Invoke();

        // checking if scene already loaded
        do
        {
            yield return new WaitForSecondsRealtime(0.1f);

        } while (scene.progress < 0.9f);

        yield return new WaitForSecondsRealtime(0.2f);

        scene.allowSceneActivation = true;
        loadingCanvas.GetComponent<Animator>().SetTrigger("Fade Out");

        yield return new WaitForSecondsRealtime(loadingCanvas.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length + 0.2f);

        loadingCanvas.SetActive(false);
    }
}
