using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMushroom : Player ,IPlayer,ITakeDamage
{
    #region PlayerStats
    public override int coinCount { get;  set; } = 0;


    [SerializeField] private float playerSpeed;
    [SerializeField] private float playerJumpHeight;
    [SerializeField] public override float healthPoints { get;  set; }

    #endregion

    #region BulletSpawn
    [SerializeField] private Transform BulletSpawnPosition;
    [SerializeField] private GameObject BulletGameObject;
    #endregion

    #region events
    public override Action isTakeDamage { get; set; }
    public override Action isTakeCoins { get; set; }
    #endregion

    #region other
    [SerializeField] private float gravityValue;
    private Animator animator;
    private PlayerInput input;
    private CharacterController characterController;
    private Vector2 playerMoveDirection;
    private Vector3 playerVelocity;
    private RaycastHit hit;
    private bool isGrounded;
    private float shootingCooldown;
    #endregion

    public PlayerMushroom()
    {
        playerSpeed = 10f;
        healthPoints = 100f;
        playerJumpHeight = 200f;
        shootingCooldown = 0;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        playerMoveDirection = input.Player.move.ReadValue<Vector2>();
        Move();
        if (shootingCooldown > 0)
        {
            shootingCooldown -= Time.fixedDeltaTime;
        }
    }

    #region InputSystem

    private void Awake()
    {
        input = new PlayerInput();
        input.Player.shoot.performed += contex => Shoot();
        input.Player.jump.performed += context => Jump();
    }
    private void OnEnable()
    {
        input.Enable();
        
    }
    private void OnDisable()
    {
        input.Disable();
    }

    #endregion


    #region PlayerMovement
    public void Move()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 playerMove = new Vector3(playerMoveDirection.x, 0, 0);
        characterController.Move(playerMove * Time.fixedDeltaTime * playerSpeed);
        if (playerMoveDirection.x == 0)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }
        if (playerMove != Vector3.zero)
        {
            gameObject.transform.right = playerMove;
        }
        playerVelocity.y += gravityValue * Time.deltaTime*1.4f;
        characterController.Move(playerVelocity * Time.deltaTime);

    }

    
    public void Jump()
    {
        if(characterController.isGrounded)
            playerVelocity.y += Mathf.Sqrt(playerJumpHeight * -3.0f * gravityValue);
    }

   

    #endregion 

    public void Shoot()
    {
        if (shootingCooldown <= 0)
        {
            animator.SetTrigger("isAttack");
            Instantiate(BulletGameObject, BulletSpawnPosition.position, Quaternion.LookRotation(BulletSpawnPosition.forward));
            shootingCooldown = 1;
        }
    }
    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float dmg)
    {
        healthPoints -= dmg;
        isTakeDamage?.Invoke();
        if (healthPoints <= 0)
        {
            Death();
            Destroy(gameObject);
        }
    }

    public void TakeCoins()
    {
        coinCount++;
        isTakeCoins?.Invoke();
    }
}
