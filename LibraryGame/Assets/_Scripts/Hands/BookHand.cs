using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BookHand : MonoBehaviour
{

    [SerializeField] Transform pickUpParent;
    [SerializeField] Transform throwParent;
    [SerializeField] LayerMask raycastLayer;


    [SerializeField]
    public GameObject Book = null;
    private GameObject LookingAtBook = null;

    [SerializeField] Camera cam;

    [SerializeField] GameObject pickUpItemText_gameObject;

    [Space(20)]
    [Header("Keys")]
    [SerializeField] KeyCode throwItemKey;
    [SerializeField] KeyCode dropItemKey;
    [SerializeField] KeyCode pickItemKey;
    [SerializeField] KeyCode placeModeKey;

    public bool isPlacing = false;

    public float playerReach;

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
                    else if (LookingAtBook == hitInfo.collider.gameObject && LookingAtBook.GetComponent<Outline>().isActiveAndEnabled == false)
                    {
                        LookingAtBook.GetComponent<Outline>().enabled = true;
                    }


                    pickUpItemText_gameObject.SetActive(true);
                    if (Input.GetKeyDown(pickItemKey))
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
                        Book.GetComponent<Book>().PickupBook();

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

        //If Player is holding a book.
        else
        {
            pickUpItemText_gameObject.SetActive(false);

            //Item Place
            
            if (Input.GetKeyDown(placeModeKey) && Book != null)
            {
                isPlacing = !isPlacing;
            }

            if (isPlacing == false)
            {
                //Item Drop
                if (Input.GetKeyDown(dropItemKey) && Book != null)
                {
                    Book.transform.SetParent(null);
                    Rigidbody rb = Book.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                    }
                    Book.GetComponent<Book>().DropBook();
                    Book = null;
                }
                //Item Throw
                else if (Input.GetKeyDown(throwItemKey) && Book != null)
                {
                    Throw();
                }
            }
        }
    }

    private void Throw()
    {
        if (Book != null)
        {
            RaycastHit hit;

            Vector3 direction = pickUpParent.position - throwParent.position;

            Ray ray = new Ray(throwParent.position, direction);

            Book.transform.SetParent(null);
            Rigidbody rb = Book.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }

            if (Physics.Raycast(ray, out hit, direction.magnitude, raycastLayer))
            {
                Book.transform.position = throwParent.position;
            }

            Book book = Book.GetComponent<Book>();

            Vector3 forceToAdd = cam.transform.forward * book.throwForce + transform.up * book.upwardsThrowForce;

            rb.AddForce(forceToAdd, ForceMode.Impulse);

            Book.GetComponent<Book>().DropBook();

            Book = null;
        }
    }

    public void SwitchedHands()
    {
        isPlacing = false;
        pickUpItemText_gameObject.SetActive(false);
        gameObject.GetComponent<PlaceObject>().ForceDisable();
        if (LookingAtBook != null)
        {
            LookingAtBook.GetComponent<Outline>().enabled = false;
        }
    }
}
