using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    public Rigidbody rb;

    public float torque = 20f;

    public float force = 5f;

    public GameObject knifeParticleSystem;

    private Vector2 startSwipe = new Vector2(0.44f, 0.24f);
    private Vector2 endSwipe = new Vector2(0.50f, 0.75f);

    public AudioSource swing;
    public AudioSource metalHit;
    public AudioSource woodHit;

    public AudioSource sliceHit;
    // Start is called before the first frame update
    void Start()
    {

        ParticleSystemInactivate();
        //swing.Play();

    }

    // Update is called once per frame
    void Update()
    {

        //if (!rb.isKinematic)
        //return;
        /*
        if (Input.GetMouseButtonDown(0))
		{
			startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Invoke("ParticleSystemInactivate", 1f);
		}

        if (Input.GetMouseButtonUp(0))
		{
			endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Swipe();
            ParticleSystemActivate();
            
            
            
		}
        */
        /*
        else
            ParticleSystemInactivate();
        */

        
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Swipe();
            swing.Play();
            //Invoke("ParticleSystemActivate", 5f);
            ParticleSystemActivate();
            //ParticleSystemActivate();

        }
        
    }

    void Swipe ()
	{
        
        
        rb.isKinematic = false;

		Vector2 swipe = endSwipe - startSwipe;
        Debug.Log("end swipe:" + endSwipe);
        Debug.Log("start swipe:" + startSwipe);
        Debug.Log("swipe:"+swipe);

		rb.AddForce(swipe * force, ForceMode.Impulse);
        rb.AddTorque(0f, 0f, -torque, ForceMode.Impulse);
		
	}

    void OnTriggerEnter(Collider col)
	{
        ParticleSystemInactivate();
		if (col.tag == "WoodenBlock")
		{
			rb.isKinematic = true;
            woodHit.Play();
        }
        else if(col.gameObject.CompareTag("CanSlice"))
        {
            sliceHit.Play();
            //gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
        }

        else if (col.tag == "Untagged")
        {
            metalHit.Play();
            //gameObject.GetComponent<BoxCollider>().isTrigger = false;
            gameObject.GetComponent<SphereCollider>().isTrigger = false;
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = false;
        }
            

    }

    void ParticleSystemInactivate()
    {
        knifeParticleSystem.SetActive(false);
    }

    void ParticleSystemActivate()
    {
        knifeParticleSystem.SetActive(true);
    }

}
