using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class FlyCamController : MonoBehaviour
    {
        private const float _movementVelocity = 1f;

        public float mainSpeed = 65.0f; //regular speed
        public float shiftAdd = 200.0f; //multiplied by how long shift is held.  Basically running
        public float maxShift = 400.0f; //Maximum speed when holdin gshift
        public float camSens = 0.20f; //How sensitive it with mouse
        private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
        private float totalRun= 1.0f;
        private bool flyMode = false;

        void Update () 
        {
            if (flyMode)
            {
                lastMouse = Input.mousePosition - lastMouse ;
                lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
                lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
                transform.eulerAngles = lastMouse;
                lastMouse =  Input.mousePosition;
                //Mouse  camera angle done.  
        
                //Keyboard commands
                float f = 0.0f;
                Vector3 p = GetBaseInput();
                if (Input.GetKey (KeyCode.LeftShift))
                {
                    totalRun += Time.deltaTime;
                    p  = p * totalRun * shiftAdd;
                    p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                    p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                    p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
                }
                else
                {
                    totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                    p = p * mainSpeed;
                }
            
                p = p * Time.deltaTime;
                Vector3 newPosition = transform.position;
                if (Input.GetKey(KeyCode.Space))
                { //If player wants to move on X and Z axis only
                    transform.Translate(p);
                    newPosition.x = transform.position.x;
                    newPosition.z = transform.position.z;
                    transform.position = newPosition;
                }
                else
                {
                    transform.Translate(p);
                }
            }   
            if (Input.GetKeyDown (KeyCode.F))
                flyMode = !flyMode;
        }
        
        private Vector3 GetBaseInput() 
        { //returns the basic values, if it's 0 than it's not active.
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey (KeyCode.Z))
            {
                p_Velocity += new Vector3(0, 0 , _movementVelocity);
            }
            if (Input.GetKey (KeyCode.S))
            {
                p_Velocity += new Vector3(0, 0, -_movementVelocity);
            }
            if (Input.GetKey (KeyCode.Q))
            {
                p_Velocity += new Vector3(-_movementVelocity, 0, 0);
            }
            if (Input.GetKey (KeyCode.D))
            {
                p_Velocity += new Vector3(_movementVelocity, 0, 0);
            }
            return p_Velocity;
        }    
    }
}