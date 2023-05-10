using UnityEngine;
using System.Collections;

public class LoadingWheel : MonoBehaviour {
	public float num;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0,0,num);
	}
}
