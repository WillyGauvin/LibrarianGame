using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image image;
    public string text_value;
    public Sprite image_value;
    // Start is called before the first frame update
    void Start()
    {
        text.text = text_value;
        image.sprite = image_value;
    }
}
