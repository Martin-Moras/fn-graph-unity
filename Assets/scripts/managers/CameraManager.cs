using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class CameraManager : I_Manager {
    #region Singleton
    public static CameraManager inst { get; private set;}
    public override void SingletonizeThis()
    {
        if (inst != null && inst != this) Destroy(this);
        else inst = this;
    }
    #endregion
    public Camera mainCamera;
	public override void Initialize()
    {
        //set camera to default size
        mainCamera.orthographicSize = VariableManager.inst.cameraDefaultSize;
    }
	public override void ManagerUpdate()
	{
        MoveCamera();
        ChangeSize();
    }
    private void MoveCamera(){
        transform.position += (Vector3)NetInteractionManager.inst.moveCamera * VariableManager.inst.cameraMoveSpeed;
    }
    private void ChangeSize(){
        var minSize = .1f;
        var sizeChange = VariableManager.inst.cameraSizechangeSpeed * NetInteractionManager.inst.changeCameraSize;
        
        if ((mainCamera.orthographicSize + sizeChange) < minSize) 
            mainCamera.orthographicSize = minSize;
        else
            mainCamera.orthographicSize += sizeChange;
    }
}