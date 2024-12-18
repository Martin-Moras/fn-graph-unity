using UnityEngine;

public abstract class I_Manager : MonoBehaviour
{
	public abstract void Initialize();
	public abstract void SingletonizeThis();
	public abstract void ManagerUpdate();
}
