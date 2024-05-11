using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Image LetterImage;
    [SerializeField] Image GenreImage;
    public Sprite LetterImage_value;
    public Sprite GenreImage_value;
    // Start is called before the first frame update
    void Start()
    {
        LetterImage.sprite = LetterImage_value;
        GenreImage.sprite = GenreImage_value;
    }
}
