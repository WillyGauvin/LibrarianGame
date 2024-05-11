using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{

    [SerializeField] GameObject popUp_prefab;
    [Space(10)]
    [Header("PopUpImages")]
    [SerializeField] Sprite RedSprite;
    [SerializeField] Sprite YellowSprite;
    [SerializeField] Sprite TealSprite;
    [SerializeField] Sprite BlueSprite;
    [SerializeField] Sprite DefaultSprite;

    [Space(10)]
    [Header("Letters")]
    [SerializeField] Sprite[] Letters;
    [Space(10)]


    GameObject popUp;

    public GameManager gameManager;

    public NPCType CitizenType;

    public Book NPCBook;

    public Book BookRequest;

    public Transform pickUpParent;

    bool hasLeftLibrary = false;

    public bool isFrontOfLine = false;

    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hasLeftLibrary)
        {
            if (CitizenType == NPCType.CheckOut && NPCBook != null)
            {
                LeaveWithBook();
            }
            Destroy(gameObject);
        }
        if (isFrontOfLine && popUp == null)
        {
            Vector3 worldPos = gameObject.transform.position + new Vector3(0, 1.75f, 0);
            popUp = Instantiate(popUp_prefab, worldPos, new Quaternion());

            BookLetter letter = BookRequest.bookLetter;
            Sprite letterSprite = Letters[(int)letter];

            Sprite sprite;
            BookGenre Genre = BookRequest.bookGenre;

            switch (Genre)
            {
                case BookGenre.Red:
                    sprite = RedSprite;
                    break;
                case BookGenre.Yellow:
                    sprite = YellowSprite;
                    break;
                case BookGenre.Teal:
                    sprite = TealSprite;
                    break;
                case BookGenre.Blue:
                    sprite = BlueSprite;
                    break;
                default:
                    sprite = DefaultSprite;
                    break;
                    
            }
            popUp.GetComponent<PopUp>().GenreImage_value = sprite;
            popUp.GetComponent<PopUp>().LetterImage_value = letterSprite;

        }
        if (!isFrontOfLine && popUp != null)
        {
            Destroy(popUp);
        }

    }

    public void LeftLibrary()
    {
        hasLeftLibrary = true;
    }

    public void PickupBook(Book book)
    {
        book.gameObject.SetActive(true);

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
        NPCBook.transform.SetParent(null);
        NPCBook.GetComponent<Rigidbody>().isKinematic = false;

        gameManager.CheckBookIn(NPCBook);

        //Code to give the player the book.

        NPCBook = null;
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

    private void LeaveWithBook()
    {
        NPCBook.transform.SetParent(null);
        NPCBook.GetComponent<Rigidbody>().isKinematic = false;

        gameManager.CheckBookOut(NPCBook);

        NPCBook.gameObject.SetActive(false);
    }
}

public enum NPCType { CheckIn, CheckOut };
