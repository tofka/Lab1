using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//4. Skapa en statisk metod i OutputHelper som heter Put och som tar en sträng som parameter och skriver ut denna sträng till konsolen. 
//Ersätt de Console.WriteLine i main-funktionen med denna funktion. [1p]
namespace Lab1.Helpers
{
    /// <summary>
    /// En hjälp-klass som hanterar utskrift till konsolen
    /// </summary>
    public class OutputHelper
    {
        /// <summary>
        /// Property som innehåller en textsträng som berättar vilka kommandon som finns tillgängliga
        /// då användaren startat programmet.
        /// </summary>
        public static string RootCommandList {
            get
            {
                string returnString = "\n\nList of Commands:";
                returnString += "\n\t?/help:\tPrints this list of commands.";
                returnString += "\n\texit:\tExits the program.";
                returnString += "\n\tlog:\tlist latest commands.";
                returnString += "\n\tFunc<int,bool>:\tExplain Func<int, bool>.";
                returnString += "\n\tDictionary:\tShow interface";
                returnString += "\n\tList:\tList 10 users";
                return returnString;
            }
        }

        public static string dictionary
        {
            get
            {
                return string.Format("\n\nSystem.Collections.Generics.Dictionary implements IDictionary<TKey,TValue>.");
            }
        }

        /// <summary>
        /// Metod som returnerar det meddelande som skall visas då programmet avslutas
        /// </summary>
        /// <param name="message">[Optional] En sträng som läggs till i slutet på befintligt meddelande</param>
        /// <returns></returns>
        /// 
        public static void Put(string text) {
            Console.WriteLine(text);            
        }
        public static string ExitMessage(string message = "") {
            return string.Format("\n\nProgram closing. {0}", message);
        }

        /// <summary>
        /// Property som innehåller felmeddelande som skall ges om användaren matat in ett kommando som inte kan hanteras
        /// av programmet.
        /// </summary>
        public static string ErrorInvalidInput
        {
            get
            {
                return string.Format("\n\nInvalid Input! (type (? or help) and [enter] for help.)");
            }
        }

       public static string func
       {
            get
            {
            return string.Format("Func<int, bool> är en delegate som kan hålla ett lambda-uttryck med en int och en bool");
            }
        }    

        /// <summary>
        /// Property som innehåller felmeddelande som skall ges om programmet inte vet vilket
        /// tillstånd det befinner sig i.
        /// </summary>
        public static string ErrorLostState
        {
            get
            {
                return string.Format("\n\nError! I've lost my state! Returning to default state.");
            }
        }

        /// <summary>
        /// Property som innehåller det meddelande som skall ges för att be användaren mata in ett kommando
        /// </summary>
        public static string EnterCommand
        {
            get
            {
                return string.Format("\n\nPlease Enter command + [enter] (help: ?):");
            }
        }
        /// <summary>
        /// Property som innehåller ett välkomstmeddelande
        /// </summary>
        public static string GreetingMessage
        {
            get
            {
                return string.Format("\n\nWelcome! {0}", EnterCommand);
            }
        }
    }
}