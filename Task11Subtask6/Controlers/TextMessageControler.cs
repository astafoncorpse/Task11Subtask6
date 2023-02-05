using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types.Enums;

namespace VoiceTexterBot.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;

        public TextMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramClient = telegramBotClient;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            switch (message.Text)
            {
                case "/start":

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Количество знаков сообщения" , $"Количество знаков в сообщеннии"),
                        InlineKeyboardButton.WithCallbackData($" Сумма знаков сообщения" , $"Сумма знаков в сообщении")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b>  Наш бот ведет подсчет символов.</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Можно узнать количество или сумму знаков сообщения.{Environment.NewLine}", cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;
                default:
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправьте сообщение", cancellationToken: ct);
                    break;
            }
        }
    }
}