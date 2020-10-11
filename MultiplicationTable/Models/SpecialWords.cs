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
        public string UserTappedWord;
        public int NumberWrongWordElement;
        public int NumberAllWordsElement;

        public List<int> lstÓ; 
        public List<int> lstż;
        public List<int> lstź;
        public List<int> lsth;
        public List<int> lsthLarge;
        public List<int> lstch;
        public List<int> lstchLarge;
        public List<int> lstrz;
        public List<int> lstrzLarge;
        public List<int> lstu;
        public List<int> lstuLarge;

        private List<string> Exceptions;
        private List<int> ExceptionPos;



        private string[] exceptions = new string[] { "ó", "ż", "h", "H", "ch", "Ch", "rz", "Rz", "u", "U" };
        
        public SpecialWords(string word)
        {
            Exceptions = new List<string>();
            ExceptionPos = new List<int>();
            ProperWord = word;
            
        }

        public void SetLetterPlaces()
        {
            lstÓ = AllIndexesOf(ProperWord, "ó");
            lstż = AllIndexesOf(ProperWord, "ż");
            lstź = AllIndexesOf(ProperWord, "ź");
            lsth = AllIndexesOf(ProperWord, "h");
            lsthLarge = AllIndexesOf(ProperWord, "H");
            lstch = AllIndexesOf(ProperWord, "ch");
            lstchLarge = AllIndexesOf(ProperWord, "Ch");
            lstrz = AllIndexesOf(ProperWord, "rz");
            lstrzLarge = AllIndexesOf(ProperWord, "Rz");
            lstu = AllIndexesOf(ProperWord, "u");
            lstuLarge = AllIndexesOf(ProperWord, "U");

            #region UNUSED
            /*
            List<string> lstWrong = new List<string>(currentLst);
            string bledne = string.Empty;
            if (currentLst.Count == 0)
            {
                bledne = ProperWord;

                foreach (string e in exceptions)
                {
                    bledne = GenerateWrongWord(e, bledne);
                    if (bledne != ProperWord && !lstWrong.Contains(bledne))
                    {
                        lstWrong.Add(bledne);
                    }
                }
            }
            else
            {
                foreach(string word in currentLst)
                {
                    bledne = word;
                    foreach (string e in exceptions)
                    {
                        bledne = GenerateWrongWord(e, bledne);
                        if (bledne != ProperWord && !lstWrong.Contains(bledne))
                        {
                            lstWrong.Add(bledne);
                        }
                    }
                }
            }
            //List<string> bonusLst = BonusWrongWords(lstWrong.Count, lstWrong);
            przebieg++;
            if (przebieg == 10)
            {
                return lstWrong;
            }
            else
            {
                return GetWrongWords(przebieg, lstWrong);
            }*/
            #endregion
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
            if (currentWord.Contains("ó") && letter=="ó")
            {
                return currentWord.Replace("ó", "u");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("u") && letter == "u")
            {
                return currentWord.Replace("u", "ó");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("U") && letter == "U")
            {
                return currentWord.Replace("U", "Ó");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("Ż") && letter == "Ż")
            {
                return currentWord.Replace("Ż", "Rz");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("ż") && letter == "ż")
            {
                return currentWord.Replace("ż", "rz");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("H") && letter == "H")
            {
                return currentWord.Replace("H", "Ch");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("h") && currentWord.Contains("ch")==false && currentWord.Contains("Ch")==false && letter == "h")
            {
                return currentWord.Replace("h", "ch");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("rz") && letter == "rz")
            {
                return currentWord.Replace("rz", "ź");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("Rz") && letter == "Rz")
            {
                return currentWord.Replace("Rz", "Ż");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("Ch") && letter == "Ch")
            {
                return currentWord.Replace("Ch", "H");
                //return GenerateWrongWord(letter, word);
            }
            else if (currentWord.Contains("ch") && letter == "ch")
            {
                return currentWord.Replace("ch", "h");
                //return GenerateWrongWord(letter, word);
            }
            else
            {
                return currentWord;
            }
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
            if (currentWord.Contains("h") && (!currentWord.Contains("ch")&&!currentWord.Contains("Ch")))
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
