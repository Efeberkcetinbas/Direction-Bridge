using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BlockTrigger : Obstacleable
{

    internal override void DoAction(Player player)
    {
        
        transform.DOLocalMoveY(-0.5f,0.2f).OnComplete(()=>Destroy(gameObject));    
        player.CreateDustParticle();
    }

    
    
}
