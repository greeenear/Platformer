using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerController : MonoBehaviour , ITakeDamage
{
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject flowerBulletGameobject;
    [SerializeField] private Transform bulletSpawnTransform;
    private Ray ray;
    private RaycastHit hit;
    private Animator animator;

    #region FlowerStats
    private float shootingCooldown = 0;
    private float helthPoint = 100f;
    #endregion

    private void Awake()
    {
        if (playerGameObject == null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Установить значение playerGameObject");
        }
       
        if (bulletSpawnTransform == null)
        {
            Debug.Log("Установить значение bulletSpawnTransform");
        }
    }
    private void Start()
    {

        animator = GetComponent<Animator>();
        ray = new Ray(bulletSpawnTransform.position, transform.forward);
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(ray, out hit, 25f) && hit.collider.gameObject == playerGameObject)
        {
            if (shootingCooldown <= 0)
            {
                animator.SetTrigger("isAttack");
                StartCoroutine(FiringBursts());
                shootingCooldown = 1.3f;
            }
        }

        if (shootingCooldown > 0)
        {
            shootingCooldown -= Time.fixedDeltaTime;
        }
    }
    IEnumerator FiringBursts()
    {
        Instantiate(flowerBulletGameobject, bulletSpawnTransform.position, Quaternion.LookRotation(bulletSpawnTransform.forward));
        yield return new WaitForSeconds(0.05f);
        Instantiate(flowerBulletGameobject, bulletSpawnTransform.position, Quaternion.LookRotation(bulletSpawnTransform.forward));
        yield return new WaitForSeconds(0.05f);
        Instantiate(flowerBulletGameobject, bulletSpawnTransform.position, Quaternion.LookRotation(bulletSpawnTransform.forward));
        yield break;
    }
    public void TakeDamage(float damage)
    {
        helthPoint -= damage;
        if (helthPoint <= 0)
        {
            Destroy(gameObject);
        }
    }
}
