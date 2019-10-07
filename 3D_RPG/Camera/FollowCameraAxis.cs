using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//카메라 축
public class FollowCameraAxis : MonoBehaviour
{
    #region INSPECTOR

    public Quaternion targetRotation;
    public Transform cameraVector;
    

    public float m_fRotationSpeed = 5;
    public float m_fZoomSpeed = 1;
    public float m_fDistance = 1;

    Vector3     axisVector;
    Vector3     rotationGap;
    Transform   cameraTrans;
    
    #endregion


    private void Start()
    {
        cameraTrans = Camera.main.transform;
        
    }

    private void Update()
    {
        CameraZoomInOut();
        CameraRotation();
    }

    void CameraZoomInOut()
    {
        m_fDistance += Input.GetAxis("Mouse ScrollWheel") * m_fZoomSpeed * -1;
        m_fDistance = Mathf.Clamp(m_fDistance, 5f, 15f);

        axisVector = transform.forward * -1;
        axisVector *= m_fDistance;
        cameraTrans.position = transform.position + axisVector;
    }

    void CameraRotation()
    {
        if (transform.rotation != targetRotation)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, m_fRotationSpeed * Time.deltaTime);

        if (Input.GetMouseButton(1))
        {
            rotationGap.x += Input.GetAxis("Mouse Y") * m_fRotationSpeed * -1;
            rotationGap.y += Input.GetAxis("Mouse X") * m_fRotationSpeed;

            //카메라 회전 범위
            rotationGap.x = Mathf.Clamp(rotationGap.x, -5f, 85f);
            
            targetRotation = Quaternion.Euler(rotationGap);

            Quaternion quaternion = targetRotation;
            quaternion.x = quaternion.z = 0;
            cameraVector.transform.rotation = quaternion;
        }
    }
}
