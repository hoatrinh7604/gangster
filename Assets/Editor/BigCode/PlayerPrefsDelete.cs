using UnityEngine;
using UnityEditor;

public class PlayerPrefsDelete : EditorWindow
{

	[MenuItem ("IGames/PlayerPrefs/Delete %q")]
	public static void DeletePrefs ()
	{
        if (EditorUtility.DisplayDialog("IGames", "Are you sure? Do you wanna delete playerprefs", "Yes","No"))
        {
            PlayerPrefs.DeleteAll();
            EditorUtility.DisplayDialog("IGames", "PlayerPrefs deleted successfully", "Ok");
        }
	}

}
