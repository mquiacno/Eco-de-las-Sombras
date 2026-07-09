using UnityEngine;

public class Object : MonoBehaviour
{
    private Rigidbody rb;
    private Collider col;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    public void Grab(Transform holdPoint)
    {
     
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            rb.detectCollisions = false; 
        }

     
        if (col != null)
        {
            col.enabled = false;
        }

       
        transform.SetParent(holdPoint);
        transform.position = holdPoint.position; 
        transform.localPosition = Vector3.zero;   
        transform.localRotation = Quaternion.identity;
    }

    public void Drop()
    {
        transform.SetParent(null); 

      
        if (col != null)
        {
            col.enabled = true;
        }

        
        if (rb != null)
        {
            rb.detectCollisions = true;
            rb.isKinematic = false;
        }
    }
}