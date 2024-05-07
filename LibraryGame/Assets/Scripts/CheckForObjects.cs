using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForObjects : MonoBehaviour
{
    private float stepOffset;

    [SerializeField] private CharacterController CC;
    // Start is called before the first frame update
    void Start()
    {
        stepOffset = CC.stepOffset;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            CC.stepOffset = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            CC.stepOffset = stepOffset;
        }
    }
}
