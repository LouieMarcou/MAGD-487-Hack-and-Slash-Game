using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform weaponObjectsContainer;
    [SerializeField] private WeaponData startingWeapon;

    // Start is called before the first frame update
    void Start()
    {
        AddWeapon(startingWeapon);
    }

    //Creates weapon for player, places it in the weapon holder, and stores its data
    public void AddWeapon(WeaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);

        weaponGameObject.GetComponent<WeaponBase>().SetData(weaponData);
        weaponGameObject.GetComponent<WeaponBase>().StoreOrginialData(weaponData);
        weaponGameObject.GetComponent<WeaponBase>().playerController = playerController;
        playerController.SetWeapon(weaponGameObject);
    }
}
