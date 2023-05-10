using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMission : MonoBehaviour
{
    [Header("Mission Config")]
    [SerializeField] int missionID;
    [SerializeField] Transform missionPosition;
    [SerializeField] Sprite missionAvatar;
    

    virtual public void BeginMission()
    {
        GameElements.Instance.miniMapController.SetMission(missionPosition, missionAvatar);
        GameElements.Instance.missionWayPoint.UpdateNewTargetMission(missionPosition);
    }

    virtual public void CompletedMission()
    {
        GameElements.Instance.miniMapController.HideMissionIcon();
        GameElements.Instance.missionsSystem.currentMainMission.MiniCompleted();
    }

    public Sprite GetAvatar()
    {
        return missionAvatar;
    }

    public int GetMissionID()
    {
        return missionID;
    }

    public Transform GetMissionPosition()
    {
        return missionPosition;
    }
}
