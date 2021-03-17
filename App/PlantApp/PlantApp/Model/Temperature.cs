using System;
using System.Collections.Generic;
using System.Text;

namespace PlantApp.Model
{
    class Temperature
    {
        private int tempId;
        private string temp;
        private DateTime date;

        public int TempId { get => tempId; set => tempId = value; }
        public string Temp { get => temp; set => temp = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
