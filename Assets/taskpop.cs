using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class taskpop : MonoBehaviour {
	public string gangster;
	public Text gangstertext;
	public int TempCountForContinueBtn;
	public GameObject OKbutn;
	// Use this for initialization
	void Start () {
		gangster = gangstertext.text;
		gangstertext.text = "";

		Invoke ("coro",3.5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void coro()
	{	
		StartCoroutine (Boss2TypeText () );

	}
	IEnumerator Boss2TypeText () 
	{
		Debug.Log("Test");
		foreach (char letter in gangster.ToCharArray()) 
		{

			gangstertext.text += letter;
			TempCountForContinueBtn=TempCountForContinueBtn+1;
			//			if(TempCountForContinueBtn>=135)
			//BossContinueBtn2.gameObject.SetActive(true);
			yield return 0;
			yield return new WaitForSeconds (0.006f);
		}      
	}
}
