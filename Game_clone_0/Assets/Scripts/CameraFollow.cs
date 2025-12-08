using PurrNet;
using PurrNet.Modules;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{   
    // References
    public GameObject player;

    // Look-ahead
    public float lookDistance;
    public float zoom;
    private Vector2 mousePosition;
    private Vector2 screenSize;

    // Camera shake
    private float cameraShake;
    private float shakeAmount;

    // Start camera shake
    public void ShakeCamera(float amount, float length)
    {
        cameraShake = length;
        shakeAmount = amount;
    }

    void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        // Look-ahead feature
        mousePosition = Input.mousePosition;
        mousePosition -= screenSize / 2;
        mousePosition *= lookDistance / 100;
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -10 * zoom) + player.transform.position;

        // Camera shake loop
        if (cameraShake > 0)
        {
            cameraShake -= Time.deltaTime;
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(-shakeAmount, shakeAmount));
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
