using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Scoped-service to shared the state of the NavMenu across different components.
/// Based on: https://stackoverflow.com/a/60951797/6156666
/// </summary>
namespace ResearchRepository.Domain.People.Repositories
{
    public class MenuStateService
    {
        public bool displayMenuGroup { get; set; } = false;
        public bool displayBack { get; set; } = false;

        public void SetDisplayMenuGroup(bool val){
            displayMenuGroup = val;
        }

        public void SetDisplayBack(bool val){
            displayBack = val;
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
