using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This services saves and sets the state of the menu so it can display the diferent options depending on the
/// view and the parameters
/// </summary>
/// Author: Tyron Fonseca

namespace ResearchRepository.Application.Core.Utils
{
    public interface IMenuState
    {
        /// <summary>
        /// Get the current state of the Menu of Groups (displayed or not)
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <returns>Current state of the Menu of Groups</returns>
        bool GetDisplayGroupMenu();

        /// <summary>
        /// Get the ID of the current group displayed
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <returns>ID of the group displayed</returns>
        int GetIdGroup();

        /// <summary>
        /// Change the state of the Menu of Groups
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <param name="val">New state of the menu of Groups</param>
        /// <param name="id"> Id of the group</param>
        void SetDisplayGroupMenu(bool val, int id);

        /// <summary>
        /// Change the state of the Menu of Groups
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <param name="val">New state of the menu of Groups</param>
        void SetDisplayGroupMenu(bool val);

        /// <summary>
        /// Evoke the action assocciate to the change of variables
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        void NotifyStateChanged();

        /// <summary>
        /// Set the action called when a variable of the state changes
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <param name="action">Action to invoke OnChange</param>
        void SetOnChange(Action action);

        /// <summary>
        /// Unset the action called when a variable of the state changes
        /// </summary>
        /// Author: Tyron Fonseca
        /// StoryID: ST-MM-2.2
        /// <param name="action">Action to invoke OnChange</param>
        void UnsetOnChange(Action action);

        /// <summary>
        /// Display a back button with a references
        /// </summary>
        /// <param name="display">Display or not the back button</param>
        /// <param name="msg">Message to display</param>
        void SetDisplayBack(bool display, string msg);

        /// <summary>
        /// Get if display the back or not
        /// </summary>
        /// <returns>Display back or not</returns>
        bool getDisplayBack();

        /// <summary>
        /// Get the message in the display back
        /// </summary>
        /// <returns>Message in the display back</returns>
        string getDisplayBackMsg();
    }
}