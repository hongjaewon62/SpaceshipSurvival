using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlScheme : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        // PlayerInput ������Ʈ ��������
        playerInput = GetComponent<PlayerInput>();

        // Input System �̺�Ʈ ������ ���
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDestroy()
    {
        // Input System �̺�Ʈ ������ ����
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput playerInput)
    {
        // Ȱ��ȭ�� Control Scheme �α׷� ���
        string defaultScheme = playerInput.defaultControlScheme;
        string activeScheme = playerInput.currentControlScheme;
        Debug.Log("Default Control Scheme: " + defaultScheme + activeScheme);
        Debug.Log("Active Control Scheme: " + activeScheme);
    }
}
