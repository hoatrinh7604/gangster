using UnityEngine;
using System.Collections;

public class Enemyscript : MonoBehaviour {


	public BoxCollider boxcolenemyL;
	public BoxCollider boxcolenemyR;
	public BoxCollider EnemyChest;
	//public Animator anim;
	public GameObject player;


	public Transform target;

	int dist;
	// Use this for initialization
	public Animator anim;
	UnityEngine.AI.NavMeshAgent agent;


	public static Enemyscript instance;
	public bool activateenemy;

	// Use this for initialization
	void Start () {
//		activateenemy = false;
		PlayerPrefs.SetInt("ActivateEnemy",0);
		anim = this.gameObject.GetComponent<Animator> ();
		agent= GetComponent<UnityEngine.AI.NavMeshAgent> ();
#if MONSTER
		player = GameElements.Instance.player;
		target = player.transform;
#endif
	}
	// Update is called once per frame
	void Update ()
	{
//		Debug.Log (Vector3.Distance (gameObject.transform.position, target.transform.position));
		if (anim.GetCurrentAnimatorStateInfo (0).IsName ("KB_m_OneTwo")) 
		{
			Debug.Log ("Playing");
			boxcolenemyL.enabled = true;
			boxcolenemyR.enabled = true;
			EnemyChest.enabled = false;
		} 
		else
		{
			boxcolenemyL.enabled = false;
			boxcolenemyR.enabled = false;
			EnemyChest.enabled = true;


		}

		if (PlayerPrefs.GetInt("ActivateEnemy")==1) {

			if (Vector3.Distance (gameObject.transform.position, target.transform.position) > 1.2f) {
				print ("farrrr");
				agent.Resume ();
				agent.SetDestination (target.transform.position);
				//animator.Play ("Run");
				anim.SetInteger ("runvalue", 1);
			}
			if (Vector3.Distance (gameObject.transform.position, target.transform.position) < 1.2f) {
				print ("near");
				anim.SetInteger ("runvalue", 2);
				agent.Stop ();
				transform.LookAt (target.transform);
				anim.Play ("KB_m_OneTwo");
			}

		}
	}

	public	void Punch()
	{

	}

	public	void UnPunch()
	{
		
	}


}
