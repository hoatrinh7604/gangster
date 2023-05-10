using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using GameAnalyticsSDK;
public class Timer : MonoBehaviour {



		public float timeLeft = 0.0f;
		public  bool stop = true;

	public static  bool levelfailedz =false;
	public static Timer instance;

		private float minutes;
		private float seconds;

		public Text text;

		void Start()
		{
		            text = GameControl.manager.Timertext;
					StartCoroutine(updateCoroutine());

		}
		public void startTimer(float from){
			stop = false;
			timeLeft = from;
			Update();
			StartCoroutine(updateCoroutine());
		}

		void Update()
		{
			if (!stop) {
				timeLeft -= Time.deltaTime;

				minutes = Mathf.Floor (timeLeft / 60);
				seconds = timeLeft % 60;
				if (seconds > 59)
					seconds = 59;
				if (minutes < 0) {
					stop = true;
					minutes = 0;
					seconds = 0;
				}
			}
		if (timeLeft <= 0) {
			print ("LevelFail");

			Invoke ("f", 1.75f);

		} 

		}


	public void f()
	{
        //if (GirlGameConfigs.mee) {
        //	GirlGameConfigs.mee.showRotationAds (1,AdsPageType.prelf);
        //}
        AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
        Invoke("MakeInGame", 2);
        levelfailedz = true;
		//MONSTER REM GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "Level" + PlayerPrefs.GetInt("Levelclick").ToString());
	}

	private IEnumerator updateCoroutine(){
			while(!stop)
			{
				if(!text)
				text =  GameControl.manager.Timertext;;

				text.text = string.Format("{0:0}:{1:00}", minutes, seconds);
				yield return new WaitForSeconds(0.2f);
			}
		}
    void MakeInGame()
    {
        AdsManager.Instance.SetStatus(ReachedPage.InGame);
    }
}

