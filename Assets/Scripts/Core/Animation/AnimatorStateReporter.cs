using UnityEngine;

namespace Core.Animation
{
    public class AnimatorStateReporter : StateMachineBehaviour
    {
        private IAnimationStateReader _stateReader;

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            FindReader(animator);
        }

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateEnter(animator, stateInfo, layerIndex);
            FindReader(animator);
        }
        
        private void FindReader(Animator animator)
        {
            if (_stateReader != null) return;
            
            _stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
        }
    }
}