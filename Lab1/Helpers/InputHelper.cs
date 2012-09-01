using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1.Helpers
{
    /// <summary>
    /// InputHelper är ansvarig för att ta emot användarinput
    /// </summary>
    public class InputHelper
    {
        /// <summary>
        /// Tar emot en sträng från konsollen
        /// </summary>
        /// <returns>En sträng som tagits emot från användaren</returns>
        public static string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}