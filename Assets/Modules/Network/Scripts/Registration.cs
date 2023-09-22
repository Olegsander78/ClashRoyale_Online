using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Registartion_Authorization
{
    public class Registration : MonoBehaviour
    {
        private const string LOGIN = "login";
        private const string PASSWORD = "password";

        public event Action OnErrorOccured;
        public event Action OnSuccessOccured;

        private string _login;
        private string _password;
        private string _confirmPassword;

        public void SetLogin(string login)
        {
            _login = login;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public void SetConfirmPassword(string confirmPassword)
        {
            _confirmPassword = confirmPassword;
        }

        public void SignUp()
        {
            if(string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password) || string.IsNullOrEmpty(_confirmPassword))
            {
                ErrorMessage("Login and/or password are empty!");
                return;
            }

            if(_password != _confirmPassword)
            {
                ErrorMessage("Password mismatch!");
                return;
            }

            var uri = URILibrary.MAIN + URILibrary.REGISTRATION;

            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {LOGIN, _login },
                {PASSWORD, _password }
            };

            Network.Instance.Post(uri, data, SuccessMessage, ErrorMessage);
        }

        private void SuccessMessage(string data)
        {            
            if(data != "ok")
            {
                ErrorMessage($"Server respond, error: {data}");
                return;
            }

            Debug.Log($"Registration success");
            OnSuccessOccured?.Invoke();
        }

        private void ErrorMessage(string error)
        {
            Debug.LogError(error);
            OnErrorOccured?.Invoke();
        }
    }
}
