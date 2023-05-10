using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineBrain mainBrainCamera;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] CinemachineVirtualCamera playerAimingCamera;
    [SerializeField] CrossHairMoving crossHairMoving;

    private int stateCamera;

    private void Start()
    {
        stateCamera = (int)CAMERA.MAIN;
    }

    public void SwitchState(int state)
    {
        if (stateCamera == state) return;

        switch(state)
        {
            case (int)CAMERA.MAIN: GoToMain(); break;
            case (int)CAMERA.AIMING: GoToAiming(); break;
        }
        stateCamera = state;
    }

    private void GoToMain()
    {
        playerCamera.gameObject.SetActive(true);
        //playerAimingCamera.gameObject.SetActive(false);
        crossHairMoving.EnableAiming(false);
    }

    private void GoToAiming()
    {
        crossHairMoving.EnableAiming(true);
        playerCamera.gameObject.SetActive(false);
    }
}
