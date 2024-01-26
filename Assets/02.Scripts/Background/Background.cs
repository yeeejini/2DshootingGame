using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float Speed;
    private void Update()
    {
        // ��� ��ũ��
        // -> ���� ȭ�鿡�� ��� �̹����� ������ �ӵ���
        // ������ ĳ���ͳ� ���� ���� �������� �� �������� ������ִ� ���
        // [ ĳ���ʹ� �״�� �ΰ� ��游 �������� ĳ���Ͱ� �����̴� ��ó�� �������� �Ѵ�.]


        // ��ǥ : �Ʒ��� �̵��ϰ� �ʹ�.
        // ���� : 
        // 1. ������ ���ϰ�
        Vector2 background_dir = Vector2.down;

        // 2. �̵��Ѵ�.
        Vector2 newPosition = transform.position + (Vector3)(background_dir * Speed) * Time.deltaTime;


        if (newPosition.y < -12.6f) 
        {
            newPosition.y = 12.6f;
        }
        transform.position = newPosition;
    }
}
