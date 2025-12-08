using PurrNet.Modules;
using UnityEditor.UI;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    [SerializeField] new Camera camera;
    private CameraFollow cameraFollow;


    public void SetReferences(GameObject setPlayer)
    {
        player = setPlayer;
        cameraFollow = (CameraFollow)camera.GetComponent("CameraFollow");
        cameraFollow.player = setPlayer;
    }
    
}
