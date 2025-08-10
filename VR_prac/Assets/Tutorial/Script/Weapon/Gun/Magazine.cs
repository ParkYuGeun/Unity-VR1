using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magazine : MonoBehaviour, IReloadable
{
    public int maxBullets = 20;
    public float chargingTime = 2f;

    private int currentBullets;
    private int CurrentBullets
    {
        get => currentBullets;
        set
        {
            if(value < 0) 
                currentBullets = 0;
            else if(value > maxBullets) 
                currentBullets = maxBullets;
            else
                currentBullets = value;

            OnBulletsChanged?.Invoke(currentBullets);
            OnChargeChanged?.Invoke((float)currentBullets/maxBullets);
        }
    }

    public UnityEvent OnReloadStart;
    public UnityEvent OnReloadEnd;

    public UnityEvent<int> OnBulletsChanged; //총알 바뀐 개수를 매개변수로 넘김
    public UnityEvent<float> OnChargeChanged; //장전진행률

    private void Start()
    {
        CurrentBullets = maxBullets;
    }

    public bool Use(int amount = 1)
    {
        if(CurrentBullets >= amount)
        {
            CurrentBullets -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void StartReload()
    {
        if (currentBullets == maxBullets)
            return;
        StopAllCoroutines();
        StartCoroutine(ReloadProcess());
    }

    public void StopReload()
    {
        StopAllCoroutines();
    }

    private IEnumerator ReloadProcess()
    {
        OnReloadStart?.Invoke();

        var begintime = Time.time;
        var beginBullets = currentBullets;
        var enoughPercent = 1f - ((float)currentBullets / maxBullets);
        var enoughChargingTime = chargingTime*enoughPercent;

        while(true)
        {
            var t = (Time.time - begintime) / enoughChargingTime;   //몇프로 진행됐는지
            if (t >= 1f)
                break;

            CurrentBullets = (int)Mathf.Lerp(beginBullets,maxBullets,t);
            yield return null;
        }

        CurrentBullets = maxBullets;

        OnReloadEnd?.Invoke();
    }
}
