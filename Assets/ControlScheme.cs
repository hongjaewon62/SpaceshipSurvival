using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlScheme : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        // PlayerInput 컴포넌트 가져오기
        playerInput = GetComponent<PlayerInput>();

        // Input System 이벤트 리스너 등록
        playerInput.onControlsChanged += OnControlsChanged;
    }

    private void OnDestroy()
    {
        // Input System 이벤트 리스너 해제
        playerInput.onControlsChanged -= OnControlsChanged;
    }

    private void OnControlsChanged(PlayerInput playerInput)
    {
        // 활성화된 Control Scheme 로그로 출력
        string defaultScheme = playerInput.defaultControlScheme;
        string activeScheme = playerInput.currentControlScheme;
        Debug.Log("Default Control Scheme: " + defaultScheme + activeScheme);
        Debug.Log("Active Control Scheme: " + activeScheme);
    }
}
