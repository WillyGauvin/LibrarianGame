using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] BookGenre bookGenre;
    [SerializeField] BookLetter bookLetter;
}

public enum BookGenre { Red, Blue, Yellow, Teal};
public enum BookLetter { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };