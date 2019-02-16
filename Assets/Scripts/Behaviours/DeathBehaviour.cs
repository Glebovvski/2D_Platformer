using System.Collections;
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
        if (enemy == null)
            enemy = animator.GetComponentInParent<EnemyController>();
        PlayerLevelManager.Instance.GainXP(enemy.Experience);
        Destroy(enemy.gameObject);
        int amountOfCoins = Random.Range(2, 4);
        for (int i = 0; i < amountOfCoins; i++)
        {
            int value = 0;
            if (amountOfCoins >= 2 && amountOfCoins <= 3)
                value = Random.Range(20, 25);
            if (amountOfCoins > 3 || amountOfCoins == 4)
                value = Random.Range(10, 12);
            CoinController coin = Instantiate(enemy.coin, enemy.transform.position + new Vector3(Random.Range(-0.5f,0.5f),0,0), Quaternion.identity) as CoinController;
            coin.value = value;
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
