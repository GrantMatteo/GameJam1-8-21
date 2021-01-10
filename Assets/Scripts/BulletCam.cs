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
        this.gameObject.SetActive(false);
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bullet == null)
        {
            Deactiv();
            return;
        }
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
     


        if (this.gameObject.activeSelf)
        {
            transform.position = bullet.transform.position + new Vector3(0, 0, -1);
        }
    }
    void SetBullet(GameObject o)
    {
        Debug.Log("setbullet" + this.gameObject.activeSelf);
        bullet = o;
    }

    public GameObject arrowInd;
    public void indicateOffScreen()
    {
        arrowInd.SetActive(true);
        Vector3 pointOnScreen = mainCam.WorldToScreenPoint(bullet.transform.position) - new Vector3(Screen.width, Screen.height, 0)/2;
        float angle = Mathf.Atan2(pointOnScreen.y, pointOnScreen.x);
        
        angle = angle % (Mathf.PI * 2); //normalize
        angle = angle < 0 ? angle + Mathf.PI * 2 : angle;

        float separatorAngle = Mathf.Atan2(Screen.height, Screen.width);
        Vector2 drawBounds = .45F * new Vector2(Screen.width, Screen.height);
        float slope = pointOnScreen.y / pointOnScreen.x;
     
        if (angle >= separatorAngle && angle <= Mathf.PI - separatorAngle ) //top side
        {
            arrowInd.transform.position = mainCam.ScreenToWorldPoint(new Vector3(drawBounds.y / slope, drawBounds.y, 1) + new Vector3(Screen.width, Screen.height, 0) / 2);    
        } 
        else if (angle >= Mathf.PI - separatorAngle && angle <= Mathf.PI + separatorAngle) // left side
        {
            arrowInd.transform.position = mainCam.ScreenToWorldPoint(new Vector3(-drawBounds.x, -drawBounds.x * slope, 1) + new Vector3(Screen.width, Screen.height, 0) / 2);

        }
        else if (angle >= Mathf.PI + separatorAngle && angle <= 2*Mathf.PI - separatorAngle) // bottom side
        {
            arrowInd.transform.position = mainCam.ScreenToWorldPoint(new Vector3(-drawBounds.y / slope, -drawBounds.y, 1) + new Vector3(Screen.width, Screen.height, 0) / 2);
        }
        else //right side
        {
            arrowInd.transform.position = mainCam.ScreenToWorldPoint(new Vector3(drawBounds.x, drawBounds.x * slope, 0) + new Vector3(Screen.width, Screen.height, 0) / 2);
        }
        arrowInd.transform.position = new Vector3(arrowInd.transform.position.x, arrowInd.transform.position.y, 0);
        arrowInd.transform.rotation = Quaternion.Euler(0,0,angle*Mathf.Rad2Deg);
    }
    public void Deactiv()
    {
        arrowInd.SetActive(false);
        this.cam.enabled = false;
    }
}
