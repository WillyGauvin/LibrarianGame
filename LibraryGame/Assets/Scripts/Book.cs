using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] BookGenre bookGenre;
    [SerializeField] char BookLetter;
}

public enum BookGenre { Red, Green, Blue, Orange};