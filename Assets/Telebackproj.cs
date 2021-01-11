using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telebackproj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        startTime= Time.time;
    }
    float startTime;
    Vector3 goalLocation, goalDiff;
    void SetGoal(Vector3 goal)
    {
        goalLocation = goal;
        goalDiff = goalLocation - this.gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = new Vector3(this.transform.position.x, this.gameObject.transform.position.y, 1);
        Vector3 newGoalDiff = goalLocation - this.gameObject.transform.position;
        if (Time.time - startTime > .1 && Vector3.SqrMagnitude(goalDiff) < Vector3.SqrMagnitude(newGoalDiff))
        {
            Destroy(this.gameObject);
        }
        goalDiff = newGoalDiff;
    }
}
