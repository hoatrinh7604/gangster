using UnityEngine;
using System.Collections;

public class ShootCame : MonoBehaviour {

	public Transform TargetPos,Player;
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (TargetPos&&ShootScript.instance.Shooting) 
		{
//			GetComponent<PlayerCamera> ().enabled = false;
//			transform.position = TargetPos.position;
//			transform.LookAt (Player.position+Player.transform.up*2.5f);
		}
	}
}
