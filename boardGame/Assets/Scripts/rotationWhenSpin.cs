using UnityEngine;

public class rotationWhenSpin : MonoBehaviour
{

    public Rigidbody rb;

    
    public void spin ()
    {
        rb.AddTorque(0, Random.Range(20, 300), 0);
    }
}
