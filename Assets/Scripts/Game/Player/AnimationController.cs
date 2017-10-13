using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour {

    public static Animator playerAnim;

	// Use this for initialization
	void Start () {
        playerAnim = GetComponent<Animator>();
	}
	
    public static void SetAnimation(string _param)
    {
        AnimatorControllerParameter param;
        for (int i = 0; i < playerAnim.parameters.Length; i++)
        {
            param = playerAnim.parameters[i];
            bool isActive;
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                if(param.name == _param)
                {
                    isActive = true;
                }
                else
                {
                    isActive = false;
                }
                playerAnim.SetBool(param.name, isActive);
            }
            else
            {
                Debug.LogWarning("Tried to set non-bool param as a bool");
            }
        }
    }
}
