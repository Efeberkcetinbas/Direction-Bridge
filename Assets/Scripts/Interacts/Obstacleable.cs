using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacleable : MonoBehaviour
{
    float st = 0;
    internal float interval = 3;
    internal bool canStay=true;
    internal bool canInteract = true;
    internal bool canDamageToPlayer=true;
    internal string interactionTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            StartInteractWithPlayer(other.GetComponent<Player>());
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (!canInteract) return;
        if (other.tag == interactionTag)
        {
            InteractWithPlayer(other.GetComponent<Player>());
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == interactionTag)
        {
            StopInteractWithPlayer(other.GetComponent<Player>());
        }
    }

    void StartInteractWithPlayer(Player player)
    {
        DoAction(player);
    }

    void StopInteractWithPlayer(Player player)
    {
        StopAction(player);
    }

    void InteractWithPlayer(Player player)
    {
        st += Time.deltaTime;
        if (st > interval && canStay)
        {
            ResetProgress();
            DoAction(player);
        }
    }
    internal virtual void ResetProgress()
    {
        st = 0;
    }
    
    internal virtual void DoAction(Player player)
    {
        throw new System.NotImplementedException();
    }

    internal virtual void StopAction(Player player)
    {
        st = 0;
    }
    internal void StopInteract()
    {
        canInteract = false;
    }
    internal void StartInteract()
    {
        canInteract = true;
    }
}
