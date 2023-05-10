using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionWayPoint : MonoBehaviour
{
    [SerializeField] Image img;
    [SerializeField] Transform target;
    [SerializeField] Text textMeters;
    [SerializeField] Vector3 offset;
    [SerializeField] float hideDistance = 10;
    [SerializeField] float distanceToComplete = 1;
    [SerializeField] float border = 100;

    [SerializeField] Transform[] listSuggestMissionWays;
    [SerializeField] Transform playerPosition;
    private Transform basePosition;

    float minX, maxX, minY, maxY;
    float width, height;
    Vector2 wayPointPosition;
    int meters = 0;
    private Camera cacheCamera;
    int currentIndex = 0;

    private void Start()
    {
        cacheCamera = Camera.main;
        basePosition = cacheCamera.transform;

        SetupAtFirst();
    }

    private void Update()
    {
        ControlWayPoint();
        AutoHide();
    }

    public void ControlWayPoint ()
    {
        if (target == null) return;

        wayPointPosition = cacheCamera.WorldToScreenPoint(target.position + offset);

        if (Vector3.Dot((target.position - basePosition.position), basePosition.forward) < 0)
        {
            if (wayPointPosition.x < Screen.width / 2)
            {
                wayPointPosition.x = maxX;
            }
            else
            {
                wayPointPosition.x = minX;
            }

            wayPointPosition.y = minY;
        }

        wayPointPosition.x = Mathf.Clamp(wayPointPosition.x, minX, maxX);
        wayPointPosition.y = Mathf.Clamp(wayPointPosition.y, minY, maxY);

        img.transform.position = wayPointPosition;
        meters = (int)Vector3.Distance(target.position, playerPosition.position);
        textMeters.text = meters + "m";

        ControlShowSuggestWayPoint();
    }

    public void AutoHide()
    {
        if (target == null) return;
        img.gameObject.SetActive(Vector3.Distance(target.position, playerPosition.position) >= hideDistance);
    }

    public void UpdateNewTargetMission(bl_MiniMapItem targetMission)
    {
        target = targetMission.transform;
    }

    public void UpdateNewTargetMission(Transform targetMission)
    {
        target = targetMission.transform;
        img.gameObject.SetActive(true);
    }

    public void HideSuggest()
    {
        target = null;
        img.gameObject.SetActive(false);
    }

    public void ControlShowSuggestWayPoint()
    {
        if(Vector3.Distance(target.position, playerPosition.position) <= distanceToComplete)
        {
            //listSuggestMissionWays[currentIndex].gameObject.SetActive(false);
            //currentIndex++;
            //if (currentIndex >= listSuggestMissionWays.Length) currentIndex = listSuggestMissionWays.Length - 1;
            //target = listSuggestMissionWays[currentIndex];
            //listSuggestMissionWays[currentIndex].gameObject.SetActive(true);
            //StartCoroutine(ChangeTheNewWayPoint(0.5f));
            HideSuggest();
        }
    }

    public void HideAllSuggestPoint()
    {
        //foreach(var item in listSuggestMissionWays)
        //{
        //    item.gameObject.SetActive(false);
        //}
    }

    IEnumerator ChangeTheNewWayPoint(float time = 0)
    {
        yield return new WaitForSeconds(time);
        currentIndex++;
        if (currentIndex >= listSuggestMissionWays.Length) currentIndex = listSuggestMissionWays.Length - 1;
        target = listSuggestMissionWays[currentIndex];
        listSuggestMissionWays[currentIndex].gameObject.SetActive(true);
    }

    public void SetupAtFirst()
    {
        HideAllSuggestPoint();
        //target = listSuggestMissionWays[currentIndex];
        //listSuggestMissionWays[currentIndex].gameObject.SetActive(true);
        HideSuggest();
        width = img.GetPixelAdjustedRect().width / 2;
        height = img.GetPixelAdjustedRect().height / 2;
        minX = width + border;
        maxX = Screen.width - minX;

        minY = height + border;
        maxY = Screen.height - minY;
    }
}
