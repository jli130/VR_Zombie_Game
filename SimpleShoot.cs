using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[AddComponentMenu("Nokobot/Modern Guns/Simple Shoot")]
public class SimpleShoot : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public AudioClip clip;
    public AudioSource source;

    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;
    public GameObject bulletPrefab = null;
    public int maxAmmo = 10;
    public int curAmmo;
    public float reloading = 2.0f;
    public bool isReloading = false;
    public Animator animator;
    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    void Start()
    {
        

        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
        curAmmo = maxAmmo;

    }

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
            gunAnimator.SetTrigger("Fire");
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
        Destroy(tempFlash, destroyTimer);

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

    //This function creates a casing at the ejection slot
    void CasingRelease()
    {
        //Cancels function if ejection slot hasn't been set or there's no casing
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //Create the casing
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //Add force on casing to push it out
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //Add torque to make casing spin in random direction
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //Destroy casing after X seconds
        Destroy(tempCasing, destroyTimer);
    }

}
