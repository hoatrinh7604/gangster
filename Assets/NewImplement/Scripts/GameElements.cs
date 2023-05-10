using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameElements : MonoBehaviour
{
    public WeaponController weaponController;
    public CameraController cameraController;
    public WeaponPickCharacter weaponPickCharacter;
    public MiniMapController miniMapController;
    public MissionsSystem missionsSystem;
    public MissionWayPoint missionWayPoint;

    public GameObject chatDialogPopup;
    public GameObject player;
    public static GameElements Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
