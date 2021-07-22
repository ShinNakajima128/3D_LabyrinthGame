using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] float m_minAngle_X = -10.0f;
    [SerializeField] float m_maxAngle_X = 10.0f;
    [SerializeField] float m_minAngle_Z = -10.0f;
    [SerializeField] float m_maxAngle_Z = 10.0f;

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if(transform.eulerAngles.x <= m_maxAngle_X) 
            {
                transform.Rotate(0.1f, 0, 0);
            } 
            
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (transform.eulerAngles.x >= m_minAngle_X)
            {
                transform.Rotate(-0.1f, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (transform.eulerAngles.z >= m_minAngle_Z)
            {
                transform.Rotate(0, 0, -0.1f);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (transform.eulerAngles.z <= m_maxAngle_Z)
            {
                transform.Rotate(0, 0, 0.1f);
            }
        }
    }
}
