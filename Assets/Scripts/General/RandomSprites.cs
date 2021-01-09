using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprites : Affecter
{
    public RandomRotateSetting rotateSetting;
    public bool randomXFlip, randomYFlip;

    private void Start()
    {
        // if (autoSetup)
            //Setup();
        foreach (var target in GetTargets<SpriteRenderer>())
        {
            ApplyFlip(target.transform);
        }
    }

    void ApplyFlip(Transform target)
    {
        Transform parent = target.parent;
        target.parent = null;
        switch (rotateSetting)
        {
            case RandomRotateSetting.Flip:
                transform.Rotate(0, 0, 180 * (int)Random.Range(0, 1));
                break;
            case RandomRotateSetting.Turn:
                transform.Rotate(0, 0, 90 * (int)Random.Range(0, 4));
                break;
            case RandomRotateSetting.None:
                break;
        }
        target.localScale = new Vector3(
            target.localScale.x * ((randomXFlip) ? ExtensionMethods.RandomSign() : 1),
            target.localScale.y * ((randomYFlip) ? ExtensionMethods.RandomSign() : 1),
            1);
        target.parent = parent;
    }
}
