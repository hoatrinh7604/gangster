using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPositionMission : MiniMission
{
    [SerializeField] Collider missionCollider;
    [SerializeField] GameObject missionObject;

    private void Start()
    {
        ControlObject(false);
    }

    public override void BeginMission()
    {
        base.BeginMission();
        ControlObject(true);
    }

    public override void CompletedMission()
    {
        base.CompletedMission();
        ControlObject(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            CompletedMission();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            CompletedMission();
        }
    }

    public void ControlObject(bool isShown)
    {
        missionObject.SetActive(isShown);
        missionCollider.enabled = isShown;
    }
}
