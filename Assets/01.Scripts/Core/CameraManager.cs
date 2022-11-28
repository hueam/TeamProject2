using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager
{
    CinemachineVirtualCamera cmCam;
    public static CameraManager Instance;
    public CameraManager(GameObject cam){
        cmCam = cam.GetComponent<CinemachineVirtualCamera>();
    }
    public IEnumerator Shake(){
        cmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 5f;
        yield return new WaitForSeconds(1f);
        cmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
    }
}