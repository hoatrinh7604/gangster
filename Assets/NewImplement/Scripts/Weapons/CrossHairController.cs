using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Image crossImage;
    private Transform playerTransform;

    private bool moving = false;
    private bool isAutoAim = false;
    public Vector3 currentDirection;
    private void OnEnable()
    {
        moving = true;    
    }

    private void OnDisable()
    {
        moving = false;
    }

    private void Update()
    {
        //if (!moving) return;
        HandleCrossHair();
    }

    public void SetAutoAim(bool isAuto, Transform playerTransform = null)
    {
        isAutoAim = isAuto;
        this.playerTransform = playerTransform;
        if(!isAutoAim)
        {
            transform.GetComponent<RectTransform>().localPosition = Vector3.zero;
        }
    }

    public void HandleCrossHair()
    {
        if(isAutoAim)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.position);
        }
        else
        {
            Vector3 ve = Camera.main.ScreenToWorldPoint(transform.position);
            Ray ray = Camera.main.ScreenPointToRay(transform.position);
            RaycastHit hit;
            currentDirection = Camera.main.transform.position + (ray.direction) * 100;
            if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hit, 300))
            {
                if(hit.transform.gameObject.layer != (int)LayerMask.NameToLayer("Player"))
                    target.position = hit.point;
            }
            else
            {
                target.position = currentDirection;
            }
        }
    }

    public void SwitchCamera()
    {
        transform.position = Camera.main.WorldToScreenPoint(target.position);
    }

    public void ShowCrossHair(bool isShown)
    {
        moving = false;
        crossImage.enabled = isShown;
        Invoke("Reload", 2);
    }

    public void Reload()
    {
        moving = true;
    }
}
