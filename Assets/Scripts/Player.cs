﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Tilemaps;


public class Player : MonoBehaviour
{
    public static Player instance;

    private int move = 1;
    [SerializeField] int movespeed=1;
    [SerializeField] Rigidbody2D playerRigidBody;
    [SerializeField] Animator playerAnimator;
    public string transitionName;
    private Vector3 bottomLeftEdge;
    private Vector3 topRightEdge;
    [SerializeField] Tilemap tilemap;
    private float yLimit = 0.5f;
    private float maxxLimit = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        //singelton
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
        
        //camera limits
        bottomLeftEdge = tilemap.localBounds.min + new Vector3(yLimit, yLimit, 0);
        topRightEdge = tilemap.localBounds.max + new Vector3(-maxxLimit, -yLimit, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //player movement
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        float verticalMovement = Input.GetAxisRaw("Vertical");
        playerRigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * movespeed;

        playerAnimator.SetFloat("movementX", playerRigidBody.velocity.x);
        playerAnimator.SetFloat("movementY", playerRigidBody.velocity.y);

        if(horizontalMovement == move || horizontalMovement == -move || verticalMovement == move || verticalMovement == -move)
        {
            playerAnimator.SetFloat("lastX", horizontalMovement);
            playerAnimator.SetFloat("lastY", verticalMovement);

        }
        //limits for the camera
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bottomLeftEdge.x, topRightEdge.x),
            Mathf.Clamp(transform.position.y, bottomLeftEdge.y, topRightEdge.y),
            Mathf.Clamp(transform.position.z, bottomLeftEdge.z, topRightEdge.z)
        );

    }
}
