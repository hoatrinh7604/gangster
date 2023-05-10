using UnityEngine;
using System.Collections;

public class streetlight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnCollisionEnter(Collision Other)
	{
		if (Other.gameObject.CompareTag("Vehicle"))
		{
			print ("streetlight");
			Rigidbody gameObjectsRigidBody = gameObject.AddComponent<Rigidbody>(); 
		}
	}

		
}
