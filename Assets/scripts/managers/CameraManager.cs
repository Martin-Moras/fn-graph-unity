using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
public class CameraManager : MonoBehaviour {
    #region Singleton
    public static CameraManager Instance { get; private set;}
    void SingletonizeThis()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void OnRuntimeMethodLoad()
    {
		Instance = FindObjectOfType<CameraManager>();
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
        transform.position += (Vector3)InputManager.Instance.moveCamera;
    }
}