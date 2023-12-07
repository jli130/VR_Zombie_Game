using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombie : MonoBehaviour
{

    public AudioClip gorwl;
    public AudioClip footsteps;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {
        source.PlayOneShot(footsteps);
        source.PlayOneShot(gorwl);
        Regdoll(true);
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        

    }
    void Run()
    {
        transform.forward = Vector3.ProjectOnPlane((Camera.main.transform.position - transform.position), Vector3.up).normalized;
        transform.position += Time.deltaTime * transform.forward * 5;
    }
    void Regdoll(bool value)
    {
        var bodyParts = GetComponentsInChildren<Rigidbody>();
        foreach (var part in bodyParts) {
        part.isKinematic = value;
                }
    }

    void killZombie(RaycastHit raycastHit)
    {
            GetComponent<Animator>().enabled = false;
            Regdoll(false);
            Vector3 hitpoints = raycastHit.point;
            var colliders = Physics.OverlapSphere(hitpoints, 0.5f);
            foreach (var item in colliders)
            {
                var rigidbody = item.GetComponent<Rigidbody>();
                if (rigidbody)
                {
                    rigidbody.AddExplosionForce(1000, hitpoints, 0.5f);
                }
            }
            this.enabled = false;
            Destroy(this.gameObject, 4);

    }
}
