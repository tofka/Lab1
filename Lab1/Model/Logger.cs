using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Model;
using Lab1.Helpers;

namespace Lab1 {
    public class Logger {
        private static List<string> LastStrings = new List<string>();
        public void Log(string msg) {
            if (LastStrings.Count < 10) {
                LastStrings.Add(msg);
            }
            else {
                LastStrings.RemoveAt(0);
                LastStrings.Add(msg);
            }
        }
        public override string ToString() {
            string msg = "\n\nList of latest commands:\n";
            foreach (string item in LastStrings) {
                msg += item + "\n";
            }
            return string.Format("{0}", msg);
        }
    }
}