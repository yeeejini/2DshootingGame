using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // ��ǥ : �ٸ� ��ü�� �浹�ϸ� �浹�� ��ü�� �ı�(����)�ع�����.
    // �������� : 
    // 1. ���࿡ �ٸ� ��ü�� �浹�ϸ�
    // 2. �浹�� ��ü�� �ı��ع�����.

    // 1. ���࿡ �ٸ� ��ü�� �浹�ϸ�
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // 2. �浹�� ��ü�� �ı��ع�����.
        Destroy(otherCollider.gameObject);
    }
}
