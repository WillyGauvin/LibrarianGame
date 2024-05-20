using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public class Book : MonoBehaviour
{
    [SerializeField] public BookGenre bookGenre;
    [SerializeField] public BookLetter bookLetter;

    [SerializeField] public float throwForce;
    [SerializeField] public float upwardsThrowForce;

    public void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    public void PickupBook()
    {
        gameObject.layer = LayerMask.NameToLayer("PlayerHand");
    }

    public void DropBook()
    {
        gameObject.layer = LayerMask.NameToLayer("Book");
    }
}

public enum BookGenre { Red, Blue, Yellow, Teal};
public enum BookLetter { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };