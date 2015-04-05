using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.PuzzleEd.Scripts.Regular.General;
using UnityEngine;

namespace Assets.PuzzleEd.Scripts.Regular.Entities
{
    public abstract class Piece : ESMonoBehaviour
    {
        public bool IsPlaced;

        public ParticleSystem ParticleTrail { get; set; }

        void Awake()
        {
            ParticleTrail = GetComponent<ParticleSystem>();
        }
    }
}
