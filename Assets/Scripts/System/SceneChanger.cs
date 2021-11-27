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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(SceneLoaded(sceneName));
    }

    private IEnumerator SceneLoaded(string sceneName)
    {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        loadingCanvas.SetActive(true);

        yield return new WaitForSeconds(loadingCanvas.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);

        // checking if scene already loaded
        do
        {
            yield return new WaitForSeconds(0.1f);

        } while (scene.progress < 0.9f);

        yield return new WaitForSeconds(0.2f);

        scene.allowSceneActivation = true;
        loadingCanvas.GetComponent<Animator>().SetTrigger("Fade Out");

        yield return new WaitForSeconds(loadingCanvas.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0).Length);
        
        loadingCanvas.SetActive(false);
    }
}
