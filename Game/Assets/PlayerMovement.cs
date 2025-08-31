using System;
using PurrNet;
using UnityEngine;

public class PlayerMovement : NetworkIdentity
{
    // Initialise Variables
    [SerializeField] private float speed;
    [SerializeField] private new Camera camera;
    [SerializeField] private new Rigidbody2D rigidbody;
    private Vector2 input;
    private Vector3 toMouse;
    private new GameObject networkManager;
    private PlayerManager playerManager;

    // On Spawn
    protected override void OnSpawned()
    {
        base.OnSpawned();
        enabled = isOwner;
        
        if (isOwner)
        {
            camera = Camera.main;

            // Get reference to Network Manager and set local player clone as player
            networkManager = GameObject.FindGameObjectWithTag("NetworkManager");
            playerManager = (PlayerManager)networkManager.GetComponent("PlayerManager");
            playerManager.SetReferences(gameObject);
        }
        
    }

    void FixedUpdate()
    {

        // Get movement
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
        rigidbody.linearVelocity = input * speed;

        // Rotate player towards mouse
        Vector3 screenMousePosition = Input.mousePosition;
        screenMousePosition.z = -camera.transform.position.z;
        toMouse = transform.position - camera.ScreenToWorldPoint(screenMousePosition);
        rigidbody.SetRotation((float)MathF.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);


    }

}
