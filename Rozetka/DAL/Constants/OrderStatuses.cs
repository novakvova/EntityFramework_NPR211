using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Constants
{
    public static class OrderStatuses
    {
        public static List<string> All = new List<string>
        {
            InProcessing,
            Deliver,
            Successfully,
            UnSuccessfully,
            Canceled
        };
        public const string InProcessing = "В обробці";
        public const string Deliver = "Доставляється";
        public const string Successfully = "Успішно";
        public const string UnSuccessfully = "Не успішно";
        public const string Canceled = "Скасовано";
    }
}
