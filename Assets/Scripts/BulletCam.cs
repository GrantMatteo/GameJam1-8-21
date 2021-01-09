using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BulletCam : MonoBehaviour
{
    public Camera mainCam;
    Camera cam;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pointOnScreen = mainCam.WorldToScreenPoint(bullet.transform.position);
        if ((pointOnScreen.x < 0) || (pointOnScreen.x > Screen.width) ||
        (pointOnScreen.y < 0) || (pointOnScreen.y > Screen.height))
        {
            indicateOffScreen();
            this.cam.enabled = true;
        } else
        {
            arrowInd.SetActive(false);
            this.cam.enabled = false;
        }
     


        if (this.gameObject.active)
        {
            transform.position = bullet.transform.position + new Vector3(0, 0, -1);
        }
    }
    void SetBullet(GameObject o)
    {
        bullet = o;
    }

    public GameObject arrowInd;
    public void indicateOffScreen()
    {
        arrowInd.SetActive(true);
        Vector3 pointOnScreen = mainCam.WorldToScreenPoint(bullet.transform.position);
        float angle = Mathf.Atan2(pointOnScreen.y, pointOnScreen.x);
        Debug.Log("");
        angle = ((angle % (Mathf.PI * 2)) + (Mathf.PI * 2)) % (Mathf.PI * 2); //normalize
        float separatorAngle = Mathf.Atan2(Screen.height, Screen.width);
        Vector2 drawBounds = .9F * new Vector2(Screen.width, Screen.height);
        float slope = pointOnScreen.y / pointOnScreen.x;
        if (angle >= separatorAngle && angle <= Mathf.PI - separatorAngle ) //top side
        {
            arrowInd.transform.position = new Vector2(drawBounds.y / slope, drawBounds.y);    
        } 
        else if (angle >= Mathf.PI - separatorAngle && angle <= Mathf.PI + separatorAngle) // left side
        {
            arrowInd.transform.position = new Vector2(-drawBounds.x, -drawBounds.x * slope);

        }
        else if (angle >= Mathf.PI + separatorAngle && angle <= 2*Mathf.PI - separatorAngle) // bottom side
        {
            arrowInd.transform.position = new Vector2(-drawBounds.y / slope, -drawBounds.y);
        }
        else //right side
        {
            arrowInd.transform.position = new Vector2(drawBounds.x, drawBounds.x * slope);

        }
    }
    void Deactiv()
    {
        this.cam.enabled = false;
    }
}
