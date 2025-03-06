using System;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private LedgeGrabDetector grabDetector;
    [SerializeField] private CharacterLedgeHandler characterLedgeHandler;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Toggle enableLedgeToggle;
    [SerializeField] private Toggle groundVelocityToggle;
    [SerializeField] private Toggle airVelocityToggle;
    [SerializeField] private Toggle doubleJumpToggle;

    [SerializeField] private Slider moveSpeedSlider;
    [SerializeField] private Slider airSpeedSlider;
    [SerializeField] private Slider jumpForceSlider;
    [SerializeField] private Slider doubleJumpForceSlider;

    [SerializeField] private Text moveSpeedText;
    [SerializeField] private Text airSpeedText;
    [SerializeField] private Text jumpForceText;
    [SerializeField] private Text doubleJumpForceText;

    private void Start()
    {
        grabDetector = GetComponent<LedgeGrabDetector>();
        characterLedgeHandler = GetComponent<CharacterLedgeHandler>();
        characterMovement = GetComponent<CharacterMovement>();

        // Устанавливаем начальные значения
        moveSpeedSlider.value = GetMoveSpeed();
        airSpeedSlider.value = GetAirSpeed();
        jumpForceSlider.value = GetJumpForce();
        doubleJumpForceSlider.value = GetDoubleJumpForce();

        moveSpeedText.text = GetMoveSpeed().ToString();
        airSpeedText.text = GetAirSpeed().ToString();
        jumpForceText.text = GetJumpForce().ToString();
        doubleJumpForceText.text = GetDoubleJumpForce().ToString(); 

        enableLedgeToggle.isOn = true;
        groundVelocityToggle.isOn = GET_GROUND_VELOCITY_MODE();
        airVelocityToggle.isOn = GET_AIR_VELOCITY_MODE();
        doubleJumpToggle.isOn = GET_DOUBLE_JUMP_ENABLE();

        // Привязываем события
        moveSpeedSlider.onValueChanged.AddListener(SetMoveSpeed);
        airSpeedSlider.onValueChanged.AddListener(SetAirSpeed);
        jumpForceSlider.onValueChanged.AddListener(SetJumpForce);
        doubleJumpForceSlider.onValueChanged.AddListener(SetDoubleJumpForce);

        groundVelocityToggle.onValueChanged.AddListener(SetGroundVelocityMode);
        airVelocityToggle.onValueChanged.AddListener(SetAirVelocityMode);
        doubleJumpToggle.onValueChanged.AddListener(SetDoubleJumpEnable);
        enableLedgeToggle.onValueChanged.AddListener(EnableLedge);
    }

    public void EnableLedge(bool value)
    {
        grabDetector.enabled = value;
        characterLedgeHandler.enabled = value;
    }

    public void SetMoveSpeed(float speed)
    {
        characterMovement.SetMoveSpeed(speed);
        moveSpeedText.text = GetMoveSpeed().ToString();
    }

    public void SetAirSpeed(float speed)
    {
        characterMovement.SetAirSpeed(speed);
        airSpeedText.text = GetAirSpeed().ToString();
    }

    public void SetJumpForce(float force)
    {
        characterMovement.SetJumpForce(force);
        jumpForceText.text = GetJumpForce().ToString();
    }

    public void SetDoubleJumpForce(float force)
    {
        characterMovement.SetDoubleJumpForce(force);
        doubleJumpForceText.text = GetDoubleJumpForce().ToString();
    }

    public void SetGroundVelocityMode(bool value)
    {
        characterMovement.SET_GROUND_VELOCITY_MODE(value);
    }

    public void SetAirVelocityMode(bool value)
    {
        characterMovement.SET_AIR_VELOCITY_MODE(value);
    }

    public void SetDoubleJumpEnable(bool value)
    {
        characterMovement.SET_DOUBLE_JUMP_ENABLE(value);
    }

    public bool GET_GROUND_VELOCITY_MODE() => characterMovement.GET_GROUND_VELOCITY_MODE();
    public bool GET_AIR_VELOCITY_MODE() => characterMovement.GET_AIR_VELOCITY_MODE();
    public bool GET_DOUBLE_JUMP_ENABLE() => characterMovement.GET_DOUBLE_JUMP_ENABLE();

    public float GetMoveSpeed() => characterMovement.GetMoveSpeed();
    public float GetAirSpeed() => characterMovement.GetAirSpeed();
    public float GetJumpForce() => characterMovement.GetJumpForce();
    public float GetDoubleJumpForce() => characterMovement.GetDoubleJumpForce();

    public void OnExitGame()
    {
        Application.Quit();
    }
}
