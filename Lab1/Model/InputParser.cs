using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Helpers;
using Lab1.Model;
using Lab1.Model.Abstract;

namespace Lab1.Model {
    /// <summary>
    /// Input Parser ansvarar för att tolka och utföra de kommandon användaren matar in
    /// </summary>
    public class InputParser {
        private IRepository _repository;
        public InputParser() {
        }
        public InputParser(IRepository _repository) {
            this._repository = _repository;
            ParserState = State.Default;
        }
        Logger logger = new Logger();
        List list = new List();
        private State ParserState;
        private enum State {
            Default,
            Exit
        }
        /// <summary>
        /// Sätter ParserState till Default
        /// </summary>
        public void SetDefaultParserState() {
            ParserState = State.Default;
        }
       
        /// <summary>
        /// Sätter ParserState till Exit
        /// </summary>
        private void SetExitParserState() {
            ParserState = State.Exit;
        }
         /// <summary>
        /// Returnerar true om ParserState är Exit (eller rättare sagt -1)
        /// </summary>
        public bool IsStateExit {
            get {
                return ParserState == State.Exit;
            }
        }
        /// <summary>
        /// Tolka input baserat på vilket tillstånd (ParserState) InputParser-objektet befinner sig i.
        /// </summary>
        /// <param name="input">Input sträng som kommer från användaren.</param>
        /// <returns></returns>
        public string ParseInput(string input) {
            if (ParserState == State.Default) {

                return ParseDefaultStateInput(input);
            }
            else if (ParserState == State.Exit)
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
        public string ParseDefaultStateInput(string input)
        {
            List<User> UserNames = _repository.GetUsers();      
            string inputLower = input.ToLower();
            logger.Log(inputLower);
            string result;
            switch (inputLower)
            {
                case "?": // Inget break; eller return; => ramlar igenom till nästa case (dvs. ?/help hanteras likadant)
                case "help":
                    result = OutputHelper.RootCommandList;
                    break;
                case "log":
                    result = logger.ToString();
                    break;                
                case "func<int, bool>": 
                    result = OutputHelper.func;
                    break;
                case "dictionary":
                    result = OutputHelper.dictionary;
                    break;
                case "list":
                    result = list.ListUsers(UserNames);
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