using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class score : MonoBehaviour {

	public  int Score;        // The player's score.
	public static  int totalscore=100;     

	 public Text t;                      // Reference to the Text component.


	void Awake ()
	{


		Score = PlayerPrefs.GetInt ("totalscore");
		sc ();
	}


	void Update ()
	{
		// Set the displayed text to be the word "Score" followed by the score value.


		sc ();

	}


	void sc()
	{
		t.text = "" + Score.ToString();

	}


}

