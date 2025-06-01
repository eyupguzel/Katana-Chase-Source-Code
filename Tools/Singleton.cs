using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T :MonoBehaviour
{
    private static T instance;
    public static T Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
            Init();
        }
        else
            Destroy(gameObject);
    }
    protected virtual void Init() { }
}
