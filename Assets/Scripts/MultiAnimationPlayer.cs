using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class MultiAnimationPlayer : MonoBehaviour {

    public string enterAnimation;
    public string exitAnimation;

    public Animator[] animators;

    int numberOfAnimators;
    void Start()
    {
        numberOfAnimators = animators.Length;
    }

	public void EnterAnimation()
    {
        StopAllCoroutines();
        foreach(Animator animator in animators)
        {
            if (!animator.gameObject.activeInHierarchy)
                animator.gameObject.SetActive(true);
            animator.Play(enterAnimation);
        }
    }

    public void ExitAnimation()
    {
        StopAllCoroutines();
        foreach (Animator animator in animators)
        {
            if (!animator.gameObject.activeInHierarchy)
                animator.gameObject.SetActive(true);
            animator.Play(enterAnimation);
            int animHash = Animator.StringToHash(exitAnimation);
            animator.Play(animHash);
            StartCoroutine(WaitForAnimation(
                animator,
                animHash,
                new Action[] { }
                )
            );
        }
    }

    IEnumerator WaitForAnimation(Animator animator, int animHash, Action[] actions)
    {
        yield return new WaitForSeconds(0f);
        while (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == animHash)
        {
            yield return null;
        }
        foreach (Action action in actions)
        {
            Debug.Log("executing end action");
            action();
        }
    }
}
