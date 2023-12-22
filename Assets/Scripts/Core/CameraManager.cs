﻿using UnityEngine;
using System;
using Cinemachine;
using System.Collections.Generic;
using DG.Tweening;

public class CameraManager : MonoBehaviour
{
    private CinemachineBasicMultiChannelPerlin _noise;

    public CinemachineVirtualCamera liveCam = null;

    public List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public CinemachineTargetGroup targetGroup;

    void Awake()
    {

    }

    public void AddCamera(CinemachineVirtualCamera targetCam)
    {
        if (!CheckForDuplicates(targetCam))
            cameras.Add(targetCam);
    }

    public void AddTargetGroup(CinemachineTargetGroup aTargetGroup)
    {
        targetGroup = aTargetGroup;
    }

    public void SetLive(String targetCam)
    {
        // Debug.Log("SET LIVE " + targetCam);
        if (targetCam != null)
        {
            foreach (var cam in cameras)
            {
                if (cam.name == targetCam)
                {
                    cam.gameObject.SetActive(true);
                    //Set active cam to live 
                    liveCam = cam.GetComponent<CinemachineVirtualCamera>();
                    //liveCam.m_Priority = 10;

                    if (liveCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>() != null)
                        _noise = liveCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                }
                else
                {
                    //If not active gamestate - disable camera and canvas
                    cam.gameObject.SetActive(false);
                    //cam.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
                }
            }
        }
    }

    public void SetLive(CinemachineVirtualCamera targetCam)
    {
        if (targetCam != null)
        {
            foreach (var cam in cameras)
            {
                if (cam == targetCam)
                {
                    //Set active cam to live 
                    liveCam = cam.GetComponent<CinemachineVirtualCamera>();
                    liveCam.m_Priority = 10;
                }
                else
                {
                    //If not active gamestate - disable camera and canvas
                    cam.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
                }
            }
        }
    }

    public void EnableNoise(float duration)
    {
        DOTween.To(() => _noise.m_AmplitudeGain, x => _noise.m_AmplitudeGain = x, 1f, duration * 0.5f)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                DOTween.To(() => _noise.m_AmplitudeGain, x => _noise.m_AmplitudeGain = x, 0f, duration * 0.5f)
                    .SetEase(Ease.Linear);
            });
    }

    public void AssignFollowTarget(Transform target)
    {
        //liveCam.m_Follow = target;
        liveCam.Follow = target;
    }

    public void AssignLookAtTarget(Transform target)
    {
        //liveCam.m_LookAt = target;
        liveCam.LookAt = target;
    }

    public void AssignTargetGroup(Transform firstTarget, Transform secondTarget)
    {
        if (targetGroup == null) return;

        targetGroup.m_Targets = new CinemachineTargetGroup.Target[0];

        targetGroup.AddMember(firstTarget, 1f, 0f);
        targetGroup.AddMember(secondTarget, 0.5f, 0f);
    }

    public GameObject[] GrabGameObjectCollection(string tag)
    {
        GameObject[] tmpObjects = GameObject.FindGameObjectsWithTag(tag);
        return tmpObjects;
    }

    public bool CheckForDuplicates(CinemachineVirtualCamera targetCam)
    {
        if (targetCam != null)
        {
            foreach (var cam in cameras)
            {
                if (cam.name == targetCam.name)
                {
                    return true;
                }
            }
        }

        return false;
    }

    #region Unused
    // public Transform fpsCamPivot;
    // public void MovePivot(Transform target)
    // {
    //     StartCoroutine(StartMovePivot(target));
    // }

    // private IEnumerator StartMovePivot(Transform target)
    // {
    //     float elapsed = 0f;
    //     float duration = 1f;
    //     Vector3 startPos = fpsCamPivot.position;

    //     while (elapsed < duration)
    //     {

    //         fpsCamPivot.position = Vector3.Lerp(startPos, target.position + Vector3.up * 0.5f, elapsed / duration);
    //         elapsed += Time.unscaledDeltaTime;
    //         yield return null;
    //     }
    // }
    #endregion
}
