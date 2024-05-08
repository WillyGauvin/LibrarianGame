using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCType CitizenType;

    public Book NPCBook;

    public Book BookRequest;

    public bool HasLeftLibrary = false;

    public Transform pickUpParent;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HasLeftLibrary)
        {
            Destroy(gameObject);
        }
    }

    public void LeftLibrary()
    {
        HasLeftLibrary = true;
    }

    public void PickupBook(Book book)
    {
        Rigidbody rb = book.GetComponent<Rigidbody>();
        book.transform.position = Vector3.zero;
        book.transform.rotation = Quaternion.identity;
        book.transform.SetParent(pickUpParent.transform, false);

        NPCBook = book;

        if (rb != null)
        {
            rb.isKinematic = true;
        }

    }

    public void GiveBook()
    {
        NPCBook.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Book book = collision.gameObject.GetComponent<Book>();

        if (book != null)
        {
            if (book ==  BookRequest)
            {
                PickupBook(book);
            }
        }
    }
}

public enum NPCType { CheckIn, CheckOut };
