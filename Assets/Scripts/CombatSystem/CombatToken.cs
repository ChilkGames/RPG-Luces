using HutongGames.PlayMaker.Actions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatToken : MonoBehaviour
{
    private Image _tokenImage;
    private const string MOVING_ANIMATION = "moving";
    private const string FADE_ANIMATION = "fading";
    private bool _animating;
    private Vector3 _newPosition;
    private string _animationType;

    public float animationSpeed;
    public float positionDelta;

    public CombatToken(Image image, Vector3 position)
    {
        _tokenImage = image;
        Spawn(position);
    }

    private void Spawn(Vector3 position)
    {
        transform.position = position;
    }

    private void Update()
    {
        if (_animating && _animationType == MOVING_ANIMATION)
            _animating = !MoveToPosition(_newPosition);

    }

    public void ChangeImage(Image newImage)
    {
        _tokenImage = newImage;
    }

    public bool MoveToPosition(Vector3 newPosition)
    {
        if (!_animating)
        {
            _animating = true;
            _newPosition = newPosition;
            _animationType = MOVING_ANIMATION;
        }

        return _newPosition != (transform.position = Vector3.MoveTowards(transform.position, _newPosition, animationSpeed * Time.deltaTime));
    }

    public void FadeAnimation()
    {
        _animating = true;
        _animationType = FADE_ANIMATION;
    }

    public static void InitCallback(CombatToken token)
    {
        token.MoveToPosition(Vector3.zero);
        token.gameObject.SetActive(true);
    }

    public static void DisposeCallback(CombatToken token)
    {
        token.gameObject.SetActive(false);
    }
}
