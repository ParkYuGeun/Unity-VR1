using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;


public class Bomb : MonoBehaviour
{
    public enum State
    {
        Idle,
        Drop,
    }

    public float explosionRadius;
    public LayerMask explosionHittableMask;

    public float recycleDelay = 1f;

    public UnityEvent OnExplosion;
    public UnityEvent OnRecycle;

    private State state;

    public void Drop()
    {
        state = State.Drop;
    }

    public void Throw()
    {
        var interactable = GetComponent<XRGrabInteractable>();
        interactable.interactionManager.CancelInteractableSelection((IXRSelectInteractable)interactable);

        var rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(0f, 150f, 300f));

        

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state == State.Idle) 
            return;

        Explosion();

    }

/*    private void OnTriggerEnter(Collider other)   ontriggerenter사용하니까 컨트롤러가 다시 닿을때 터짐. oncollision 사용 or outtrigger + 시간후 폭발 사용
    {
        if (state == State.Idle)
            return;

        Explosion();

    }*/

    private void Explosion()
    {
        var overlaps = Physics.OverlapSphere(transform.position, explosionRadius, explosionHittableMask,QueryTriggerInteraction.Collide);
        foreach (var overlap in overlaps)
        {
            var hitObject = overlap.GetComponent<Hittable>();
            hitObject?.Hit();
        }
        OnExplosion?.Invoke();
        Invoke(nameof(Recycle),recycleDelay);
    }
    private void Recycle()
    {
        state = State.Idle;

        OnRecycle?.Invoke();
    }
}
