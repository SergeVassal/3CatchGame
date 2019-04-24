using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeactivator : MonoBehaviour
{    
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private int explosionPooledAmount;
    [SerializeField] private float explosionTime;
    [SerializeField] private Transform insideHatExplosionPosition;
    private List<GameObject> listOfAvailObj;
    private List<GameObject> listOfUnavailObj;
    private const int SCORE_INTERVAL= 1;



    private void Start()
    {
        listOfAvailObj = new List<GameObject>();
        listOfUnavailObj = new List<GameObject>();

        for (int i = 0; i < explosionPooledAmount; i++)
        {
            GameObject obj = Instantiate(explosionPrefab);
            obj.SetActive(false);
            listOfAvailObj.Add(obj);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Bomb"&& collision.gameObject.GetComponent<SpawnableObject>()!=null)
        {
            BombHitHandler(collision);
        }
    }

    private void BombHitHandler(Collision2D collision)
    {
        GameManager.Instance.DecreaseScore(SCORE_INTERVAL);
        collision.gameObject.GetComponent<SpawnableObject>().DisableObject();

        Vector2 collisionPoint = GetCollisionPoint(collision);

        if (listOfAvailObj.Count > 0)
        {
            StartCoroutine(Explosion(collisionPoint));
        }
    }

    private Vector2 GetCollisionPoint(Collision2D collision)
    {
        ContactPoint2D[] contactPoints = new ContactPoint2D[1];
        int collisionCount = collision.GetContacts(contactPoints);
        Vector2 collisionPoint = contactPoints[0].point;
        return collisionPoint;
    }

    IEnumerator Explosion(Vector2 explosionPosition)
    {
        GameObject currentExplosion = listOfAvailObj[0];
        currentExplosion.transform.position = explosionPosition;
        currentExplosion.SetActive(true);

        listOfUnavailObj.Add(currentExplosion);
        listOfAvailObj.Remove(currentExplosion);


        yield return new WaitForSeconds(explosionTime);
        DeactivateExplosion(currentExplosion);
    }

    private void DeactivateExplosion(GameObject currentExplosion)
    {
        currentExplosion.SetActive(false);
        listOfAvailObj.Add(currentExplosion);
        listOfUnavailObj.Remove(currentExplosion);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Ball"&& collision.gameObject.GetComponent<SpawnableObject>()!=null)
        {
            BallHitHandler(collision);
        }
    }

    private void BallHitHandler(Collider2D collider)
    {
        GameManager.Instance.IncreaseScore(SCORE_INTERVAL);
        collider.gameObject.GetComponent<SpawnableObject>().DisableObject();
    }

    

}
