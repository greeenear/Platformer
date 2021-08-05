using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIplayerStats : MonoBehaviour
{
    [SerializeField] Text helthPoint;
    [SerializeField] Text coinCount;
    [SerializeField] private GameObject playerGameObject;

    private void Start()
    {
        if (playerGameObject == null)
        {
            playerGameObject = GameObject.FindGameObjectWithTag("Player");
        }
        playerGameObject.GetComponent<Player>().isTakeDamage += СhangesHelthPoint;
        playerGameObject.GetComponent<Player>().isTakeCoins += ChangesCoinsCount;
        helthPoint.text = playerGameObject.GetComponent<Player>().healthPoints.ToString();
    }

    private void СhangesHelthPoint()
    {
        helthPoint.text = playerGameObject.GetComponent<Player>().healthPoints.ToString();
    }
    private void ChangesCoinsCount()
    {
        coinCount.text = playerGameObject.GetComponent<Player>().coinCount.ToString();
    }

}
