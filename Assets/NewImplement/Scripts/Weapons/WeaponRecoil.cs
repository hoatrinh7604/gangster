using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    [HideInInspector] public CinemachineFreeLook playerCamera;
    [HideInInspector] public CinemachineImpulseSource cameraShake;
    [HideInInspector] public Animator rigController;

    public Vector2[] recoilPatern;
    public float duration;

    float verticalRecoil;
    float horizontalRecoil;
    float time;
    int index;

    private void Awake()
    {
        cameraShake = GetComponent<CinemachineImpulseSource>();
    }

    public void Reset()
    {
        index = 0;
    }

    public void GenerateRecoil(string weaponName)
    {
        time = duration;

        cameraShake.GenerateImpulse(Camera.main.transform.forward);

        horizontalRecoil = recoilPatern[index].x;
        verticalRecoil = recoilPatern[index].y;

        index = NextIndex(index);

        rigController.Play("weapon_recoil_" + weaponName, 1, 0.0f);
    }

    int NextIndex(int index)
    {
        return (index + 1) % recoilPatern.Length;
    }

    private void Update()
    {
        if(time > 0)
        {
            playerCamera.m_YAxis.Value -= (verticalRecoil / 1000 * Time.deltaTime) / duration;
            playerCamera.m_XAxis.Value -= (horizontalRecoil/10 * Time.deltaTime) / duration;
            time -= Time.deltaTime;
        }
    }
}
