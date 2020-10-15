using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplicationTable.Services
{
    public static class EquationResults
    {
        private static int okAnswers;
        private static int badAnswers;
        
        public static void Resest()
        {
            okAnswers = 0;
            badAnswers = 0;
        }

        public static void AddOkAnswer()
        {
            okAnswers = okAnswers + 1;
        }

        public static void AddBadAnswer()
        {
            badAnswers = badAnswers + 1;
        }

        public static string GetTotalAnswers()
        {
            return (badAnswers + okAnswers).ToString();
        }

        public static string GetOkAnswers()
        {
            return okAnswers.ToString();
        }

        public static string GetBadAnswers()
        {
            return badAnswers.ToString();
        }
    }
}
