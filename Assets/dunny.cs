using UnityEngine;
using System.Collections;

public class dunny : MonoBehaviour {
	public Transform target;
	public Animator anim;
	UnityEngine.AI.NavMeshAgent agent;

	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
		agent= GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {

//		Debug.Log (Vector3.Distance (gameObject.transform.position, target.transform.position));
		if (Vector3.Distance (gameObject.transform.position, target.transform.position) > 50)
		{
			print ("farrrr");
			agent.Resume ();
			agent.SetDestination (target.transform.position);
			//animator.Play ("Run");
			anim.SetInteger ("runvalue", 1);
		}
		if (Vector3.Distance (gameObject.transform.position, target.transform.position) < 50) 
		{
			print ("near");
			anim.SetInteger ("runvalue", 2);
			agent.Stop ();
			anim.Play ("KB_m_OneTwo");
		}

			
			
//		}

	}
}
