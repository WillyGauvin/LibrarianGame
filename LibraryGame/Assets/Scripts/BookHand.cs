using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookHand : MonoBehaviour
{

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject Book = null;

    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItemText_gameObject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode pickItemKey;

    public float playerReach;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Book == null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, playerReach))
            {
                Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();

                if (hitInfo.collider.GetComponent<Book>())
                {
                    pickUpItemText_gameObject.SetActive(true);
                    if (Input.GetKey(pickItemKey))
                    {
                        Book = hitInfo.collider.gameObject;
                        Book.transform.position = Vector3.zero;
                        Book.transform.rotation = Quaternion.identity;
                        Book.transform.SetParent(pickUpParent.transform, false);
                        if (rb != null)
                        {
                            rb.isKinematic = true;
                        }
                    }
                }
                else
                {
                    pickUpItemText_gameObject.SetActive(false);

                }
            }
            else
            {
                pickUpItemText_gameObject.SetActive(false);
            }
        }

        else
        {
            pickUpItemText_gameObject.SetActive(false);
            //Item Throw
            if (Input.GetKeyDown(throwItemKey) && Book != null)
            {
                Book.transform.SetParent(null);
                Rigidbody rb = Book.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                Book = null;
            }
        }
    }
}
