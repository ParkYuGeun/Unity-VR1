using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mob : MonoBehaviour
{
    public float destroyDelay = 1f;

    public UnityEvent onCreated;
    public UnityEvent onDestroyed;

    private bool isDestroy = false;

    private void Start()
    {
        onCreated?.Invoke();
        MobManager.Instance.OnSpawned(this);
    }

    public void Destory()
    {
        if (isDestroy)
            return;
        isDestroy = true;

        Destroy(gameObject,destroyDelay);

        onDestroyed?.Invoke();
        MobManager.Instance.OnDestroyed(this);
    }

}
