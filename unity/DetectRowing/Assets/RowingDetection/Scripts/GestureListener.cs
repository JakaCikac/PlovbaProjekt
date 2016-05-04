using UnityEngine;
using System.Collections;
using System;


public class GestureListener : MonoBehaviour, KinectGestures.GestureListenerInterface
{
	// GUI Text to display the gesture messages.
	public GUIText GestureInfo;
	
	private bool raiseLeft;
	private bool raiseRight;
    private bool leftPull;
    private bool rightPull;
    private bool leftPush;
    private bool rightPush;
    //private bool push;
    // private bool pull;
    private bool wave;

	public bool IsRaiseLeftHand()
	{
		if(raiseLeft)
		{
            raiseLeft = false;
			return true;
		}
		
		return false;
	}
	
	public bool IsRaiseRightHand()
	{
		if(raiseRight)
		{
            raiseRight = false;
			return true;
		}
		
		return false;
	}

    /* public bool IsPush()
    {
        if (push)
        {
            push = false;
            return true;
        }

        return false;
    }

    public bool IsPull()
    {
        if (pull)
        {
            pull = false;
            return true;
        }

        return false;
    } */

    public bool IsLeftPush()
    {
        if (leftPush)
        {
            leftPush = false;
            return true;
        }

        return false;
    }

    public bool IsRightPush()
    {
        if (rightPush)
        {
            rightPush = false;
            return true;
        }

        return false;
    }

    public bool IsLeftPull()
    {
        if (leftPull)
        {
            leftPull = false;
            return true;
        }

        return false;
    }

    public bool IsRightPull()
    {
        if (rightPull)
        {
            rightPull = false;
            return true;
        }

        return false;
    }

    public bool IsWave()
    {
        if (wave)
        {
            wave = false;
            return true;
        }

        return false;
    }


    public void UserDetected(uint userId, int userIndex)
	{
		// detect these user specific gestures
		KinectManager manager = KinectManager.Instance;
		
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseLeftHand);
		manager.DetectGesture(userId, KinectGestures.Gestures.RaiseRightHand);
        manager.DetectGesture(userId, KinectGestures.Gestures.LeftPush);
        manager.DetectGesture(userId, KinectGestures.Gestures.RightPush);
        manager.DetectGesture(userId, KinectGestures.Gestures.RightPull);
        manager.DetectGesture(userId, KinectGestures.Gestures.LeftPull);
        manager.DetectGesture(userId, KinectGestures.Gestures.Wave);

        if (GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = "Raise one of your hands to begin rowing! Wave to reset.";
		}
	}
	
	public void UserLost(uint userId, int userIndex)
	{ 
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = string.Empty;
		}
	}

	public void GestureInProgress(uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              float progress, KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
        // don/t do anything here ... 
    }

	public bool GestureCompleted (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint, Vector3 screenPos)
	{
		string sGestureText = gesture + " completed";
		if(GestureInfo != null)
		{
			GestureInfo.GetComponent<GUIText>().text = sGestureText;
		}

        if (gesture == KinectGestures.Gestures.RaiseLeftHand)
            raiseLeft = true;
        else if (gesture == KinectGestures.Gestures.RaiseRightHand)
            raiseRight = true;
        else if (gesture == KinectGestures.Gestures.LeftPush)
            leftPush = true;
        else if (gesture == KinectGestures.Gestures.RightPush)
            rightPush = true;
        else if (gesture == KinectGestures.Gestures.LeftPull)
            leftPull = true;
        else if (gesture == KinectGestures.Gestures.RightPull)
            rightPull = true;
        else if (gesture == KinectGestures.Gestures.Wave)
            wave = true;

        return true;
	}

	public bool GestureCancelled (uint userId, int userIndex, KinectGestures.Gestures gesture, 
	                              KinectWrapper.NuiSkeletonPositionIndex joint)
	{
		return true;
	}
	
}
