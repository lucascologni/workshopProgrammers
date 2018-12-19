using AdaptiveCards;
using System;
using System.Collections.Generic;
using WorkshopProgrammers.Forecast;

namespace WorkshopProgrammers.Dialogs.AdaptiveCards
{
    public class ForecastAdaptiveCard
    {
        public AdaptiveCard GetAdaptiveCard(ForecastResult item)
        {
            //Cultura pt-BR para traduzirmos o nome dos dias da semana.
            var culture = new System.Globalization.CultureInfo("pt-BR");

            return new AdaptiveCard()
            {
                BackgroundImage = item.LinkImagemBackground,

                Body = new List<CardElement>()
                {
                    new Container()
                    {
                        Items = new List<CardElement>()
                        {
                            new TextBlock()
                            {
                                Text = item.Dia.Date == DateTime.Now.Date ? "Hoje" : UppercaseFirst(culture.DateTimeFormat.GetDayName(item.Dia.DayOfWeek)),
                                Weight = TextWeight.Bolder,
                                Size = TextSize.Medium,
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new Image()
                            {
                                Url = item.LinkImagemClima,
                                Size = ImageSize.Medium,
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new TextBlock()
                            {
                                Text = item.Dia.ToString("dd-MM-yyyy"),
                                Weight = TextWeight.Lighter,
                                Size = TextSize.Medium,
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new TextBlock()
                            {
                                Text = item.Tempo,
                                Weight = TextWeight.Normal,
                                Size = TextSize.Normal,
                                HorizontalAlignment = HorizontalAlignment.Center
                            },
                            new ColumnSet()
                            {
                                Columns = new List<Column>()
                                {
                                    new Column()
                                    {
                                        Items = new List<CardElement>()
                                        {
                                            new Container()
                                            {
                                                Items = new List<CardElement>()
                                                {
                                                    new TextBlock()
                                                    {
                                                        Text = "Temperatura",
                                                        Weight = TextWeight.Bolder,
                                                        Size = TextSize.Medium,
                                                        HorizontalAlignment = HorizontalAlignment.Center
                                                    },
                                                    new ColumnSet()
                                                    {
                                                        Columns = new List<Column>()
                                                        {
                                                            new Column()
                                                            {
                                                                Separation = SeparationStyle.Strong,
                                                                Items = new List<CardElement>()
                                                                {
                                                                    new TextBlock()
                                                                    {
                                                                        Text = "Mínima",
                                                                        Weight = TextWeight.Bolder,
                                                                        Size = TextSize.Medium,
                                                                        HorizontalAlignment = HorizontalAlignment.Center
                                                                    },
                                                                    new TextBlock()
                                                                    {
                                                                        Text = $"{item.Minima}°",
                                                                        Weight = TextWeight.Normal,
                                                                        Size = TextSize.Medium,
                                                                        HorizontalAlignment = HorizontalAlignment.Center
                                                                    }
                                                                }
                                                            },
                                                            new Column()
                                                            {
                                                                Items = new List<CardElement>()
                                                                {
                                                                    new TextBlock()
                                                                    {
                                                                        Text = "Máxima",
                                                                        Weight = TextWeight.Bolder,
                                                                        Size = TextSize.Medium,
                                                                        HorizontalAlignment = HorizontalAlignment.Center
                                                                    },
                                                                    new TextBlock()
                                                                    {
                                                                        Text = $"{item.Maxima}°",
                                                                        Weight = TextWeight.Normal,
                                                                        Size = TextSize.Medium,
                                                                        HorizontalAlignment = HorizontalAlignment.Center
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };
        }

        private string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);

            return new string(a);
        }
    }
}