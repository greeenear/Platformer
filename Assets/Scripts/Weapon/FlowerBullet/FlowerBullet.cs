using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private GameObject playerObject;
    private Vector3 lastPosition;

    private void Awake()
    {

        if (bulletSpeed == 0)
        {
            Debug.Log("Установить значение скорости для FlowerBullet");
            bulletSpeed = 15;
        }
        if (playerObject == null)
        {
            Debug.Log("Установить GameObject персонажа для FlowerBullet");
            playerObject = GameObject.FindGameObjectWithTag("Player");
        }
    }
    private void Start()
    {
        lastPosition = transform.position;
    }


    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
        RaycastHit hitInfo;
        if (Physics.Linecast(lastPosition, transform.position, out hitInfo))
        {
            Debug.DrawLine(lastPosition, transform.position);
            Debug.Log(hitInfo.transform.name);
            Destroy(gameObject);
        }
        lastPosition = transform.position;
    }
}
