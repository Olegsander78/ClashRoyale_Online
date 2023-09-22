using UnityEngine;
using UnityEngine.UI;

namespace Registartion_Authorization
{
    public class RegistrationUI_Adapter : MonoBehaviour
    {
        [SerializeField] private Registration _registration;
        [SerializeField] private InputField _loginInput;
        [SerializeField] private InputField _passwordInput;
        [SerializeField] private InputField _confirmPasswordInput;
        [SerializeField] private Button _applyButton;
        [SerializeField] private Button _signInButton;
        [SerializeField] private GameObject _authorizationCanvas;
        [SerializeField] private GameObject _registrationCanvas;

        private void Awake()
        {
            _loginInput.onEndEdit.AddListener(_registration.SetLogin);
            _passwordInput.onEndEdit.AddListener(_registration.SetPassword);
            _confirmPasswordInput.onEndEdit.AddListener(_registration.SetConfirmPassword);

            _applyButton.onClick.AddListener(OnSignUpClicked);
            _signInButton.onClick.AddListener(OnSignInClicked);

            _registration.OnErrorOccured += OnRegistrationWithErrorEnded;
            _registration.OnSuccessOccured += OnRegistrationWithSuccesEnded;
        }

        private void OnDisable()
        {
            _loginInput.onEndEdit.RemoveListener(_registration.SetLogin);
            _passwordInput.onEndEdit.RemoveListener(_registration.SetPassword);

            _applyButton.onClick.RemoveListener(OnSignUpClicked);
            _signInButton.onClick.RemoveListener(OnSignInClicked);

            _registration.OnErrorOccured -= OnRegistrationWithErrorEnded;
            _registration.OnSuccessOccured -= OnRegistrationWithSuccesEnded;
        }

        private void OnRegistrationWithErrorEnded()
        {
            _applyButton.gameObject.SetActive(true);
            _signInButton.gameObject.SetActive(true);
        }

        private void OnRegistrationWithSuccesEnded()
        {
            _signInButton.gameObject.SetActive(true);
        }

        private void OnSignUpClicked()
        {
            _applyButton.gameObject.SetActive(false);
            _signInButton.gameObject.SetActive(false);
            _registration.SignUp();
        }

        private void OnSignInClicked()
        {
            _registrationCanvas.SetActive(false);
            _authorizationCanvas.SetActive(true);
        }
    }
}
