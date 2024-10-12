using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Core.Gameplay.Enemies
{
    public class EnemyGfx : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        
        public void PlayDeath() => animator.SetTrigger(Die);
        public void PlayHit() => animator.SetTrigger(Hit);

        public void Move(float speed)
        {
            animator.SetBool(IsMoving, true);
            animator.SetFloat(Speed, speed);
        }

        public void StopMoving() => animator.SetBool(IsMoving, false);

        public void PlayAttack() => animator.SetTrigger(Attack);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));
        
        private AnimatorState StateFor(int stateHash)
        {
            return null;//TODO Не дописано + добавить скрипты на стейты в аниметоре юнити
        }
    }
}