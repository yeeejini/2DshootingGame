using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ���� : ������ �����ϴ� ���� ������
public class ScoreManager : MonoBehaviour
{
    // ��ǥ : ���� ���� ������ ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�.
    // �ʿ� �Ӽ�
    // - ���� ������ ǥ���� UI
    public Text ScoreTextUI;
    // - ���� ������ ����� ����
    public int Score = 0;


    // �ְ� ���� ���� �Ӽ�
    public Text BestScoreTextUI;
    public int BestScore = 0;


    // ��ǥ : ������ ������ �� �ְ� ������ �ҷ�����, UI�� ǥ���ϰ� �ʹ�.
    // ���� ����
    // 1. ������ ������ ��
    private void Start()
    {
        // 2. �ְ� ������ �ҷ��´�.
        BestScore = PlayerPrefs.GetInt("BestScore", 0);

        // 3. UI�� ǥ���Ѵ�.
        BestScoreTextUI.text = $"�ְ� ���� : {BestScore}";
    }

















    // ��ǥ : ���� ���� ������ ������ �ø���, ���� ������ UI�� ǥ���ϰ� �ʹ�.
    // ���� ����
    // 1. ���࿡ ���� ������?
    // 2. ���ھ ���� ��Ų��.
    // 3. ���ھ ȭ�鿡 ǥ���Ѵ�.
}
