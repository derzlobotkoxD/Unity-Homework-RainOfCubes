using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : Item
{
    public event UnityAction<Bomb> Deleted;

    private void OnEnable() =>
        StartCoroutine(Disappear());

    private IEnumerator Disappear()
    {
        float delay = GetRandomDelay();
        float time = 0;
        Color color = Renderer.material.color;

        while (time < delay)
        {
            time += Time.deltaTime;
            color.a -= Time.deltaTime / delay;
            Renderer.material.color = color;

            yield return null;
        }

        Deleted?.Invoke(this);
    }
}