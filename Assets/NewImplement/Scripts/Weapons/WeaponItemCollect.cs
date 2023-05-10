using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItemCollect : MonoBehaviour
{
    [SerializeField] WeaponPickCharacter weaponPickCharacter;

    private void Start()
    {
        weaponPickCharacter = GameElements.Instance.weaponPickCharacter;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            if(!weaponPickCharacter)
                weaponPickCharacter = GameElements.Instance.weaponPickCharacter;
            weaponPickCharacter.GetWeapon(GetComponent<WeaponID>().GetID());

            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == GameConstants.PLAYER_TAG)
        {
            if (!weaponPickCharacter)
                weaponPickCharacter = GameElements.Instance.weaponPickCharacter;
            weaponPickCharacter.GetWeapon(GetComponent<WeaponID>().GetID());

            Destroy(this.gameObject);
        }
    }
}
