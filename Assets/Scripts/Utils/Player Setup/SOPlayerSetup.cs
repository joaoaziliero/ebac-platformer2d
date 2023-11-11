using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace PlayerSetup
{
    [CreateAssetMenu]
    public class SOPlayerSetup : ScriptableObject
    {
        [Header("Movement Settings")]
        // Velocidade de caminhada
        public float walkingSpeed;
        // Velocidade de corrida
        public float runningSpeed;
        // Friccao do movimento horizontal
        public float friction;
        // Forca de pulo
        public float jumpingForce;
        // Numero maximo de pulos antes de cair
        public int maxJumps;
        // Duracao do giro do personagem,
        // a direita ou a esquerda
        public float turnDuration;

        [Header("Scaling Settings")]
        // Proporcoes do jogador no pulo
        public Vector3 scaleOnJump;
        // Proporcoes do jogador na queda
        public Vector3 scaleOnFall;

        [Header("Ease Settings")]
        // Ease para fazer o tweening das proporcoes
        public Ease scalingEase;

        [Header("Scaling Durations")]
        // Duracao do tween de pulo
        public float jumpScaleTime;
        // Duracao do tween de queda
        public float fallScaleTime;
        // Duracao do tween que refaz as proporcoes originais
        public float redoScaleTime;
    }
}
