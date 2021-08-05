using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    private void Update()
    {
        ray = new Ray(transform.position,gameObject.transform.right);
        if(Physics.Raycast(ray,out hit, 1f) && hit.collider.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x - 0.01f, transform.position.y, transform.position.z);
        }
        ray = new Ray(transform.position, -gameObject.transform.right);
        if (Physics.Raycast(ray, out hit, 1f) && hit.collider.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        }

    }
}
