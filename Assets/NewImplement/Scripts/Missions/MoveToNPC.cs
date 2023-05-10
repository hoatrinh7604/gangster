using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveToNPC : MiniMission
{
    [SerializeField] Collider missionCollider;
    [SerializeField] GameObject chatPopup;
    [SerializeField] string textChat;
    [SerializeField] GameObject missionObject;

    private void Start()
    {
        chatPopup = GameElements.Instance.chatDialogPopup;
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
        if (other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            ShowDialog();
            //CompletedMission();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            //CompletedMission();
            ShowDialog();
        }
    }

    public void ShowDialog()
    {
        if (!chatPopup)
        {
            CompletedMission();
            return;
        }
        chatPopup.SetActive(true);
        chatPopup.GetComponentInChildren<Text>().text = textChat;
        CompletedMission();
    }

    public void Complete()
    {
        CompletedMission();
    }

    public void ControlObject(bool isShown)
    {
        missionObject.SetActive(isShown);
        missionCollider.enabled = isShown;
    }
}
