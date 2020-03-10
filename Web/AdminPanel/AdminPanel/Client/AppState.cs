using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanel.Client
{
    public class AppState
    {
        private bool _authenticated = false;
        public bool Authenticated
        {
            get
            {
                return _authenticated;
            }
            set
            {
                _authenticated = value;
                OnChange?.Invoke();
            }
        }

        public event Action OnChange;
    }
}
