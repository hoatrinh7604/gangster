using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponPickCharacter : MonoBehaviour
{
    [SerializeField] WeaponController weaponController;
    [SerializeField] int type;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        weaponController = GameElements.Instance.weaponController;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetAll();
        }
    }

    public void GetWeapon(int type)
    {
        weaponController.SetupEnableWeapon(type);
    }

    private void GetAll()
    {
        for(int i = 1; i<= Enum.GetNames(typeof(WEAPON)).Length; i++)
        {
            GetWeapon(i);
        }
    }
}
