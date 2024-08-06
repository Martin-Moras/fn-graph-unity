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
    void Awake()
    {
        SingletonizeThis();
    }
    private void LateUpdate(){
        moveCamera();
    }
    private void moveCamera(){
        transform.position += (Vector3)NetInteractionManager.inst.moveCamera * VariableManager.inst.cameraMoveSpeed;
    }
}