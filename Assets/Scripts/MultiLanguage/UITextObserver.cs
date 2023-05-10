using UnityEngine.UI;


public class UITextObserver : Observer {

	public Text textHolder;

	public override void OnLanguageChanged (string value)
	{
        textHolder.text = value;
	}
}
