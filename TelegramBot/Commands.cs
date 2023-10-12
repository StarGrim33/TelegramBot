using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    internal class Commands
    {
        public const string COMMAND_LIST = "Список комманд: " +
            "\n/sign <eng> - записаться на уборку " +
            "\n/help <eng> - связаться с нами " +
            "\n/faq <eng> - часто задаваемые вопросы и ответы " +
            "\n/schedule <eng> - график работы ";

        public const string StartCommand = "/start";
        public const string HelpCommand = "/help";
        public const string SignUpCommand = "/sign";
        public const string YesCommand = "да";
        public const string HelloCommand = "привет";
        public const string FaqCommand = "/faq";
        public const string HouseCommand = "дом";
        public const string ApartmentCommand = "квартира";
        public const string OneRoomApartmentCommand = "одна";
        public const string TwoRoomApartmentCommand = "две";
        public const string ThreeRoomApartmentCommand = "три";
        public const string FourRoomApartmentCommand = "четыре";
        public const string CommonCleanCommand = "поддерживающая";
        public const string SuperCleanCommand = "генеральная";
        public const string CallCommand = "позвоните мне";
    }
}
