using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A classe Player implementa a movimentacao bidimensional de um Rigidbody2D
/// e tambem organiza as animacoes do personagem associado a esse rigidbody.
/// Em resumo, a classe faz a mediacao entre o jogador e um dos niveis do jogo.
/// </summary>

public class Player : MonoBehaviour
{
    // Rigidbody do jogador
    private Rigidbody2D _rigidBody;
    // Animator do jogador
    private Animator _animator;

    [Header("Movement Settings")]
    // Velocidade de caminhada
    public float walkingSpeed;
    // Velocidade de corrida
    public float runningSpeed;
    // Friccao do movimento horizontal
    public float friction;
    // Forca de pulo
    public float jumpingForce;
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

    // Velocidade atual do jogador,
    // pode ser para caminhar ou correr
    private float _currentSpeed;
    
    private Vector3 scaleOnRedo; // = Vector3.one (proporcoes originais)

    // Vetores equivalentes aos anteriores,
    // mas simetricos pelo eixo y, com x < 0
    private Vector3 scaleOnJumpLeft;
    private Vector3 scaleOnFallLeft;
    private Vector3 scaleOnRedoLeft;

    // Sequencia de tweens para pular
    private Sequence _jumpSequence;
    // Sequencia de tweens para pular a esquerda
    private Sequence _jumpLeftSequence;

    // Referencias de tweens que podem ser uteis
    // para atualizar esse script nos proximos exercicios
    private Tween
        _tweenTurnRight, // Virar o personagem a direita
        _tweenJump,
        _tweenFall,
        _tweenRedo;
    private Tween
        _tweenTurnLeft, // Virar o personagem a esquerda
        _tweenJumpLeft,
        _tweenFallLeft,
        _tweenRedoLeft;


    private void Start()
    {
        Init();
    }

    private void Update()
    {
        HandleMovement();
        HandleJumps();

        if (_rigidBody.velocity.y <= 0)
        {
            _animator.SetBool("Jump", false);
        }
    }

    private void Init()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponentInChildren<Animator>();
        
        _currentSpeed = walkingSpeed;

        scaleOnRedo = Vector3.one;
        scaleOnJumpLeft = new Vector3(-scaleOnJump.x, scaleOnJump.y, scaleOnJump.z);
        scaleOnFallLeft = new Vector3(-scaleOnFall.x, scaleOnFall.y, scaleOnFall.z);
        scaleOnRedoLeft = new Vector3(-1, 1, 1);
    }

    // O HandleMovement coordena as velocidades do _rigidBody
    // e as animacoes do personagem, tanto pelo _animator como
    // pelo DOTween.
    private void HandleMovement()
    {
        if (_rigidBody.velocity.x == 0 && _rigidBody.velocity.y != 0)
        {
            // Esse comando pode ser util caso queiramos que o jogador nao
            // se mova horizontalmente depois de um pulo desacompanhado de
            // movimentacao horizontal anterior.
            //_currentSpeed = 0; << Sem efeito no momento >>
        }
        else
        {
            // Quando o LeftControl esta pressionado,
            // o movimento do jogador e a animacao
            // ocorrem com velocidade maior.
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                _animator.SetBool("Run", false);
                _currentSpeed = walkingSpeed;
            }
            else
            {
                _animator.SetBool("Run", true);
                _currentSpeed = runningSpeed;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (gameObject.transform.localScale.x < 0)
            {
                _tweenTurnLeft.Kill();
                _tweenTurnRight = gameObject.transform.DOScaleX((+1), turnDuration);
                
                if (_rigidBody.velocity.y != 0)
                {
                    TweenJump();
                }
            }

            _animator.SetBool("Walk", true);
            _rigidBody.velocity = new Vector2((+1) * _currentSpeed, _rigidBody.velocity.y);

            // O comando logo acima e responsavel por ativar a velocidade do _rigidBody.
            // Os comandos anteriores sao responsaveis pela organizacao das animacoes.
            // O mesmo vale para o bloco seguinte:
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                _tweenTurnRight.Kill();
                _tweenTurnLeft = gameObject.transform.DOScaleX((-1), turnDuration);
                
                if(_rigidBody.velocity.y != 0)
                {
                    TweenJumpLeft();
                }
            }

            _animator.SetBool("Walk", true);
            _rigidBody.velocity = new Vector2((-1) * _currentSpeed, _rigidBody.velocity.y);
        }
        else
        {
            // Se nenhuma das Arrow Keys estiver pressionada,
            // As animacoes de caminhada e corrida ficam desativadas.
            _animator.SetBool("Walk", false);
            _animator.SetBool("Run", false);
        }

        if (_rigidBody.velocity.x != 0 && _rigidBody.velocity.y == 0)
        {
            // Aqui, garantimos que ocorra friccao no movimento
            // caso a velocidade horizontal do _rigidBody seja nula.
            _rigidBody.velocity -= friction * _rigidBody.velocity.normalized;
        }
    }

    // O HandleJumps equivale ao HandleMovement,
    // mas para coordenar os pulos do jogador.
    private void HandleJumps()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (gameObject.transform.localScale.x > 0)
            {
                TweenJump();
            }
            else
            {
                TweenJumpLeft();
            }
            
            _animator.SetBool("Jump", true);
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, jumpingForce);
        }
    }


    // O TweenJump faz o tweening do pulo e da queda
    // usando a sequencia _jumpSequence e os vetores
    // correspondentes.
    private void TweenJump()
    {
        KillJumpSequences();
        _jumpSequence = DOTween.Sequence();

        _jumpSequence
            .Append(_tweenJump = gameObject.transform
            .DOScale(scaleOnJump, jumpScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpSequence
            .Append(_tweenFall = gameObject.transform
            .DOScale(scaleOnFall, fallScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpSequence
            .Append(_tweenRedo = gameObject.transform
            .DOScale(scaleOnRedo, redoScaleTime))
            .SetEase(scalingEase);

        // Esse delay e necessario para evitar
        // conflitos com os tweens que giram
        // o personagem ate a direcao oposta a
        // sua direcao atual.
        _jumpSequence.SetDelay(turnDuration);
    }

    private void TweenJumpLeft() // Equivalente ao TweenJump, mas para pulos a esquerda
    {
        KillJumpSequences();
        _jumpLeftSequence = DOTween.Sequence();

        _jumpLeftSequence
            .Append(_tweenJump = gameObject.transform
            .DOScale(scaleOnJumpLeft, jumpScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpLeftSequence
            .Append(_tweenFall = gameObject.transform
            .DOScale(scaleOnFallLeft, fallScaleTime)
            .SetEase(scalingEase)
            .SetLoops(2, LoopType.Yoyo));

        _jumpLeftSequence
            .Append(_tweenRedo = gameObject.transform
            .DOScale(scaleOnRedoLeft, redoScaleTime))
            .SetEase(scalingEase);

        _jumpLeftSequence.SetDelay(turnDuration);
    }

    // O KillJumpSequences interrompe tanto o TweenJump
    // quanto o TweenJumpLeft. Util caso as animacoes
    // precisem ser reiniciadas
    private void KillJumpSequences()
    {
        _jumpSequence.Kill();
        _jumpLeftSequence.Kill();
    }
}
