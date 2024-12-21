using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class CameraManager : I_Manager {
    public Camera mainCamera;
	public override void Initialize()
    {
		mainCamera ??= Camera.main;
        //set camera to default size
        mainCamera.orthographicSize = GameManager.inst.variableManager.cameraDefaultSize;
    }
	public override void ManagerUpdate()
	{
        MoveCamera();
        ChangeSize();
    }
    private void MoveCamera(){
        mainCamera.transform.position += (Vector3)GameManager.inst.netInteractionManager.moveCamera * GameManager.inst.variableManager.cameraMoveSpeed;
    }
    private void ChangeSize(){
        var minSize = .1f;
        var sizeChange = GameManager.inst.variableManager.cameraSizechangeSpeed * GameManager.inst.netInteractionManager.changeCameraSize;
        
        if ((mainCamera.orthographicSize + sizeChange) < minSize) 
            mainCamera.orthographicSize = minSize;
        else
            mainCamera.orthographicSize += sizeChange;
    }
}