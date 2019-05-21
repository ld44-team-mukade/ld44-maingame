#if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.
//////////////////////////////////////////////////////////////////////
//
// Copyright (c) 2014 Audiokinetic Inc. / All Rights Reserved
//
//////////////////////////////////////////////////////////////////////
using UnityEngine;

public enum InteractionType{
    None,
    Enter,
    Exit
}
public class WhObjectInteraction : AkTriggerBase
{
    [Header("Events")]
    public InteractionType interactionType = InteractionType.Enter;
	public bool enableTriggerOnly = false;

    [Header("TargetFilter")]
	public UnityEngine.GameObject triggerObject = null;
    public string targetNameIncluded = "";
    public bool exactMatchName = false;
    public string targetTag = "";

	private void OnCollisionEnter(UnityEngine.Collision in_other)
	{
		if (triggerDelegate == null)return;
		if (enableTriggerOnly)return;
        if (interactionType != InteractionType.Enter)return;
		if (IsTargetObject(in_other.gameObject)){
			triggerDelegate(in_other.gameObject);
        }
	}

	private void OnCollisionExit(UnityEngine.Collision in_other)
	{
		if (triggerDelegate == null)return;
		if (enableTriggerOnly)return;
        if (interactionType != InteractionType.Exit)return;
		if (IsTargetObject(in_other.gameObject)){
			triggerDelegate(in_other.gameObject);
        }
	}

	private void OnTriggerEnter(UnityEngine.Collider in_other)
	{
		if (triggerDelegate == null)return;
        if (interactionType != InteractionType.Enter)return;
		if (IsTargetObject(in_other.gameObject)){
			triggerDelegate(in_other.gameObject);
        }
	}

	private void OnTriggerExit(UnityEngine.Collider in_other)
	{
		if (triggerDelegate == null)return;
        if (interactionType != InteractionType.Exit)return;
		if (IsTargetObject(in_other.gameObject)){
			triggerDelegate(in_other.gameObject);
        }
	}

    private bool IsTargetObject(UnityEngine.GameObject other){
        if(triggerObject == other)return true;
        if(exactMatchName){
            if(other.name == targetNameIncluded) return true;
        }else{
            if(targetNameIncluded != "" && other.name.Contains(targetNameIncluded)) return true;
        }
        if(other.tag == targetTag) return true;
        return false;
    }
}

#endif // #if ! (UNITY_DASHBOARD_WIDGET || UNITY_WEBPLAYER || UNITY_WII || UNITY_WIIU || UNITY_NACL || UNITY_FLASH || UNITY_BLACKBERRY) // Disable under unsupported platforms.