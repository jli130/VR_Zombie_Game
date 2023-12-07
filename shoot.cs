using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class shoot : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject bulletPrefab = null;
    public int maxAmmo = 20;
    public int curAmmo;
    public float reloading = 3.0f;
    public bool isReloading = false;
    public Animator animator;
    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [Tooltip("Bullet Speed")][SerializeField] private float shotPower = 200000f;
    // Start is called before the first frame update
    void Start()
    {
        curAmmo = maxAmmo;
    }

    // Update is called once per frame

    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            StartCoroutine(reload());
            return;
        }
        //If you want a different input, change it here
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && curAmmo >= 0)
        {
            //Calls animation on the gun that has the relevant animation events that will fire
            Shoot();
            source.PlayOneShot(clip);
        }
        
    }

    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("reloading", false);
    }
    IEnumerator reload()
    {
        isReloading = true;
        animator.SetBool("reloading", true);

        yield return new WaitForSeconds(reloading);
        curAmmo = maxAmmo;
        animator.SetBool("reloading", false);
        isReloading = false;
    }

    //This function creates the bullet behavior
    void Shoot()
    {
        curAmmo -= 1;
        GameObject tempFlash;
        tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

        //Destroy the muzzle flash effecta
        Destroy(tempFlash, 2);

        GameObject newBulletPrefab = Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation);
        // Create a bullet and add force on it in direction of the barrel
        newBulletPrefab.GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);
        Destroy(newBulletPrefab, 4);
        RaycastHit hitLocation;
        bool hit = Physics.Raycast(barrelLocation.position, barrelLocation.forward, out hitLocation, 150);
        var tagOfHit = hitLocation.rigidbody.tag;
        if (hit && tagOfHit == "Zombie")
        {
            hitLocation.collider.SendMessageUpwards("killZombie", hitLocation, SendMessageOptions.DontRequireReceiver);
        }
        //Create the muzzle flash
        
    }
}
