using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsSystem : MonoBehaviour
{
    [SerializeField] AMainMission[] listMainMissions;
    [SerializeField] GameObject detailsPopup;
    [SerializeField] GameObject taskPopup;
    private int currentMission;
    public AMainMission currentMainMission;

    public void SetMission(int mainMissionID)
    {
        currentMission = mainMissionID;
        currentMainMission = listMainMissions[currentMission];
        currentMainMission.StartMission();
        taskPopup.SetActive(true);
    }

    public void CompletedAMainMission(int mainMissionID)
    {
        //HandleSetNextMainMission(mainMissionID);
        detailsPopup.SetActive(true);
    }

    public void HandleSetNextMainMission(int mainMissionID)
    {
        detailsPopup.SetActive(false);
        SetMission(Random.Range(0, listMainMissions.Length));
    }
}
