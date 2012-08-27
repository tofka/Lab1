using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Helpers;

namespace Lab1.Model
{
    /// <summary>
    /// Input Parser ansvarar för att tolka och utföra de kommandon användaren matar in
    /// </summary>
    public class InputParser
    {
        /// <summary>
        /// ParserState används för att hålla reda på vilket tillstånd InputParser-objektet
        /// befinner sig i. 
        /// 
        /// Anledningen till att vi har olika tillstånd är för att veta vilka kommandon som skall finnas 
        /// tillgängliga för användaren.
        /// 
        /// Ett tillstånd skulle kunna vara att användaren har listat Users och därmed har tillgång
        /// till kommandon för att lista detaljer för en User. Ett annat tillstånd skulle kunna vara 
        /// att användaren håller på och lägger in en ny User och därmed har tillgång till kommandon
        /// för att sätta namn, etc, för användaren.
        /// 
        /// Som koden ser ut nu så finns endast två tillgängliga tillstånd, 1, som är Default State.
        /// Och -1 som är det tillståndet som InputParser går in i när programmet skall avslutas
        /// Ifall nya tillstånd implementeras skulle de kunna vara 2, 3, 4, etc.
        /// </summary>
        private int ParserState { get; set; }

        /// <summary>
        /// Sätter ParserState till Default
        /// </summary>
        public void SetDefaultParserState()
        {
            ParserState = 1;
        }

        /// <summary>
        /// Returnerar en int som motsvarar Default State
        /// </summary>
        private int DefaultParserState
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Sätter ParserState till Exit
        /// </summary>
        private void SetExitParserState()
        {
            ParserState = -1;
        }

        /// <summary>
        /// Returnerar en int som motsvarar Exit State
        /// </summary>
        private int ExitParserState
        {
            get
            {
                return -1;
            }
        }

        /// <summary>
        /// Returnerar true om ParserState är Exit (eller rättare sagt -1)
        /// </summary>
        public bool IsStateExit
        {
            get
            {
                return ParserState == ExitParserState;
            }
        }

        /// <summary>
        /// Tolka input baserat på vilket tillstånd (ParserState) InputParser-objektet befinner sig i.
        /// </summary>
        /// <param name="input">Input sträng som kommer från användaren.</param>
        /// <returns></returns>
        public string ParseInput(string input)
        {
            if (ParserState == DefaultParserState)
            {
                return ParseDefaultStateInput(input);
            }
            else if (ParserState == ExitParserState)
            {
                // Do nothing - program should exit
                return "";
            }
            else
            {
                SetDefaultParserState();
                return OutputHelper.ErrorLostState;
            }
        }

        /// <summary>
        /// Tolka och utför de kommandon som ges när InputParser är i Default State
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ParseDefaultStateInput(string input)
        {
            string result;
            switch (input)
            {
                case "?": // Inget break; eller return; => ramlar igenom till nästa case (dvs. ?/help hanteras likadant)
                case "help":
                    result = OutputHelper.RootCommandList;
                    break;
                case "exit":
                    SetExitParserState(); // Lägg märke till att vi utför en Action här.
                    result = OutputHelper.ExitMessage("Bye!"); // Det går bra att skicka parametrar
                    break;
                default:
                    result = OutputHelper.ErrorInvalidInput;
                    break;
            }
            return result + OutputHelper.EnterCommand;
        }
    }
}
