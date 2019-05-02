using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundaryObjectDeactivator : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {        
        collision.GetComponent<Collider2D>().gameObject.GetComponent<SpawnableObject>().DisableObject();
    }

}
