using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Progression : MonoBehaviour
{
    public GameObject objectiveObj; // potomky tohoto objektu budou objekty potřebné ke splnění objektivu
    public GameObject progressBarrier;

    public int eliminationCount;
    public int targetCount;

    // Update is called once per frame
    void LateUpdate()
    {
        CheckObjective();
    }

    void CheckObjective()
    {
        if (objectiveObj.transform.childCount == 0)
        {
            Destroy(progressBarrier);
        }
    }
}
