using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ParticleSystem dustParticle;

    internal void CreateDustParticle()
    {
        dustParticle.Play();
    }
}