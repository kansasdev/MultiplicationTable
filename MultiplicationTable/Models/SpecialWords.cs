using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MultiplicationTable.Models
{
    public class SpecialWords
    {
        public string ProperWord;
        public string DashedWord;
        private List<string> Exceptions;
        private List<int> ExceptionPos;

        private string[] exceptions = new string[] { "ó", "ż", "h", "H", "ch", "Ch", "rz", "Rz", "u", "U" };
        
        public SpecialWords(string word)
        {
            Exceptions = new List<string>();
            ExceptionPos = new List<int>();
            ProperWord = word;
            
        }

        public List<string> GetWrongWords()
        {
            List<int> lstÓ = AllIndexesOf(ProperWord, "ó");
            List<int> lstż = AllIndexesOf(ProperWord, "ż");
            List<int> lstź = AllIndexesOf(ProperWord, "ź");
            List<int> lsth = AllIndexesOf(ProperWord, "h");
            List<int> lsthLarge = AllIndexesOf(ProperWord, "H");
            List<int> lstch = AllIndexesOf(ProperWord, "ch");
            List<int> lstchLarge = AllIndexesOf(ProperWord, "Ch");
            List<int> lstrz = AllIndexesOf(ProperWord, "rz");
            List<int> lstrzLarge = AllIndexesOf(ProperWord, "Rz");
            List<int> lstu = AllIndexesOf(ProperWord, "u");
            List<int> lstuLarge = AllIndexesOf(ProperWord, "U");

            List<string> lstWrong = new List<string>();
            string bledne = string.Empty;
            foreach(string e in exceptions)
            {
                bledne = GenerateWrongWord(e,bledne);
               
            }
            lstWrong.Add(bledne);

            return lstWrong;
        }

        public string GetDashedWord()
        {
            string word = ProperWord;

            foreach(string e in exceptions)
            {
                word = GenerateDashedWord(e,word);
            }

            return word;
        }

        private string GenerateWrongWord(string letter,string currentWord)
        {
            if(currentWord.Contains("ó"))
            {
                return currentWord.Replace("ó", "u");
            }
            if(currentWord.Contains("u"))
            {
                return currentWord.Replace("u", "ó");
            }
            if(currentWord.Contains("U"))
            {
                return currentWord.Replace("U", "Ó");
            }
            if (currentWord.Contains("Ż"))
            {
                return currentWord.Replace("Ż", "Rz");
            }
            if(currentWord.Contains("ż"))
            {
                return currentWord.Replace("ż", "rz");
            }
            if(currentWord.Contains("H"))
            {
                return currentWord.Replace("H", "Ch");
            }
            if(currentWord.Contains("h") && (!currentWord.Contains("ch")||!currentWord.Contains("Ch")))
            {
                return currentWord.Replace("h", "ch");
            }
            if(currentWord.Contains("rz"))
            {
                return currentWord.Replace("rz", "ź");
            }
            if(currentWord.Contains("Rz"))
            {
                return currentWord.Replace("Rz", "Ż");
            }
            if(currentWord.Contains("Ch"))
            {
                return currentWord.Replace("Ch", "H");
            }
            if(currentWord.Contains("ch"))
            {
                return currentWord.Replace("ch", "h");
            }
            

            return currentWord;
        }

        private string GenerateDashedWord(string letter,string currentWord)
        {
            if (currentWord.Contains("ó"))
            {
                return currentWord.Replace("ó", "_");
            }
            if (currentWord.Contains("u"))
            {
                return currentWord.Replace("u", "_");
            }
            if (currentWord.Contains("U"))
            {
                return currentWord.Replace("U", "_");
            }
            if (currentWord.Contains("Ż"))
            {
                return currentWord.Replace("Ż", "_");
            }
            if (currentWord.Contains("ż"))
            {
                return currentWord.Replace("ż", "_");
            }
            if (currentWord.Contains("H"))
            {
                return currentWord.Replace("H", "_");
            }
            if (currentWord.Contains("h") && (!currentWord.Contains("ch")||!currentWord.Contains("Ch")))
            {
                return currentWord.Replace("h", "_");
            }
            if (currentWord.Contains("rz"))
            {
                return currentWord.Replace("rz", "_");
            }
            if (currentWord.Contains("Rz"))
            {
                return currentWord.Replace("Rz", "_");
            }
            if (currentWord.Contains("Ch"))
            {
                return currentWord.Replace("Ch", "_");
            }
            if (currentWord.Contains("ch"))
            {
                return currentWord.Replace("ch", "_");
            }


            return currentWord;
        }

        public List<int> AllIndexesOf(string str, string value)
        {
            if (String.IsNullOrEmpty(value))
                throw new ArgumentException("the string to find may not be empty", "value");
            List<int> indexes = new List<int>();
            for (int index = 0; ; index += value.Length)
            {
                index = str.IndexOf(value, index);
                if (index == -1)
                    return indexes;
                indexes.Add(index);
            }
        }
    }
}
