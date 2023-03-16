using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Core.Utils
{
    public class MenuState : IMenuState
    {
        public bool displayMenu { get; set; }
        public int idGroup { get; set; }

        public bool displayBack { get; set; }
        public string? msgBack { get; set; }
        public event Action? OnChange;

        public void SetDisplayGroupMenu(bool val, int id)
        {
            displayMenu = val;
            displayBack = false;
            idGroup = id;
            NotifyStateChanged();
        }

        public void SetDisplayGroupMenu(bool val)
        {
            SetDisplayGroupMenu(val, 0);
        }

        public bool GetDisplayGroupMenu()
        {
            return displayMenu;
        }

        public int GetIdGroup()
        {
            return idGroup;
        }

        public void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }

        public void SetOnChange(Action action)
        {
            OnChange += action;
        }

        public void UnsetOnChange(Action action)
        {
            OnChange -= action;
        }

        public void SetDisplayBack(bool display, string msg)
        {
            displayBack = display;
            displayMenu = false;
            msgBack = msg;
            NotifyStateChanged();
        }

        public bool getDisplayBack()
        {
            return displayBack;
        }

        public string getDisplayBackMsg()
        {
            return msgBack;
        }
    }
}
