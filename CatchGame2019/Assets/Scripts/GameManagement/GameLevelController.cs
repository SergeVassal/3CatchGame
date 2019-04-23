using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelController : MonoBehaviour
{


    private void Start()
    {
        DontDestroyOnLoad(gameObject);

    }


    public float[] GetCurrentSpawnIntervals()
    {
        return new float[2] { 2f, 3f };
    }
    

}
