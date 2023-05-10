//using UnityEngine;
//using System.Collections;
//using UnityEngine.UI;
//
//public class AllPopUps : MonoBehaviour
//{
//	public GameObject BossPopUp1,FirstAssistantPopup1,FirstAssistantPopup2;
//	public GameObject BigMAnPopUp1;
//	public string BoosMessage1,FirstAsstMsg1,BigManMsg1,FirstAsstMsg2;
//	public Text BoxText1,FirstAsstText1,BigManText1,FirstAsstText2;
//	public int TempCountForContinueBtn;
//	public GameObject BossContinueBtn1,FirstAsstContinue1,BigManContinue1,FirstAsstContinue2;
//
//	public GameObject BossPopUp2;
//	public string BoosMessage2;
//	public Text BoxText2;
//	public GameObject BossContinueBtn2;
//
//	public GameObject SecondAssistantPopUp1;
//	public string SecondAssistantMessage1;
//	public Text SecondAssistantText1;
//	public GameObject SecondAssistantContinueBtn1;
//
//	public GameObject BigMAnPopUp2;
//	public string BigMAnMessage2;
//	public Text BigMAnText2;
//	public GameObject BigMAnContinueBtn2;
//
//	public GameObject ThirdAssistantPopUp1;
//	public string ThirdAssistantMessage1;
//	public Text ThirdAssistantText1;
//	public GameObject ThirdAssistantContinueBtn1;
//	
//	public GameObject BigMAnPopUp3;
//	public string BigMAnMessage3;
//	public Text BigMAnText3;
//	public GameObject BigMAnContinueBtn3;
//
//	public GameObject FourthAssistantPopUp1;
//	public string FourthAssistantMessage1;
//	public Text FourthAssistantText1;
//	public GameObject FourthAssistantContinueBtn1;
//	
//	public GameObject BigMAnPopUp4;
//	public string BigMAnMessage4;
//	public Text BigMAnText4;
//	public GameObject BigMAnContinueBtn4;
//
//	public GameObject BossPopUp3;
//	public string BoosMessage3;
//	public Text BoxText3;
//	public GameObject BossContinueBtn3;
//	void Start () 
//	{
////		PlayerPrefs.SetInt ("SoundBool", 1);
////		PlayerPrefs.SetInt ("MusicBool",1);
////		PlayerPrefs.SetInt ("IsIntroDone",0);
//
//		if (PlayerPrefs.GetInt ("IsIntroDone") == 0) {
//			BossPopUp1.gameObject.SetActive (true);
//		}
//		if (PlayerPrefs.GetInt ("IsIntroDone") ==1) {
//			FirstAssistantPopup1.gameObject.SetActive (true);
//			PlayerPrefs.SetInt ("IsIntroDone",2);
//		}
//		if (PlayerPrefs.GetInt ("UnLockedLevels") ==6) {
////			SoundController.Static.SecondAsstMsg1Sound ();
//			SecondAssistantPopUp1.gameObject.SetActive (true);
//			PlayerPrefs.SetInt ("secondAsstdone", 1);
//		}
//		if (PlayerPrefs.GetInt ("UnLockedLevels") ==11) {
////			SoundController.Static.ThirdAsstMsg1Sound ();
//			ThirdAssistantPopUp1.gameObject.SetActive (true);
//			PlayerPrefs.SetInt ("ThirdAsstdone", 1);
//		}
//		if (PlayerPrefs.GetInt ("UnLockedLevels") ==16) {
////			SoundController.Static.FourthAsstMsg1Sound ();
//			FourthAssistantPopUp1.gameObject.SetActive (true);
//			PlayerPrefs.SetInt ("FourthAsstdone", 1);
//		}
//		if (PlayerPrefs.GetInt ("UnLockedLevels") ==21) {
////			SoundController.Static.BossPopUp3Sound ();
//			BossPopUp3.gameObject.SetActive (true);
//			PlayerPrefs.SetInt ("BossAsst", 1);
//		}
//
//		TempCountForContinueBtn = 0;
//		BoosMessage1 = BoxText1.text;
//		FirstAsstMsg1 = FirstAsstText1.text;
//		BigManMsg1 = BigManText1.text;
//		FirstAsstMsg2 = FirstAsstText2.text;
//		BoosMessage2 = BoxText2.text;
//		SecondAssistantMessage1 = SecondAssistantText1.text;
//		BigMAnMessage2 = BigMAnText2.text;
//		ThirdAssistantMessage1 = ThirdAssistantText1.text;
//		BigMAnMessage3 = BigMAnText3.text;
//		FourthAssistantMessage1 = FourthAssistantText1.text;
//		BigMAnMessage4 = BigMAnText4.text;
//		BoosMessage3 = BoxText3.text;
//		if (BossPopUp1.gameObject.activeInHierarchy)
//		{
//			BoxText1.text = "";
//			Invoke ("CallBoss1TypeText", 1.1f);
//		}
//		if (FirstAsstText1.gameObject.activeInHierarchy)
//		{
//			FirstAsstText1.text = "";
//			Invoke ("CallFirstAsst1TypeText", 1.1f);
//		}
//		if (BigMAnPopUp1.gameObject.activeInHierarchy)
//		{
//			BigManText1.text = "";
//			Invoke ("CallBigManText1TypeText", 1.1f);
//		}
//		if (FirstAsstText2.gameObject.activeInHierarchy)
//		{
//			FirstAsstText2.text = "";
//			Invoke ("CallFirstAsst2TypeText", 1.1f);
//		}
//		if (BossPopUp2.gameObject.activeInHierarchy)
//		{
//			BoxText2.text = "";
//			Invoke ("CallBoss2TypeText", 1.1f);
//		}
//		if (BossPopUp3.gameObject.activeInHierarchy)
//		{
//			BoxText3.text = "";
//			Invoke ("CallBoss3TypeText", 1.1f);
//		}
//		if (SecondAssistantText1.gameObject.activeInHierarchy)
//		{
//			SecondAssistantText1.text = "";
//			Invoke ("CallSecondAsst1TypeText", 1.1f);
//		}
//		if (ThirdAssistantText1.gameObject.activeInHierarchy)
//		{
//			ThirdAssistantText1.text = "";
//			Invoke ("CallThirdAsst1TypeText", 1.1f);
//		}
//		if (FourthAssistantText1.gameObject.activeInHierarchy)
//		{
//			FourthAssistantText1.text = "";
//			Invoke ("CallFourthAsst1TypeText", 1.1f);
//		}
//	}
////	void FirstSound()
////	{
////		SoundController.Static.BossPopUp1Sound ();
////	}
//	void CallBoss1TypeText()
//	{
//		SoundController.Static.BossPopUp1Sound ();
//		StartCoroutine(Boss1TypeText ());
//	}
//	void CallBoss2TypeText()
//	{
//		StartCoroutine(Boss2TypeText ());
//	}
//	void CallBoss3TypeText()
//	{
//		SoundController.Static.BossPopUp3Sound ();
//		StartCoroutine(Boss3TypeText ());
//	}
//	void CallFirstAsst1TypeText()
//	{
//		SoundController.Static.FirstAsstMsg1Sound ();
//		StartCoroutine(FirstAsst1TypeText ());
//	}
//	void CallFirstAsst2TypeText()
//	{
//		StartCoroutine(FirstAsst2TypeText ());
//	}
//	void CallBigManText1TypeText()
//	{
//		StartCoroutine(BigMan1TypeText ());
//	}
//	void CallSecondAsst1TypeText()
//	{
//		SoundController.Static.SecondAsstMsg1Sound ();
//		StartCoroutine(SecondAsst1TypeText ());
//	}
//	void CallBigMAn2TypeText()
//	{
//		StartCoroutine(BigMan2TypeText ());
//	}
//	void CallThirdAsst1TypeText()
//	{
//		SoundController.Static.ThirdAsstMsg1Sound ();
//		StartCoroutine(ThirdAsst1TypeText ());
//	}
//	void CallBigMAn3TypeText()
//	{
//		StartCoroutine(BigMan3TypeText ());
//	}
//	void CallFourthAsst1TypeText()
//	{
//		SoundController.Static.FourthAsstMsg1Sound ();
//		StartCoroutine(FourthAsst1TypeText ());
//	}
//	void CallBigMAn4TypeText()
//	{
//		StartCoroutine(BigMan4TypeText ());
//	}
//	IEnumerator Boss1TypeText () 
//	{
//		foreach (char letter in BoosMessage1.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			BoxText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=138)
//				BossContinueBtn1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator Boss2TypeText () 
//	{
//		foreach (char letter in BoosMessage2.ToCharArray()) 
//		{
////						Debug.Log("Test");
//			BoxText2.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=135)
//				BossContinueBtn2.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator Boss3TypeText () 
//	{
//		foreach (char letter in BoosMessage3.ToCharArray()) 
//		{
//									Debug.Log("Test");
//			BoxText3.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=200)
//				BossContinueBtn3.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator FirstAsst1TypeText () 
//	{
//		foreach (char letter in FirstAsstMsg1.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			FirstAsstText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=78)
//				FirstAsstContinue1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator FirstAsst2TypeText () 
//	{
//		foreach (char letter in FirstAsstMsg2.ToCharArray()) 
//		{
////						Debug.Log("Test");
//			FirstAsstText2.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=58)
//				FirstAsstContinue2.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator BigMan1TypeText () 
//	{
//		foreach (char letter in BigManMsg1.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			BigManText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=125)
//				BigManContinue1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator SecondAsst1TypeText () 
//	{
//		foreach (char letter in SecondAssistantMessage1.ToCharArray()) 
//		{
//						Debug.Log("Test");
//			SecondAssistantText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=92)
//				SecondAssistantContinueBtn1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator BigMan2TypeText () 
//	{
//		foreach (char letter in BigMAnMessage2.ToCharArray()) 
//		{
//						Debug.Log("Test");
//			BigMAnText2.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=112)
//				BigMAnContinueBtn2.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator ThirdAsst1TypeText () 
//	{
//		foreach (char letter in ThirdAssistantMessage1.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			ThirdAssistantText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=117)
//				ThirdAssistantContinueBtn1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator BigMan3TypeText () 
//	{
//		foreach (char letter in BigMAnMessage3.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			BigMAnText3.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=61)
//				BigMAnContinueBtn3.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator FourthAsst1TypeText () 
//	{
//		foreach (char letter in FourthAssistantMessage1.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			FourthAssistantText1.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=119)
//				FourthAssistantContinueBtn1.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	IEnumerator BigMan4TypeText () 
//	{
//		foreach (char letter in BigMAnMessage4.ToCharArray()) 
//		{
////			Debug.Log("Test");
//			BigMAnText4.text += letter;
//			TempCountForContinueBtn=TempCountForContinueBtn+1;
//			if(TempCountForContinueBtn>=174)
//				BigMAnContinueBtn4.gameObject.SetActive(true);
//			yield return 0;
//			yield return new WaitForSeconds (0.01f);
//		}      
//	}
//	void Update () 
//	{
//	
//	}
//	public void ButtonActions(string ClickedBtn)
//	{
//		if (ClickedBtn == "BossContinueBtn1")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			if(BossPopUp1.gameObject.activeInHierarchy)
//			{
////				Debug.Log("ContinueToIntroScene");
////				Application.LoadLevel ("IntroLevel");
//				Application.LoadLevel("Loading");
//				Loading.LevelToLoad = "IntroLevel";
//			}
//		}
//		if (ClickedBtn == "FirstAsstContinue1") 
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("FirstAsstContinue1");
//			Invoke("CallFirstAsstContinue1",0.5f);
//
//		}
//		if (ClickedBtn == "BigManContinue1") 
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("BigManContinue1");
//			Invoke("CallBigManContinue1",0.5f);
//		}
//		if (ClickedBtn == "FirstAsstContinue2") 
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("FirstAsstContinue2");
//			Loading.LevelToLoad = "Level1";
//			Application.LoadLevel ("Loading");
//		}
//		if (ClickedBtn == "BossContinueBtn2")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			if(BossPopUp2.gameObject.activeInHierarchy)
//			{
//				Debug.Log("ContinueToIntroScene");
//			}
//		}
//		if (ClickedBtn == "BossContinueBtn3")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			if(BossPopUp3.gameObject.activeInHierarchy)
//			{
//				Debug.Log("BossContinueBtn3");
//				Loading.LevelToLoad = "Level17";
//				Application.LoadLevel ("Loading");
//			}
//		}
//		if (ClickedBtn == "SecondAssistantContinueBtn1")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("SecondAssistantContinueBtn1");
//			Invoke("CallSecondAsstContinue1",0.5f);
//		}
//		if (ClickedBtn == "BigMAnContinueBtn2")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("BigMAnContinueBtn2");
//			Loading.LevelToLoad = "Level5";
//			Application.LoadLevel ("Loading");
//		}
//		if (ClickedBtn == "ThirdAssistantContinueBtn1")
//		{
//			TempCountForContinueBtn=0;
//			Debug.Log("ThirdAssistantContinueBtn1");
//			Invoke("CallThirdAsstContinue1",0.5f);
//		}
//		if (ClickedBtn == "BigMAnContinueBtn3")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("BigMAnContinueBtn3");
//			Loading.LevelToLoad = "Level9";
//			Application.LoadLevel ("Loading");
//		}
//		if (ClickedBtn == "FourthAssistantContinueBtn1")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("FourthAssistantContinueBtn1");
//			Invoke("CallFourthAsstContinue1",0.5f);
//		}
//		if (ClickedBtn == "BigMAnContinueBtn4")
//		{
//			SoundController.Static.PlayClickSound ();
//			TempCountForContinueBtn=0;
//			Debug.Log("BigMAnContinueBtn4");
//			Loading.LevelToLoad = "Level13";
//			Application.LoadLevel ("Loading");
//		}
//	}
//	void CallFirstAsstContinue1()
//	{
//		iTween.MoveTo (FirstAssistantPopup1.gameObject, iTween.Hash ("x",15000f,"time", 0.8f));
//		BigMAnPopUp1.gameObject.SetActive (true);
//		SoundController.Static.audioSources [1].Stop ();
//		SoundController.Static.BigMAnMsg1Sound ();
//		if (BigMAnPopUp1.gameObject.activeInHierarchy)
//		{
//			BigManText1.text = "";
//			Invoke ("CallBigManText1TypeText", 1.1f);
//		}
//	}
//	void CallBigManContinue1()
//	{
//		iTween.MoveTo (BigMAnPopUp1.gameObject, iTween.Hash ("x",15000f,"time", 0.8f));
//		FirstAssistantPopup2.gameObject.SetActive (true);
//		SoundController.Static.audioSources [1].Stop ();
//		SoundController.Static.FirstAsstMsg2Sound ();
//		if (FirstAsstText2.gameObject.activeInHierarchy)
//		{
//			FirstAsstText2.text = "";
//			Invoke ("CallFirstAsst2TypeText", 1.1f);
//		}
//	}
//	void CallSecondAsstContinue1()
//	{
//		iTween.MoveTo (SecondAssistantPopUp1.gameObject, iTween.Hash ("x",15000f,"time", 0.8f));
//		SoundController.Static.audioSources [1].Stop ();
//		SoundController.Static.BigMAnMsg2Sound ();
//		BigMAnPopUp2.gameObject.SetActive (true);
//		if (BigMAnText2.gameObject.activeInHierarchy)
//		{
//			BigMAnText2.text = "";
//			Invoke ("CallBigMAn2TypeText", 1.1f);
//		}
//	}
//	void CallThirdAsstContinue1()
//	{
//		iTween.MoveTo (ThirdAssistantPopUp1.gameObject, iTween.Hash ("x",15000f,"time", 0.8f));
//		SoundController.Static.audioSources [1].Stop ();
//		SoundController.Static.BigMAnMsg3Sound ();
//		BigMAnPopUp3.gameObject.SetActive (true);
//		if (BigMAnText3.gameObject.activeInHierarchy)
//		{
//			BigMAnText3.text = "";
//			Invoke ("CallBigMAn3TypeText", 1.1f);
//		}
//	}
//	void CallFourthAsstContinue1()
//	{
//		iTween.MoveTo (FourthAssistantPopUp1.gameObject, iTween.Hash ("x",15000f,"time", 0.8f));
//		BigMAnPopUp4.gameObject.SetActive (true);
//		SoundController.Static.audioSources [1].Stop ();
//		SoundController.Static.BigMAnMsg4Sound ();
//		if (BigMAnText4.gameObject.activeInHierarchy)
//		{
//			BigMAnText4.text = "";
//			Invoke ("CallBigMAn4TypeText", 1.1f);
//		}
//	}
//
//}
