using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PowerupDisplay : MonoBehaviour
{
    public Sprite NoneSprite;
    public Sprite BullSprite;
    public Sprite ClearSprite;
    public Sprite BearSprite;

    Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetImage(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.MASSIVE_BULLET:
                image.sprite = BullSprite;
                break;
            case PowerupType.CLEAR_SCREEN:
                image.sprite = ClearSprite;
                break;
            case PowerupType.BEAR_TRAP:
                image.sprite = BearSprite;
                break;
            default:
                image.sprite = NoneSprite;
                break;
        }
    }
}
