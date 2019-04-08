using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController:MonoBehaviour
{  
    private AsyncOperation ao;
    private List<AsyncOperation> loadOperations;
    private string currentSceneName = string.Empty;    


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitiateVariables();
    }

    private void InitiateVariables()
    {
        loadOperations = new List<AsyncOperation>();
    }

    public void StartNewGameScene()
    {
        LoadScene("001Level");
    }

    private void LoadScene(string sceneName)
    {        
        ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to loal level" + sceneName);
            return;
        }
        ao.completed += OnLoadOperationComplete;
        loadOperations.Add(ao);
        currentSceneName = sceneName;
    }

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            if (loadOperations.Count == 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName("001Level"));
                GameManager.Instance.SetupNewGame();
            }
        }
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to unloal level" + levelName);
            return;
        }
        ao.completed += OnUnloadOperationComplete;
    }

    void OnUnloadOperationComplete(AsyncOperation ao)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("000Boot"));
        GameManager.Instance.StartBootScene();
    }

}
