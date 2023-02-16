using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//goes on main camera and uses TopRightBarrier and BottomLeftBarrier Objects
public class CreateBarrier : MonoBehaviour
{
    private GameObject objTopRightBarrier;
    private GameObject objBottomLeftBarrier;

    public float xMinValue, xMaxValue;

    // Start is called before the first frame update
    void Start()
    {
        objTopRightBarrier = GameObject.Find("/Barrier/TopRightBarrier");
        objBottomLeftBarrier = GameObject.Find("/Barrier/BottomLeftBarrier");
        SetUpBoundarier();
    }

    public void SetUpBoundarier()
    {
        if (objBottomLeftBarrier != null && objTopRightBarrier != null)
        {
            Debug.Log("barrier activate");
            Vector3 pointMin = new Vector3(xMinValue, objTopRightBarrier.transform.position.y, objTopRightBarrier.transform.position.z);
            Vector3 pointMax = new Vector3(xMaxValue, objBottomLeftBarrier.transform.position.y, objBottomLeftBarrier.transform.position.z);

            //place topright
            //point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));
            objTopRightBarrier.transform.position = pointMin;

            //place bottomleft
            //point = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            objBottomLeftBarrier.transform.position = pointMax;
        }
    }
}
