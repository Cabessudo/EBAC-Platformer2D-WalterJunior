using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cinemachine;

public class ShakeCamera : Singleton<ShakeCamera>
{
    public CinemachineVirtualCamera cam;
    public float amplitude;
    public float frequency;

    public float timeToShake;
    private float timeShaking;
    private bool shaking;

    [NaughtyAttributes.Button]
    public void Shake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = amplitude;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = frequency;

        timeShaking = timeToShake;
        shaking = true;
    }

    void StopShake()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        timeShaking = 0;
        shaking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(shaking)
        {
            if(timeShaking > 0)
                timeShaking -= Time.deltaTime;
            else
                StopShake();
        }
    }
}
