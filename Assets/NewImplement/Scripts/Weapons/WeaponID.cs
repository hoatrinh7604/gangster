using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponID : MonoBehaviour
{
    [SerializeField] int ID;

    public int GetID()
    {
        return ID;
    }
}
