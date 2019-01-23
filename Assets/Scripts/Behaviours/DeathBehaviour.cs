﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        var enemy = animator.GetComponent<EnemyController>();
        enemy.player.PlayerLevel.GainXP(enemy.Experience);
        Destroy(enemy.gameObject);
        for (int i = 0; i < Random.Range(2,4); i++)
        {
            CoinController coin = Instantiate(enemy.coin, enemy.transform.position + new Vector3(Random.Range(-0.5f,0.5f),0,0), Quaternion.identity) as CoinController;
        }
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
