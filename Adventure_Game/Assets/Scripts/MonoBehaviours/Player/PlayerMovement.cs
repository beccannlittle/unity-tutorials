﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;

    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;

    private WaitForSeconds inputHoldWait;
    private Vector3 destinationPosition;
    private Interactable currentInteractable;
    private bool handleInput = true;

    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;

    private readonly int hashSpeedPara = Animator.StringToHash("Speed");
    private readonly int hashLocomotionTag = Animator.StringToHash("Locomotion");

    private void Start()
    {
        agent.updateRotation = false;
        inputHoldWait = new WaitForSeconds(inputHoldDelay);

        destinationPosition = transform.position;
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    private void Update()
    {
        if (agent.pathPending)
        {
            return;
        }

        float speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing(out speed, agent.remainingDistance);
        }
        else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
    }

    private void Stopping(out float speed)
    {
        agent.isStopped = true;
        transform.position = destinationPosition;
        speed = 0;

        if (currentInteractable)
        {
            transform.rotation = currentInteractable.interactionLocation.rotation;
            currentInteractable.Interact();
            currentInteractable = null;
            StartCoroutine(WaitForInteraction());
        }
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.isStopped = true;
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);

        Quaternion targetRotation = currentInteractable ? currentInteractable.interactionLocation.rotation : transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
    }

    private void Moving()
    {
        Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSmoothing * Time.deltaTime);
    }

    public void OnGroundClick(BaseEventData data)
    {
        if (!handleInput)
        {
            return;
        }
        currentInteractable = null;

        PointerEventData pData = (PointerEventData)data;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            destinationPosition = pData.pointerCurrentRaycast.worldPosition;
        }
        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }

    public void OnInteractableClick(Interactable interactable)
    {
        if (!handleInput)
        {
            return;
        }
        currentInteractable = interactable;
        destinationPosition = interactable.interactionLocation.position;
        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }

    private IEnumerator WaitForInteraction()
    {
        handleInput = false;

        yield return inputHoldWait;

        while (animator.GetCurrentAnimatorStateInfo(0).tagHash != hashLocomotionTag)
        {
            yield return null;
        }

        handleInput = true;
    }

}
