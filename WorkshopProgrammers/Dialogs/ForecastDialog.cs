using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;

namespace WorkshopProgrammers.Dialogs
{
    public class ForecastDialog : IDialog
    {
        private readonly ForecastAPI.IForecastAPI Forecast;
        private readonly string entity;

        public ForecastDialog(string entity)
        {
            this.entity = entity;
            Forecast = new ForecastAPI.ForecastAPI();
        }

        public async Task StartAsync(IDialogContext context)
        {
            //Coleta previsões do tempo.
            var results = await Forecast.GetForecast(entity);
            if (results != null)
            {
                //Cria uma nova Activity.
                var message = context.MakeMessage();
                
                //Cultura pt-BR para traduzirmos o nome dos dias da semana.
                var culture = new System.Globalization.CultureInfo("pt-BR");

                foreach (var item in results)
                {
                    //Insere cards interativos como anexos na mensagem.
                    message.Attachments.Add(
                        new ThumbnailCard()
                        {
                            Title = culture.DateTimeFormat.GetDayName(item.dia.DayOfWeek),
                            Subtitle = item.dia.ToString("dd-MM-yyyy"),
                            Text = $"Máxima: {item.maxima}\n" +
                            $"Mínima: {item.minima}"
                        }.ToAttachment());
                }

                //Envia a mensagem contendo os cards para o usuário.
                await context.PostAsync(message);

                //Finaliza o diálogo atual, retornando o controle para o RootDialog.
                context.Done("");
            }
            else
            {
                //Envia mensagem para o usuário.
                await context.PostAsync($"Não consegui encontrar nenhuma previsão para \"{entity}\".");

                //Finaliza o diálogo atual, retornando o controle para o RootDialog.
                context.Done("");
            }
        }
    }
}