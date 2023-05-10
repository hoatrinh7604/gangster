using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAd : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.ingame);
        //}
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
