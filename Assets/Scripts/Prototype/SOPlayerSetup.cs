using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    [Header("Movement")]
    public Vector2 friction = new Vector2(.1f, 0);
    public float flatSpeed;
    public float speedRun;
    public float speed;
    public float jumpForce;

    [Header("Animation")]
    public Animator anim;
    public bool cutScene;
    //Jump Up
    public float jumpDuration = 1;
    public float jumpScaleY = 1.2f;
    public float jumpScaleX = 0.8f;
    public bool readyToJump = true;
    public float jumpCoolDown = 0.1f;
    public Ease easeOut = Ease.OutBack;
    public string triggerJump = "JumpUp";
    //Jump Down
    public string triggerFall = "JumpDown";
    //Jump Landing
    public string triggerLanding = "JumpLanding";
    public float timeToLand = 1;
    //Fall
    public bool isFalling;
    public bool grounded;
    public float fallDuration;
    public float fallX;
    public float fallY;
    public float timeToFalling = 1;
    //Movement
    public float durationToSwipe = .1f;
    public string triggerToWalk = "Walk";
    public string triggerToRun = "Run";
    public bool direction;
    public bool isWalking;
    //Scene Start/Awake
    public string triggerToAwake = "Awake";
    public float cutSceneDuration;
    //Surprised
    public string triggerSurprised = "Surprised";

    [Header("Live & Death")]
    public bool gameOver;
    public string triggerDeath = "Death";
}
