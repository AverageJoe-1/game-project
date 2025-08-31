using System;
using PurrNet;
using UnityEngine;

public class PlayerMovement : NetworkIdentity
{
    [SerializeField] private float speed;
    [SerializeField] private Camera camera;
    [SerializeField] private Rigidbody2D rigidbody;
    private Vector2 input;
    private Vector3 toMouse;

    protected override void OnSpawned()
    {
        base.OnSpawned();
        enabled = isOwner;

        camera = Camera.main;
    }

    void FixedUpdate()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
        rigidbody.linearVelocity = input * speed;

        toMouse = transform.position - camera.ScreenToWorldPoint(Input.mousePosition);
        rigidbody.SetRotation((float)MathF.Atan2(toMouse.y, toMouse.x) * Mathf.Rad2Deg);
        

    }

}
