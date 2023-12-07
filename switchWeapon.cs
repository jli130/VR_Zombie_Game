using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int selectedWeapon = 0;
    void Start()
    {
        selectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previous = selectedWeapon;
        if(OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)){
            if(selectedWeapon >= transform.childCount-1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
            
        }
        if(previous != selectedWeapon)
        {
            selectWeapon();
        }
    }

    void selectWeapon()
    {
        int i = 0;
        foreach(Transform weapon in transform)
        {
            if(i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);

            }
            i++;
        }
    }
}
