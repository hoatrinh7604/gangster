using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillNPCMission : MiniMission
{
    [SerializeField] Collider missionCollider;
    [SerializeField] GameObject[] listNPC;
    [SerializeField] GameObject chatPopup;
    [SerializeField] string textChat;
    [SerializeField] GameObject missionObject;

    private bool isMissionCompleted = false;
    private void Start()
    {
        chatPopup = GameElements.Instance.chatDialogPopup;
        //ControlObject(false);
    }

    private void Update()
    {
        CheckMission();
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
            EnemyWakeUp();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            //CompletedMission();
            ShowDialog();
            EnemyWakeUp();
        }
    }

    public void CheckMission()
    {
        if (isMissionCompleted) return;

        bool isKillAll = true;
        foreach(var enemy in listNPC)
        {
            if (!enemy.GetComponent<HumanController>().IsDeath())
            {
                isKillAll = false;
                break;
            }
        }

        if(isKillAll)
        {
            isMissionCompleted = true;
            CompletedMission();
        }

        
    }

    private void EnemyWakeUp()
    {
        PlayerPrefs.SetInt("ActivateEnemy", 1);
    }

    public void ShowDialog()
    {
        if (!chatPopup)
        {
            //CompletedMission();
            return;
        }
        chatPopup.SetActive(true);
        chatPopup.GetComponentInChildren<Text>().text = textChat;
        

        Invoke("AutoHidePopup", 1);
    }

    public void AutoHidePopup()
    {
        missionCollider.enabled = false;
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
