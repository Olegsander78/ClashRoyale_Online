using System;
using System.Collections.Generic;
using UnityEngine;

namespace Registartion_Authorization
{
    public class Authorization : MonoBehaviour
    {
        private const string LOGIN = "login";
        private const string PASSWORD = "password";

        public event Action OnErrorOccured;

        private string _login;
        private string _password;

        public void SetLogin(string login)
        {
            _login = login;
        }

        public void SetPassword(string password)
        {
            _password = password;
        }

        public void SignIn()
        {
            if(string.IsNullOrEmpty(_login) || string.IsNullOrEmpty(_password))
            {
                ErrorMessage("Логин и/или пароль пустые!");
                return;
            }

            var uri = URILibrary.MAIN + URILibrary.AUTHORIZATION;
            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {LOGIN, _login },
                {PASSWORD, _password }
            };

            Network.Instance.Post(uri, data, Success, ErrorMessage);
        }

        private void Success(string data)
        {
            string[] result = data.Split('|');
            if(result.Length < 2 || result[0] != "ok")
            {
                ErrorMessage($"Server respond, error: {data}");
                return;
            }

            if (int.TryParse(result[1], out int id))
            {
                UserInfo.Instance.SetID(id);
                Debug.Log($"Login success. User ID: {id}");
            }
            else
            {
                ErrorMessage($"Failed to parse \"{result[1]}\" to INT. Full response: {data}");
            }
        }

        private void ErrorMessage(string error)
        {
            Debug.LogError(error);
            OnErrorOccured?.Invoke();
        }
    }
}
