﻿using UnityEngine;
using System.Collections;
using System;


public enum CameraMode { FromBack=0,FromFront,Up }
public class CameraChange : Singleton<CameraChange> {

    public GameObject leanCameraObject;
    public GameObject upCameraObject;
    public GameObject actionUI;
    CameraMove leanCamera;
	// Use this for initialization
	void Start () {

        leanCamera = leanCameraObject.GetComponent<CameraMove>();

        leanCameraObject.SetActive(true);
        upCameraObject.SetActive(false);
    }
	
    public CameraMode nowCameraMode;
    public void ChangeCameraMode()
    {
        nowCameraMode = (CameraMode)(((int)nowCameraMode + 1) % Enum.GetNames(typeof(CameraMode)).Length);
        if (nowCameraMode == CameraMode.FromBack)
        {
            actionUI.SetActive(true);
            leanCameraObject.SetActive(true);
            upCameraObject.SetActive(false);
        }
        if (nowCameraMode == CameraMode.FromBack || nowCameraMode == CameraMode.FromFront)
        {

            ActiveLeanCamera(nowCameraMode);   
        }
        else
        {
            ActiveUpMode();
        }
        
    }
    void ActiveLeanCamera(CameraMode _cameraMode)
    {
        

        leanCamera.ChangeFrontMode(_cameraMode);


        
    }
    void ActiveUpMode()
    {
        leanCameraObject.SetActive(false);
        upCameraObject.SetActive(true);
        actionUI.SetActive(false);
    }
}