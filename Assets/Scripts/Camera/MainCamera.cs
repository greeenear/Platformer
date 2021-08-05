using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float cameraSpeed;
    [SerializeField] private string playerTag;
    int a;
    private void Start()
    {
       
        if (playerTransform == null)
        { 
            if (playerTag == "")
            {
                playerTag = "Player";
            }
            playerTransform = GameObject.FindGameObjectWithTag(playerTag).transform;
            
        }

        transform.position = new Vector3(playerTransform.position.x + 5, playerTransform.position.y+5,playerTransform.position.z - 10);
    }

    private void FixedUpdate()
    {
        Vector3 cameraOffset = new Vector3(playerTransform.position.x + 5, playerTransform.position.y+5, playerTransform.position.z - 15);
        Vector3 cameraPosition = Vector3.Lerp(transform.position, cameraOffset, Time.fixedDeltaTime * cameraSpeed);
        transform.position = cameraPosition;
    }
}
