using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace MultiplicationTable.Models
{
    public class SpecialWords
    {
        public string ProperWord;
        private List<string> Exceptions;
        private List<int> ExceptionPos;
        
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
            string wrongWord = GenerateWrongWord("ó");
            if(!string.IsNullOrEmpty(wrongWord))
            {
                lstWrong.Add(wrongWord);
            }

            return lstWrong;
        }

        private string GenerateWrongWord(string letter)
        {
            if(ProperWord.Contains("ó"))
            {
                return ProperWord.Replace("ó", "u");
            }
            if(ProperWord.Contains("u"))
            {
                return ProperWord.Replace("u", "ó");
            }
            if(ProperWord.Contains("U"))
            {
                return ProperWord.Replace("U", "Ó");
            }
            if (ProperWord.Contains("Ż"))
            {
                return ProperWord.Replace("Ż", "Rz");
            }
            if(ProperWord.Contains("ż"))
            {
                return ProperWord.Replace("ż", "rz");
            }
            

            return string.Empty;
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
