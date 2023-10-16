using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticField : MonoBehaviour
{
    public float magneticstrength = 5f;
    public float magneticfieldrange = 5f;
    [SerializeField] AudioSource BadCoin;
    [SerializeField] AudioSource GoodCoin;

    private void Start()
    {
        
    }
    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, magneticfieldrange);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("GoodCoin"))
            {
                Vector3 magneticDirection = transform.position - hitCollider.transform.position;
                hitCollider.GetComponent<Rigidbody>().AddForce(magneticDirection.normalized * magneticstrength);
                
               
            }
            else if (hitCollider.CompareTag("BadCoin"))
            {
                Vector3 magneticDirection = transform.position - hitCollider.transform.position;
                hitCollider.GetComponent<Rigidbody>().AddForce(magneticDirection.normalized * magneticstrength);


            }

        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magneticfieldrange);
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GoodCoin"))
        {
            GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>().SumarPunto();
            GoodCoin.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("BadCoin"))
        {
            GameObject.FindGameObjectWithTag("PointManager").GetComponent<PointManager>().RestarPunto();
            BadCoin.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject);
        }
       
    }

}
