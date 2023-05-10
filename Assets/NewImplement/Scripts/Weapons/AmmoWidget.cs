using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoWidget : MonoBehaviour
{
    public Text ammoCountText;

    public void Refresh(int ammoCount)
    {
        ammoCountText.text = ammoCount.ToString();
    }
}
