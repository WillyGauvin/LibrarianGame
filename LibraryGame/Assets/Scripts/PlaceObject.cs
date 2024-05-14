using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    [SerializeField] GameObject ghost;
    
    public GameObject placed;

    [SerializeField] float placementReach;

    [SerializeField] Camera cam;

    [SerializeField] KeyCode placeItemKey;
    [SerializeField] KeyCode rotateClockWiseKey;
    [SerializeField] KeyCode rotateCounterKey;

    [Space(20)]
    [SerializeField] float rotationSpeed;

    BookHand bookHand;

    Quaternion DefaultRotaion = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));


    // Start is called before the first frame update
    void Start()
    {
        ghost.SetActive(false);
        bookHand = gameObject.GetComponent<BookHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bookHand.isPlacing)
        {
            placed = bookHand.Book;

            if (placed != null)
            {
                ghost.SetActive(true);

                RaycastHit hit;

                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, placementReach))
                {
                    ghost.SetActive(true);

                    ghost.transform.position = hit.point;

                    if (Input.GetKey(rotateClockWiseKey))
                    {
                        Vector3 rotation = new Vector3(0, rotationSpeed * Time.deltaTime, 0);
                        ghost.transform.Rotate(rotation, Space.World);

                    }
                    else if (Input.GetKey(rotateCounterKey))
                    {
                        Vector3 rotation = new Vector3(0, -rotationSpeed * Time.deltaTime, 0);
                        ghost.transform.Rotate(rotation, Space.World);
                    }
                    if (Input.GetKey(placeItemKey))
                    {
                        placed.transform.SetParent(null);
                        Rigidbody rb = placed.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = false;
                        }
                        placed.transform.position = ghost.transform.position;

                        Quaternion rotation = Quaternion.Euler(-90, ghost.transform.rotation.eulerAngles.y, ghost.transform.rotation.eulerAngles.z);

                        placed.transform.rotation = rotation;

                        ghost.transform.localRotation = DefaultRotaion;
                        ghost.SetActive(false);

                        bookHand.Book = null;
                        bookHand.isPlacing = false;
                    }
                }
                else
                {
                    ghost.transform.localRotation = DefaultRotaion;
                    ghost.SetActive(false);
                }
            }
        }
        else
        {
            ghost.transform.localRotation = DefaultRotaion;
            ghost.SetActive(false);
        }
    }

    public void ForceDisable()
    {
        ghost.SetActive(false);
    }
}
