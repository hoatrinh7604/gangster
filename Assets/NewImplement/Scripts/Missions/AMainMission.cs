using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMainMission : MonoBehaviour
{
    [SerializeField] int ID;
    [SerializeField] MiniMission[] listMiniMissions;
    private int currentMiniMission = 1;

    public void StartMission()
    {
        StartMiniMission(1);
    }

    public void MiniCompleted()
    {
        if(IsCompleteAll())
        {
            CompletedAMainMission();
            return;
        }

        SetNextMiniMission();
    }

    public void StartMiniMission(int missionOrder)
    {
        currentMiniMission = missionOrder;
        listMiniMissions[currentMiniMission - 1].BeginMission();
    }

    public void SetNextMiniMission()
    {
        StartMiniMission(++currentMiniMission);
    }

    public void CompletedAMainMission()
    {
        GameElements.Instance.missionsSystem.CompletedAMainMission(ID);
    }

    public bool IsCompleteAll()
    {
        return currentMiniMission >= listMiniMissions.Length;
    }
}
