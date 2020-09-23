using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IconToggle : MonoBehaviour
{
    public Sprite iconOn;
    public Sprite iconOff;
    public bool defaultState = true;

    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
		image.sprite = (defaultState) ? iconOn : iconOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleIcon(bool state)
    {
        if(state)
        {
            image.sprite = iconOn;
        }
        else
        {
            image.sprite = iconOff;
        }
    }
}
