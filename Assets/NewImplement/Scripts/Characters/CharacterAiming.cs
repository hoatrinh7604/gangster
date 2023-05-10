using UnityEngine.Animations.Rigging;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15;
    public float aimDuration = 0.3f;
    public Rig aimLayer;
    RaycastWeapon weapon;
    Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        weapon = GetComponentInChildren<RaycastWeapon>();
    }

    private void FixedUpdate()
    {
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        
    }
}
