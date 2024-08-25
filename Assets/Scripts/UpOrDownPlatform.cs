using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpOrDownPlatform : MonoBehaviour
{

    public bool up, active = true;
    public float force;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player" && active == true)
        {
            if (up)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * -force, ForceMode.Impulse);
                Debug.Log("WEEEE");
                //active = false;
            }
            if (!up)
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
                //active = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
