using PurrNet;
using PurrNet.Modules;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float lookDistance;
    public float zoom;
    private Vector2 mousePosition;
    private Vector2 screenSize;
    


    void Awake()
    {
        screenSize = new Vector2(Screen.width, Screen.height);
    }

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition -= screenSize / 2;
        mousePosition *= lookDistance / 100 ;
        transform.position = new Vector3(mousePosition.x, mousePosition.y, -10 *zoom) + player.transform.position;

    }
}
