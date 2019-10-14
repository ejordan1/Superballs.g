using UnityEngine;
using System.Collections;
 
[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class DragMouseOrbit : MonoBehaviour {

    //public float cameraZoomToCenterSpd = .0000001f;

    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
 
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

 
    float x = 0.0f;
    float y = 0.0f;


    
    // Use this for initialization
    void Start ()  
    {
           
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }
 
    void LateUpdate () 
    {
        if (Input.GetKey(KeyCode.Alpha3)) //that equation doesn't work yet
            x -= (0.005f / (1/Time.deltaTime));//probably includea  square function here
        
        if (Input.GetKey(KeyCode.Alpha4))
            x += (0.005f / (1/Time.deltaTime));//probably includea  square function here

        scrollZoom();
    }
 
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void updateMouseOrbit(float distance)
    {
        if (Input.GetMouseButton(1) && Input.GetKey(KeyCode.LeftShift))
        {
            
           /* Vector3 targetPos = Vector3.zero;
            if (target)
            {
                targetPos = target.transform.position;
                distance = Vector3.Distance(camObj.transform.position, target.transform.position);
            }
            else
            {
                distance = Vector3.Distance(camObj.transform.position, Vector3.zero);
            }
            */
            
            x += Input.GetAxis("Mouse X") * xSpeed * (distance / 10) * 0.02f;//probably includea  square function here
            y -= Input.GetAxis("Mouse Y") * ySpeed * (distance / 50) * 0.02f;

            //  y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);


            Vector3 position;
            position = rotation * new Vector3(0.0f, 0.0f, -distance);

            transform.LookAt(Vector3.zero);
            transform.position = position;
        }
    }
   
    public void rotateLeft(float distance)
    {
            
        x -=  0.2f;//probably includea  square function here
          

        //  y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);


        Vector3 position;
        position = rotation * new Vector3(0.0f, 0.0f, -distance);

        transform.LookAt(Vector3.zero);
        transform.position = position;
        
    }

    public void rotateRight(float distance)
    {

        x += 0.2f;//probably includea  square function here


        //  y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);

        Vector3 position;
        position = rotation * new Vector3(0.0f, 0.0f, -distance);

        transform.LookAt(Vector3.zero);
        transform.position = position;

    }
    public void scrollZoom()
    {

        var d = Input.GetAxis("Mouse ScrollWheel");
        Vector3 zoomTarg = Vector3.zero;
        float distToTarg;
        distToTarg = Vector3.Distance(transform.position, zoomTarg);

        if (d > 0f && distToTarg > 1)
        {
            transform.position = transform.position * (1 - .1f);
           // cameraManager.zoom(.99f);
        }
        else if (d < 0f && distToTarg < 15000)
        {
            transform.position = transform.position * (1 + .1f);
           // cameraManager.zoom(1.01f);
        }
    }
}

//make it so you can follow an object