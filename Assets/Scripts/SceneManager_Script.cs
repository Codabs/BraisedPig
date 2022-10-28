using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_Script : MonoBehaviour
{
    public static SceneManager_Script Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadMenuScene()
    {
        StartCoroutine(LoadScene(0));
    }
    public void LoadGameScene()
    {
        StartCoroutine(LoadScene(1));
    }
    private IEnumerator LoadScene(int sceneid)
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneid, LoadSceneMode.Single);
    }
}
