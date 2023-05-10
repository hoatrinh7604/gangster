//----------------------------------------------
//            Realistic Car Controller
//
// Copyright © 2015 BoneCracker Games
// http://www.bonecrackergames.com
//
//----------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class RCCUIController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

	public float input;
	public float sensitivity = 3f;
	public bool pressing;
	public static bool prezzing =false;

	public void OnPointerDown(PointerEventData eventData){
		prezzing = true;
		pressing = true;
		Invoke ("m",0.15f);
	}

	public void OnPointerUp(PointerEventData eventData){
		prezzing = false;
		pressing = false;
		print (prezzing);
		ShootScript.instance.PressedExit ();

	}
	void m()
	{
		if (pressing) {
			Debug.Log ("jkjfbkjsbf");
			ShootScript.instance.PressedFun ();
		}
	}
	void Update(){
//		Debug.Log (pressing);
		
		if (pressing) {
			
//			input += Time.deltaTime * sensitivity;
		} else {
//			ShootScript.instance.PressedExit ();
//			input -= Time.deltaTime * sensitivity;
		}
		
//		if(input < 0f)
//			input = 0f;
//		if(input > 1f)
//			input = 1f;
		
	}

}
