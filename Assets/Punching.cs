using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Punching : MonoBehaviour {

	public Image barz;

	public GameObject complete;
	public GameObject patiL;
	public GameObject patiR;
	public GameObject patiB;
    public static bool temp;

    //	Animator anim;
    // Use this for initialization
    void Start ()
	{
        temp = true;
    }
	void comp()
	{

        if (temp)
        {
            complete.SetActive(true);
            PlayerPrefs.SetInt("totalscore", PlayerPrefs.GetInt("totalscore") + 2000);
            temp = false;
            AdsManager.Instance.SetStatusWithDelay(ReachedPage.LevelFail, AdsManager.Instance.levelFailAdDelay);
            //Time.timeScale = 0;
        }
    }

	
	// Update is called once per frame
	void Update ()
	{
		if (barz.fillAmount==0) 		
		{
			print ("working");

			Invoke ("comp", 1.25f);



		}
	}

	void OnTriggerEnter(Collider Other)
	{

		//print ("Enemy Head");
		if (Other.gameObject.CompareTag("EnemyHead"))
		{
			patiB.SetActive (true);
			print ("Enemy Head");
			barz.fillAmount -= 0.1f;
//			Other.gameObject.GetComponent<Animator>().
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_Hit_m_MidFront_Med");


		}

		if (Other.gameObject.CompareTag("EnemyChest"))
		{
			patiB.SetActive (true);
			print ("EnemyChest");
			barz.fillAmount -= 0.1f;
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_Hit_m_MidFront_Med");
		}

		if (Other.gameObject.CompareTag("EnemyLH"))
		{
			patiL.SetActive (true);
			print ("EnemyLH");
			barz.fillAmount -= 0.1f;
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_Hit_m_MidFront_Med");
		}


		if (Other.gameObject.CompareTag("EnemyRH"))
		{patiR.SetActive (true);
			print ("EnemyRH");
			barz.fillAmount -= 0.1f;
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_Hit_m_MidFront_Med");
		}


		if (Other.gameObject.CompareTag("EnemyBack"))
		{patiB.SetActive (true);
			print ("EnemyBack");
//			Vector3 dir=new Vector3( gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
//			target.transform.forward = dir.normalized*90 ;
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_TurnR_180");
			barz.fillAmount -= 0.1f;


		}

		if (Other.gameObject.CompareTag("EnemyLhCol"))
		{
			patiL.SetActive (true);
			print ("EnemyLhCol");
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_TurnL_90");
			barz.fillAmount -= 0.1f;


		}

		if (Other.gameObject.CompareTag("EnemyRhCol"))
		{

			patiR.SetActive (true);
			print ("EnemyRhCol");
			Animator anim = Other.gameObject.GetComponentInParent<Animator>();
			anim.Play ("KB_TurnR_90");
			barz.fillAmount -= 0.1f;


		}

		 


	}

}











