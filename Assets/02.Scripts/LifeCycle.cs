using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{
    // ����Ƽ p121
    private void Awake()
    {
        // �ν��Ͻ�ȭ �� ���Ŀ� ȣ��ȴ�.
        // �ַ� ������ ���³� ������ �ʱ�ȭ�� �� ���
        Debug.Log("Awake");
    }

    private void OnEnable()
    {
        // ��� ������ ������ ȣ��ȴ�.
        // ����ڰ� ���� �̺�Ʈ�� ������ ��
        Debug.Log("OnEnable");
    }

    private void Start()
    {
        // ������ �� ȣ��ȴ�.
        // �ٸ� ��ũ��Ʈ�� ��� Awake ��� ����� ���Ŀ� ȣ��ȴ�.
        Debug.Log("Start");
    }

    // Input ������Ʈ

    private void Update()
    {
        // �� �����Ӹ��� ȣ���Ѵ�.
        // Debug.Log("Update");
    }

    // �ڷ�ƾ ������Ʈ

    private void LateUpdate()
    {
        // ��� Ȱ��ȭ�� ��ũ��Ʈ�� Update �Լ��� ȣ��ǰ� ���� �� ���� ȣ��ȴ�.
    }

    private void OnDisable()
    {
        // ��� �Ұ����� ������ ȣ��ȴ�.
        // ����ڰ� ���� �̺�Ʈ�� ���� ���� �� ��
        Debug.Log("OnDisable");
    }
}
