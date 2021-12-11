using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cycleThrough : MonoBehaviour
{
    public List<Image> images;

    public int noClick = 0;


    public void CycleForward()
    {
        images[noClick].gameObject.SetActive(false);

        noClick++;

        if (noClick >= images.Count)
        {
            noClick = 0;
        }
        images[noClick].gameObject.SetActive(true);
    }

    public void CycleBackwards()
    {
        images[noClick].gameObject.SetActive(false);

        noClick--;

        if (noClick < 0)
        {
            noClick = images.Count - 1;
        }
        images[noClick].gameObject.SetActive(true);
    }
}
