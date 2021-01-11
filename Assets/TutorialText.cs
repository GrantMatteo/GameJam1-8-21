using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        textbox = GetComponent<Text>();
    }

    int curProgress = 0;
    string[] messages = { "WASD to move", "Left Click to Shoot", "Right Click to Teleport", "Weapons Insufficient. Artifacts: 0/3", "Teleback Unlocked. (Right Click)", "Weapons Insufficient. Artifacts: 1/3", "Charge Shot Unlocked (hold left click)", "Weapons Insufficient. Artifacts: 2/3", "TeleStun Unlocked. Retrieve Core Rune.", "Kill."};
    Text textbox;

    public void pushToMin(int x)
    {
        curProgress = x > curProgress ? x : curProgress;
    }
    // Update is called once per frame
    void Update()
    {
        textbox.text = messages[curProgress];
    }
}
