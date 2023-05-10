using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fader : MonoBehaviour {
	public static fader instance;
	public Image Fader;
	public Text DebugText;
	void Awake () 
	{
		//		Debug.Log ("GM"+SystemInfo.graphicsMemorySize+" : GM"+SystemInfo.systemMemorySize);

		//		DebugText.text = "GM"+SystemInfo.graphicsMemorySize+" : GM"+SystemInfo.systemMemorySize;
		//		if (SystemInfo.systemMemorySize < 1024) {
		//			Screen.SetResolution ((int)(Screen.width * 0.80f), (int)(Screen.height * 0.80f), true);
		//		} else 
		//		{
		//			Screen.SetResolution ((int)(Screen.width ), (int)(Screen.height ), true);
		//		}
		if (instance) {
			Destroy (this.gameObject);
		} else {
			instance=this;
			DontDestroyOnLoad (this.gameObject);
		}
	}
	public void FadeIn(bool fade)
	{
		if (fade) {
			iTween.ValueTo (gameObject, iTween.Hash ("from", 1, "to", 0f, "onupdate", "ChangeAlpha", "time", 0.8f));
		} else {
			iTween.ValueTo (gameObject, iTween.Hash ("from", 0, "to", 1f, "onupdate", "ChangeAlpha", "time", 0.8f));

		}

	}	

	public void ChangeAlpha(float alpha  )
	{
		Fader.color = new Color (0, 0, 0, alpha);
	}
}
