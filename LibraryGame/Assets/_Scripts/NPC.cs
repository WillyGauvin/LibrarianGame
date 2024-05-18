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
    [SerializeField] Sprite BlueBook;
    [SerializeField] Sprite RedBook;
    [SerializeField] Sprite TealBook;
    [SerializeField] Sprite YellowBook;
    [SerializeField] Sprite DefaultBook;

    [Space(10)]
    [Header("Letters")]
    [SerializeField] Sprite[] BlueLetters;
    [SerializeField] Sprite[] OrangeLetters;
    [SerializeField] Sprite[] TealLetters;
    [SerializeField] Sprite[] YellowLetters;
    [SerializeField] Sprite DefaultLetter;

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

            Sprite letterSprite;
            Sprite bookSprite;
            BookGenre Genre = BookRequest.bookGenre;

            switch (Genre)
            {
                case BookGenre.Red:
                    bookSprite = RedBook;
                    letterSprite = TealLetters[(int)letter];
                    break;
                case BookGenre.Yellow:
                    bookSprite = YellowBook;
                    letterSprite = BlueLetters[(int)letter];
                    break;
                case BookGenre.Teal:
                    bookSprite = TealBook;
                    letterSprite = OrangeLetters[(int)letter];
                    break;
                case BookGenre.Blue:
                    bookSprite = BlueBook;
                    letterSprite = YellowLetters[(int)letter];
                    break;
                default:
                    bookSprite = DefaultBook;
                    letterSprite = DefaultLetter;
                    break;
                    
            }

            popUp.GetComponent<PopUp>().GenreImage_value = bookSprite;
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
