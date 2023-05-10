using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAiming : MonoBehaviour
{
    [SerializeField] HumanController[] allHumans;
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform targetPoint;
    [SerializeField] Transform shotPoint;
    private Camera mainCamera;
    private bool startAutoAim = false;
    [SerializeField] bool isAuto = false;
    [SerializeField] CrossHairController crossHairController;
    private void OnEnable()
    {
        crossHairController.ShowCrossHair(true);
        startAutoAim = true;
        SetAuto(isAuto);
    }

    private void OnDisable()
    {
        crossHairController.ShowCrossHair(false);
        startAutoAim = false;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        UpdateHumanList();

        SetAuto(isAuto);
    }

    public bool IsVisible(Camera camera, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        var point = target.transform.position;

        foreach(var plane in planes)
        {
            if(plane.GetDistanceToPoint(point) < 0)
            {
                return false;
            }
        }

        return true;
    }

    private void Update()
    {
        delayUpdate -= Time.deltaTime;
        if (isAuto)
        {
            AutoAim();
            Target();
        }
        else
        {
            Aiming();
        }
    }

    bool isAimingLookAt = false;
    public void Aiming()
    {
        //if(!isAimingLookAt)
        //{
        //    playerTransform.transform.forward = crossHairController.currentDirection;
        //}
        //playerTransform.transform.LookAt(targetPoint);
    }

    public void SetPlayeLookAt(bool isAiming)
    {
        //playerTransform.transform.LookAt(isAiming ? targetPoint : null);
        //isAimingLookAt = isAiming;

        //if(isAiming)
        //{
        //    playerTransform.transform.LookAt(targetPoint);
        //}
    }

    public void AutoAim()
    {
        if (!startAutoAim) return;
        if (delayUpdate > 0) return;

        foreach(var character in allHumans)
        {
            if(IsVisible(mainCamera, character.gameObject))
            {
                currentTarget = character.transform;
                delayUpdate = 2;
                return;
            }
        }

    }

    public void SetAuto(bool isAuto)
    {
        this.isAuto = isAuto;
        crossHairController.SetAutoAim(isAuto, playerTransform);
    }

    private float delayUpdate = 0;
    private Transform currentTarget;
    public void UpdateHumanList()
    {
        if (!startAutoAim) return;
        allHumans = FindObjectsOfType<HumanController>();
        Invoke("UpdateHumanList", 5);
    }

    public void Target()
    {
        targetPoint.position = new Vector3(currentTarget.position.x, currentTarget.position.y, currentTarget.position.z);
    }

    public void MakeTargetToCenterCamera()
    {
        targetPoint.position = shotPoint.position.normalized * 100;
    }

    public Transform GetTarget()
    {
        return targetPoint;
    }

    public void SwitchCamera()
    {
        crossHairController.SwitchCamera();
    }
}
