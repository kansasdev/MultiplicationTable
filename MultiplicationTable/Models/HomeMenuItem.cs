using System;
using System.Collections.Generic;
using System.Text;

namespace MultiplicationTable.Models
{
    public enum MenuItemType
    {
        Multiplication,
        Dictation,
        Configuration
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
