using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToPool;
    [SerializeField] private int pooledAmount;

    private List<GameObject> listOfAvailObj;
    private List<GameObject> listOfUnavailObj;
    private float maxWidth;
    private float ballHalfWidth;
    private int spawnIntervalMin;
    private int spawnIntervalMax;


    private void Start()
    {
        listOfAvailObj = new List<GameObject>();
        listOfUnavailObj = new List<GameObject>();

        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = Instantiate(objectsToPool[Random.Range(0, objectsToPool.Length)]);

            if(obj.GetComponent<SpawnableObject>() != null)
            {
                obj.GetComponent<SpawnableObject>().SetSpawner(this);
            }

            obj.SetActive(false);
            listOfAvailObj.Add(obj);
        }

        ballHalfWidth = listOfAvailObj[0].GetComponent<Renderer>().bounds.extents.x;
        Vector3 screenDimen = new Vector3(Screen.width, Screen.height, 0f);
        Vector3 screenToWorld = Camera.main.ScreenToWorldPoint(screenDimen);
        maxWidth = screenToWorld.x - ballHalfWidth;
    }

    public void StartSpawning(int spawnIntervalMin, int spawnIntervalMax)
    {
        this.spawnIntervalMin = spawnIntervalMin;
        this.spawnIntervalMax = spawnIntervalMax;
        StartCoroutine(Spawn());
    }

    public void StopSpawning()
    {
        StopCoroutine(Spawn());
    }


    IEnumerator Spawn()
    {

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 100; i++)
        {
            if (listOfAvailObj.Count > 0)
            {
                int randomInt = Random.Range(0, listOfAvailObj.Count);
                GameObject exactObj = listOfAvailObj[randomInt];
                exactObj.transform.position = new Vector3(Random.Range(-maxWidth, maxWidth), transform.position.y, 0f);
                exactObj.transform.rotation = Quaternion.identity;
                exactObj.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,0f);
                exactObj.SetActive(true);
                listOfUnavailObj.Add(exactObj);
                listOfAvailObj.Remove(exactObj);
                yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
            }
            

        }
    }

    public void AddToAvailList(GameObject obj)
    {
        listOfAvailObj.Add(obj);
        listOfUnavailObj.Remove(obj);

    }


}
