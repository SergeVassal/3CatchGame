using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    private ObjectSpawner spawner;

    public void SetSpawner(ObjectSpawner spawner)
    {
        this.spawner = spawner;
    }

    public void DisableObject()
    {
        spawner.AddToAvailList(this.gameObject);
        gameObject.SetActive(false);
    }

}
