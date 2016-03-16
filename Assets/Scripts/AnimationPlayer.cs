using System;
using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour {

    public string enterAnimation;
    public string exitAnimation;

    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}

    public void EnterAnimation()
    {
        StopAllCoroutines();
        if(!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        if(animator == null)
            animator = GetComponent<Animator>();
        gameObject.SetActive(true);
        animator.Play(Animator.StringToHash(enterAnimation));
    }

    public void ExitAnimation()
    {
        StopAllCoroutines();
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        if (animator == null)
            animator = GetComponent<Animator>();
        int animHash = Animator.StringToHash(exitAnimation);
        animator.Play(animHash);
        StartCoroutine(WaitForAnimation(
            animHash,
            new Action[] { () => gameObject.SetActive(false)  }
            )
        );
    }

    public void ExitAnimation(AnimationPlayer animPlayer)
    {
        if (!gameObject.activeInHierarchy)
            gameObject.SetActive(true);
        if (animator == null)
            animator = GetComponent<Animator>();
        int animHash = Animator.StringToHash(exitAnimation);
        animator.Play(animHash);
        StartCoroutine(WaitForAnimation(
            animHash,
            new Action[] { () => gameObject.SetActive(false), () => animPlayer.EnterAnimation() }
            )
        );
    }

    IEnumerator WaitForAnimation(int animHash, Action[] actions)
    {
        yield return new WaitForSeconds(0f);
        while(animator.GetCurrentAnimatorStateInfo(0).shortNameHash == animHash)
        {
            yield return null;
        }
        foreach(Action action in actions)
        {
            Debug.Log("executing end action");
            action();
        }       
    }

    public virtual void PlayAnimation(bool enter)
    {
        if (enter)
            EnterAnimation();
        else
            ExitAnimation();
    }
}
