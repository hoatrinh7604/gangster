using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using System;

public class WeaponController : MonoBehaviour
{
    [SerializeField] GameObject[] listWeapon;
    [SerializeField] GameObject currentWeapon;

    [SerializeField] GameObject vehiclePanel;
    public Rigidbody rigidBody;

    [SerializeField] AutoAiming autoAiming;
    [SerializeField] WeaponPickController weaponPickController;
    [SerializeField] GameObject[] weaponOriginGroup;

    [SerializeField] Button[] weaponPickButtons;

    [SerializeField] Button punchButton;
    [SerializeField] Button unEquipButton;

    private void Start()
    {
        SetupButton();
    }

    private void Update()
    {
        if(vehiclePanel.gameObject.activeSelf)
        {
            UnEquip();
        }

        //if(PlayerPrefs.GetInt("OnAttack")==0)
        //{
        //    UnAttack();
        //}
    }

    public void Equip(int ID)
    {
        HideAllWeapon(true);
        HideAllWeaponPick(true);
        foreach (var weapon in listWeapon)
        {
            if (weapon.GetComponent<Weapon>().ID == ID)
            {
                Equip(weapon);
                return;
            }
        }
    }

    public void Equip(GameObject item)
    {
        UnEquipNow();
        item.SetActive(true);
        currentWeapon = item;
        currentWeapon.GetComponent<Weapon>().Equip();

        weaponPickController.HandleDisplayListWeapon();
        SwictchUnequipTransform();
    }

    public void UnEquip()
    {
        currentWeapon.GetComponent<Weapon>().UnEquip();
        //HideAllWeapon(true);
        HideAllWeaponPick(false);
        Invoke("SwictchEquipTransform", 1f);
    }

    public void UnEquipNow()
    {
        if (currentWeapon == null) return;
        currentWeapon.GetComponent<Weapon>().UnEquipNow();
    }

    public void SwictchEquipTransform()
    {
        punchButton.gameObject.SetActive(true);
        unEquipButton.gameObject.SetActive(false);
    }

    public void SwictchUnequipTransform()
    {
        punchButton.gameObject.SetActive(false);
        unEquipButton.gameObject.SetActive(true);
    }

    public void Attack()
    {
        currentWeapon.GetComponent<Weapon>().Attack();
    }

    public void UnAttack()
    {
        if(currentWeapon)
            currentWeapon.GetComponent<Weapon>().UnAttack();
    }
    
    public void OnAttack()
    {
        currentWeapon.GetComponent<Weapon>().OnAttack(rigidBody);
    }

    public void HideAllWeapon(bool isHidden)
    {
        foreach(var weapon in listWeapon)
        {
            weapon.SetActive(!isHidden);
        }
    }

    public void HideAllWeaponPick(bool isHidden)
    {
        //foreach (var weapon in listWeaponPicked)
        //{
        //    weapon.SetActive(!isHidden);
        //}
    }

    public AutoAiming GetAutoAiming()
    {
        return autoAiming;
    }

    public void SetupButton()
    {
        for(int i = 0; i< weaponPickButtons.Length; i++)
        {
            int j = i;
            weaponPickButtons[i].onClick.AddListener(delegate { Equip(j + 1); });
        }
    }

    public void SetupEnableWeapon(int type)
    {
        if (type > Enum.GetNames(typeof(WEAPON)).Length) return;
        foreach (var item in weaponPickButtons)
        {
            if(item.GetComponent<WeaponID>().GetID() == type)
            {
                item.gameObject.SetActive(true);
                break;
            }
        }

        foreach (var item in weaponOriginGroup)
        {
            if (item.GetComponent<WeaponID>().GetID() == type)
            {
                item.gameObject.SetActive(true);
                break;
            }
        }

        weaponPickController.UpdatePanelSize();
    }

    private int numOfTiem = 0;
    public void AddAllItem()
    {
        SetupEnableWeapon(numOfTiem++);
    }

}
