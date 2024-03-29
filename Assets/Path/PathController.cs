﻿using Game.Components.Timer;
using UnityEngine;

public class PathController : TimerView
{
    const float defaultOffset = 1f;

    [SerializeField] private SpriteRenderer BaseSpriteRenderer;
    [SerializeField] private SpriteRenderer MovementSpriteRenderer;

    float time;
    float offset;

    private void Awake()
    {
        MovementSpriteRenderer.gameObject.SetActive(false);
    }

    public override void SetData(float time)
    {
        this.time = time;
        //MovementSpriteRenderer.transform.localPosition = new Vector3(offset, 0, 0);
        MovementSpriteRenderer.gameObject.SetActive(true);
    }

    public void Lock()
    {
        Color color = BaseSpriteRenderer.color;
        color.a = 0.5f;
        BaseSpriteRenderer.color = color;
    }

    public void Unlock()
    {
        Color color = BaseSpriteRenderer.color;
        color.a = 1;
        BaseSpriteRenderer.color = color;
    }

    public void Prepare(bool leftToRight)
    {
        //offset = leftToRight ? -defaultOffset : defaultOffset;
        offset = leftToRight ? defaultOffset : -defaultOffset;
        MovementSpriteRenderer.transform.localPosition = new Vector3(-0.5f * offset, 0, 0);
    }

    public override void UpdateView(float timePass)
    {
        //MovementSpriteRenderer.transform.localPosition = new Vector3(offset * (1 - (timePass / time)), 0, 0);

        float scalse = timePass / time;
        
        if (scalse > 1)
            scalse = 1;

        MovementSpriteRenderer.transform.localScale = new Vector3(scalse * offset, 1, 1);
    }

    public void SetEndState()
    {
        //MovementSpriteRenderer.transform.localPosition = new Vector3(0, 0, 0);
        MovementSpriteRenderer.transform.localPosition = new Vector3(-0.5f, 0, 0);
        MovementSpriteRenderer.transform.localScale = new Vector3(1, 1, 1);
        MovementSpriteRenderer.gameObject.SetActive(true);
    }
}
