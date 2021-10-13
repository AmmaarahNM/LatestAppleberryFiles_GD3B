using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{
    public Image collectionUI;
    public float timePassed;
    // Start is called before the first frame update
    void Start()
    {
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timePassed < 3.94f)
        {
            timePassed += Time.deltaTime;
            collectionUI.fillAmount = timePassed / 3.94f;
        }

        else
        {
            timePassed = 0;
        }
        
        
    }
}
