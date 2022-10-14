using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Trampoline : MonoBehaviour
{
    public Rigidbody ballRigidBody;

    public AudioClip bounceSound;
    public AudioSource audioSource;
    
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit the trampoline");
        if (collision.gameObject.CompareTag("ball"))
        {
            ballRigidBody.AddForce((5 * Vector3.up) + (2 * Vector3.forward), ForceMode.Impulse);
            audioSource.PlayOneShot(bounceSound);
        }
    }
}