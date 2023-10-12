using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            int myChatID = 385769216;
            bool isSendToMe = false;
            string accessToken = "6093975646:AAHEGveuvJRB4fUc3O6JNn9bLPkThHiygxc";
            var botClient = new TelegramBotClient(accessToken);
            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new() { AllowedUpdates = Array.Empty<UpdateType>() };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();

            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
                {
                new KeyboardButton[] { "Позвоните мне" },
                })
                {
                    ResizeKeyboard = true
                };

                if (update.Message is not { } message)
                    return;

                if (message.Text is not { } messageText)
                    return;

                var chatId = message.Chat.Id;

                Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

                var lower = message.Text.ToLower();

                if (isSendToMe)
                {
                    Message sentToMeMessage = await botClient.SendTextMessageAsync(
                        chatId: myChatID,
                        text: messageText + Environment.NewLine + $"Сообщение от @{update.Message.From.Username}",
                        cancellationToken: cancellationToken);
                }

                if (lower.StartsWith(Commands.StartCommand))
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Привет, {message.Chat.Username}.\nМы клиниговая компания diamond cleaning :) \n" +
                    $"Хотите, чтобы мы убрались у вас? \n",
                    cancellationToken: cancellationToken);
                    Message sticker = await botClient.SendStickerAsync(chatId: chatId, sticker: InputFile.FromUri(Stickers.HelloSticker), replyMarkup: new InlineKeyboardMarkup(
                    InlineKeyboardButton.WithUrl(
                    text: "Наша группа Вконтакте",
                    url: "https://vk.com/diamondclean1ng")), cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.YesCommand))
                {
                    //isSendToMe = true;
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"У вас квартира или дом?",
                    cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.HouseCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Сколько у вас квадратных метров?",
                    cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.ApartmentCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                   chatId: chatId,
                   text: $"Сколько у вас комнат?",
                   cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.OneRoomApartmentCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: $"Нужна поддерживающая уборка или генеральная?",
                  cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.TwoRoomApartmentCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: $"Нужна поддерживающая уборка или генеральная?",
                  cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.ThreeRoomApartmentCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: $"Нужна поддерживающая уборка или генеральная?",
                  cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.FourRoomApartmentCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                  chatId: chatId,
                  text: $"Нужна поддерживающая уборка или генеральная?",
                  cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.CommonCleanCommand) || lower.StartsWith(Commands.SuperCleanCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Мы вам очень благодарны. В ближайшее время с вами свяжется менеджер",
                    cancellationToken: cancellationToken);
                }
                else if (lower.StartsWith(Commands.CallCommand))
                {
                    Message firstAnswer = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Введите номер телефона: ",
                    cancellationToken: cancellationToken);
                }
                else
                {
                    Message sentMessage = await botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: $"Привет, {message.Chat.Username}.\nМы клиниговая компания diamond cleaning :) \n" +
                    $"{Commands.COMMAND_LIST}? \n", replyMarkup: replyKeyboardMarkup,
                    cancellationToken: cancellationToken);
                }
            }

            Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
            {
                var ErrorMessage = exception switch
                {
                    ApiRequestException apiRequestException
                        => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                    _ => exception.ToString()
                };

                Console.WriteLine(ErrorMessage);
                return Task.CompletedTask;
            }

        }
    }
}