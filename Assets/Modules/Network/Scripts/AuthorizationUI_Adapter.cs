using UnityEngine;
using UnityEngine.UI;

namespace Registartion_Authorization
{
    public class AuthorizationUI_Adapter : MonoBehaviour
    {
        [SerializeField] private Authorization _authorization;
        [SerializeField] private InputField _loginInput;
        [SerializeField] private InputField _passwordInput;
        [SerializeField] private Button _signInButton;
        [SerializeField] private Button _signUpButton;
        [SerializeField] private GameObject _authorizationCanvas;
        [SerializeField] private GameObject _registrationCanvas;

        private void Awake()
        {
            _loginInput.onEndEdit.AddListener(_authorization.SetLogin);
            _passwordInput.onEndEdit.AddListener(_authorization.SetPassword);

            _signInButton.onClick.AddListener(OnSignInClicked);
            _signUpButton.onClick.AddListener(OnSignUpClicked);

            _authorization.OnErrorOccured += OnAuthorizationEnded;
        }

        private void OnDisable()
        {
            _loginInput.onEndEdit.RemoveListener(_authorization.SetLogin);
            _passwordInput.onEndEdit.RemoveListener(_authorization.SetPassword);

            _signInButton.onClick.RemoveListener(OnSignInClicked);
            _signUpButton.onClick.RemoveListener(OnSignUpClicked);

            _authorization.OnErrorOccured -= OnAuthorizationEnded;
        }

        private void OnAuthorizationEnded()
        {
            _signInButton.gameObject.SetActive(true);
            _signUpButton.gameObject.SetActive(true);
        }

        private void OnSignInClicked()
        {
            _signInButton.gameObject.SetActive(false);
            _signUpButton.gameObject.SetActive(false);
            _authorization.SignIn();
        }

        private void OnSignUpClicked()
        {
            _authorizationCanvas.SetActive(false);
            _registrationCanvas.SetActive(true);
        }
    }
}
