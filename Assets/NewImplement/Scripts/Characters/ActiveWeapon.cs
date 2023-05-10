using System;
using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEditor.Animations;
using UnityEngine.Animations.Rigging;

public class ActiveWeapon : MonoBehaviour
{
    public enum WEAPONSLOT
    {
        Primary = 0,
        Secondary
    }

    public Transform crossHairTarget;
    public Rig handIK;
    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;
    public Animator rigController;
    public Transform[] weaponSlots;
    public CinemachineFreeLook playerCamera;
    public AmmoWidget ammoWidget;
    public bool isChangingWeapon = false;

    RaycastWeapon[] equipment_waepons = new RaycastWeapon[2];
    int activeWeaponIndex;
    int isSprintingLayerParam;
    bool isHolstered = false;


    // Start is called before the first frame update
    void Start()
    {
        RaycastWeapon existWeapon = GetComponentInChildren<RaycastWeapon>();
        if(existWeapon)
        {
            Equip(existWeapon);
        }
    }

    public bool IsFiring()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if(!currentWeapon)
        {
            return false;
        }

        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equipment_waepons.Length)
        {
            return null;
        }

        return equipment_waepons[index];
    }

    // Update is called once per frame
    void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        if(weapon && !isHolstered)
        {
            weapon.UpdateWeapon(Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ToggleActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActiveWeapon(WEAPONSLOT.Primary);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveWeapon(WEAPONSLOT.Secondary);
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if(weapon)
        {
            Destroy(weapon.gameObject);
        }

        weapon = newWeapon;
        weapon.rayCastDestination = crossHairTarget;
        weapon.recoil.playerCamera = playerCamera;
        weapon.recoil.rigController = rigController;
        weapon.transform.parent = weaponSlots[weaponSlotIndex];
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        weapon.transform.localScale = Vector3.one;

        equipment_waepons[weaponSlotIndex] = weapon;
        SetActiveWeapon(newWeapon.weaponSlot);

        ammoWidget.Refresh(weapon.ammoCount);
    }

    void ToggleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if(isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    void SetActiveWeapon(WEAPONSLOT weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex = -1;
        }

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
        isHolstered = true;
        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holster_weapon", true);
            ControlSprintingWeightLayer(false);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        isChangingWeapon = false;
    }

    IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            ControlSprintingWeightLayer(true);
            rigController.Play("equip_" + weapon.weaponName);
            do
            {
                yield return new WaitForEndOfFrame();
            } while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
        }
        isChangingWeapon = false;
    }

    private void ControlSprintingWeightLayer(bool isEnable)
    {
        isSprintingLayerParam = rigController.GetLayerIndex("Sprinting Layer");
        rigController.SetLayerWeight(isSprintingLayerParam, isEnable ? 1 : 0);
    }

    //[ContextMenu("Save Weapon Pose")]
    //void SaveWeaponPose()
    //{
    //    GameObjectRecorder recorder = new GameObjectRecorder(gameObject);
    //    recorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
    //    recorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
    //    recorder.TakeSnapshot(0.0f);
    //    recorder.SaveToClip(weapon.weaponAnimation);
    //    UnityEditor.AssetDatabase.SaveAssets();
    //}
}
