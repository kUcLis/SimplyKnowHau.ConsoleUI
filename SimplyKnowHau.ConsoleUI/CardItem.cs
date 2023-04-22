using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplyKnowHau.ConsoleUI
{
    public class CardItem
    {
        public int Id { get; set; }

        public int ContentId { get; set; }
        public string? CardString { get; set; }

        public string? CardContent { get; set; }

        public CardItem(int id, string cardstring)
        {
            Id = id;

            CardString = cardstring;
        }

        public CardItem(int id, string cardstring, string cardcontent)
        {
            Id = id;

            CardString = cardstring;

            CardContent = cardcontent;
        }

        public CardItem(int id, int contentId, string cardstring, string cardcontent)
        {
            Id = id;

            ContentId = contentId;

            CardString = cardstring;

            CardContent = cardcontent;
        }
    }
}
