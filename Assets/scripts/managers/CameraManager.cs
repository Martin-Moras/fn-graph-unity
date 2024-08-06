using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class CameraManager : MonoBehaviour {
    #region Singleton
    public static CameraManager inst { get; private set;}
    void SingletonizeThis()
    {
        if (inst != null && inst != this) Destroy(this);
        else inst = this;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
		inst = FindObjectOfType<CameraManager>();
	}
    #endregion
    public Camera mainCamera;
    void Awake()
    {
        SingletonizeThis();
    }
    private void Start() {
        mainCamera = GetComponent<Camera>();
        //set camera to default size
        mainCamera.orthographicSize = VariableManager.inst.cameraDefaultSize;
    
    }
    private void LateUpdate(){
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