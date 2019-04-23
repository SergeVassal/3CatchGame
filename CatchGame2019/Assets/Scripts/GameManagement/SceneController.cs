using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController:MonoBehaviour
{
    [SerializeField] private string initialSceneName;
    [SerializeField] private string bootSceneName;

    public event Action NewGameStarted;
    public event Action BootSceneStarted;

    private AsyncOperation ao;
    private List<AsyncOperation> loadOperations;
    private string currentSceneName = string.Empty;
    private string loadingSceneName = string.Empty;



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
        LoadScene(initialSceneName);  
    }

    

    private void LoadScene(string sceneName)
    {
        loadingSceneName = sceneName;
        ao = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        
        loadOperations.Add(ao);
        
        if (ao == null)
        {
            Debug.Log("[GameManager] Unable to loal level" + sceneName);
            return;
        }
        if (sceneName == initialSceneName)
        {
            ao.completed += OnLoadOperationComplete;
        }

        ao.completed += OnLoadOperationComplete;        
        currentSceneName = sceneName;
    }   

    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
            if (loadOperations.Count == 0)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadingSceneName));                
            }
            if (NewGameStarted != null)
            {
                NewGameStarted();
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
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(bootSceneName));
        if (BootSceneStarted != null)
        {
            BootSceneStarted();
        }
    }

}
