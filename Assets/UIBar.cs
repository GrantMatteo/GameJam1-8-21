using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIBar : MonoBehaviour
{
    RectTransform r;
    Image i;
    // Start is called before the first frame update
    void Start()
    {
        i = GetComponent<Image>();
        r = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetSize(float val)
    {
        i.enabled = (val != 0);
        Debug.Log("hello there " + val);
        r.sizeDelta = new Vector2(val * 160, r.sizeDelta.y);
    }
}
