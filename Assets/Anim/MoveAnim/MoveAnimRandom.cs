using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimRandom : StateMachineBehaviour
{
    int hashRandom = Animator.StringToHash("Random");

    public int range = 4;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(hashRandom, Random.Range(0, range) + 1);
    }
}
