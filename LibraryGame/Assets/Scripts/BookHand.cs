using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BookHand : MonoBehaviour
{

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject Book = null;
    private GameObject LookingAtBook = null;

    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItemText_gameObject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode dropItemKey;
    [SerializeField] KeyCode pickItemKey;

    public float playerReach;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.gameObject.transform.rotation = Quaternion.Euler(cam.transform.eulerAngles.x, 0, 0);
        if (Book == null)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, playerReach))
            {
                Rigidbody rb = hitInfo.collider.GetComponent<Rigidbody>();

                if (hitInfo.collider.GetComponent<Book>())
                {
                    if (LookingAtBook == null)
                    {
                        LookingAtBook = hitInfo.collider.gameObject;
                        LookingAtBook.GetComponent<Outline>().enabled = true;
                    }
                    else if (LookingAtBook != hitInfo.collider.gameObject)
                    {
                        LookingAtBook.GetComponent<Outline>().enabled = false;
                        LookingAtBook = hitInfo.collider.gameObject;
                        LookingAtBook.GetComponent<Outline>().enabled = true;

                    }


                    pickUpItemText_gameObject.SetActive(true);
                    if (Input.GetKey(pickItemKey))
                    {
                        Book = hitInfo.collider.gameObject;
                        if (Book.transform.parent != null)
                        {
                            NPC npc = Book.transform.parent.parent.gameObject.GetComponent<NPC>();
                            if (npc)
                            {
                                npc.GiveBook();
                            }
                        }
                        Book.transform.position = Vector3.zero;
                        Book.transform.rotation = Quaternion.identity;
                        Book.transform.SetParent(pickUpParent.transform, false);

                        //ToggleOutline
                        LookingAtBook.GetComponent<Outline>().enabled = false;

                        if (rb != null)
                        {
                            rb.isKinematic = true;
                        }
                    }
                }
                else
                {
                    if (LookingAtBook != null)
                    {
                        LookingAtBook.GetComponent<Outline>().enabled = false;
                        LookingAtBook = null;
                    }
                    pickUpItemText_gameObject.SetActive(false);

                }
            }
            else
            {
                if (LookingAtBook != null)
                {
                    LookingAtBook.GetComponent<Outline>().enabled = false;
                    LookingAtBook = null;
                }

                pickUpItemText_gameObject.SetActive(false);
            }
        }

        else
        {
            pickUpItemText_gameObject.SetActive(false);
            //Item Throw
            if (Input.GetKeyDown(dropItemKey) && Book != null)
            {
                Book.transform.SetParent(null);
                Rigidbody rb = Book.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
                Book = null;
            }
            else if (Input.GetKeyDown(throwItemKey) && Book != null)
            {
                Throw();
            }
        }
    }

    private void Throw()
    {
        if (Book != null)
        {
            Book.transform.SetParent(null);
            Rigidbody rb = Book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            Book book = Book.GetComponent<Book>();

            Vector3 forceToAdd = cam.transform.forward * book.throwForce + transform.up * book.upwardsThrowForce;

            rb.AddForce(forceToAdd, ForceMode.Impulse);

            Book = null;
        }
    }
}
