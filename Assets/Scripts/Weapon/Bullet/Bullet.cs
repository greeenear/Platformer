using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damagePoint;
    private Vector3 lastPosition;
    

    private void Awake()
    {
        if (damagePoint == 0)
        {
            Debug.Log("Установить значение урона для Bullet");
            damagePoint = 10;
        }
           
        if (bulletSpeed == 0)
        {
            Debug.Log("Установить значение скорости для Bullet");
            bulletSpeed = 15;
        }
    }
    private void Start()
    {
        lastPosition = transform.position;
        Destroy(gameObject,7);
    }

   
    private void FixedUpdate()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.fixedDeltaTime);
        RaycastHit hitInfo;
        if(Physics.Linecast(lastPosition, transform.position, out hitInfo))
        {
            
            if (hitInfo.collider.gameObject.tag=="Enemy"|| hitInfo.collider.gameObject.tag == "Player")
            {
                hitInfo.collider.gameObject.GetComponent<ITakeDamage>().TakeDamage(damagePoint);
            }
            if(hitInfo.collider.gameObject.tag != "TriggerObject")
                Destroy(gameObject);
        }
       
        lastPosition = transform.position;
    }
}
