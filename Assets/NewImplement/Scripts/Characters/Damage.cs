using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == (int)LayerMask.NameToLayer(GameConstants.CHARACTERLAYER))
        {
            other.gameObject.GetComponent<HumanController>().OnDamage(damage, transform.position - other.transform.position);
        }
    }
}
