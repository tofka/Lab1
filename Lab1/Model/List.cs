using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lab1.Model;
      
namespace Lab1 {
    public class List {       
        public string ListUsers(List<User> list) {
            var query = list.Select(u => u.UserName).Take(10);
            string user = "\n\nList 10 users:\n";
            foreach (string q in query) {
                user += q + "\n";
            }
            return string.Format("{0}", user);
        } 
    }
}