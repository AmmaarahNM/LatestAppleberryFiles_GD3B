using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{
    public Image collectionUI;
    float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed < 4)
        {
            timePassed += Time.deltaTime;
        }
        
        collectionUI.fillAmount = timePassed / 4;
    }
}
