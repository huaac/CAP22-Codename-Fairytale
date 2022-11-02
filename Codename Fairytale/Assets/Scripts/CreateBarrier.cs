using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//goes on main camera and uses TopRightBarrier and BottomLeftBarrier Objects
public class CreateBarrier : MonoBehaviour
{
    private GameObject objTopRightBarrier;
    private GameObject objBottomLeftBarrier;

    // Start is called before the first frame update
    void Start()
    {
        objTopRightBarrier = GameObject.Find("/Barrier/TopRightBarrier");
        objBottomLeftBarrier = GameObject.Find("/Barrier/BottomLeftBarrier");
        SetUpBoundarier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SetUpBoundarier()
    {
        if (objBottomLeftBarrier != null && objTopRightBarrier != null)
        {
            Vector3 point = new Vector3();

            //place topright
            point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
            objTopRightBarrier.transform.position = point;

            //place bottomleft
            point = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            objBottomLeftBarrier.transform.position = point;
        }
    }
}
