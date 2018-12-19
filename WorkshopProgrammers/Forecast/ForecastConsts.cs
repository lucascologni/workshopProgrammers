using System.Collections.Generic;

namespace WorkshopProgrammers.Forecast
{
    public class ForecastConsts
    {
        internal readonly static string CITY_ENDPOINT = "http://servicos.cptec.inpe.br/XML/listaCidades?city=";
        internal readonly static string CITY_FORECAST_ENDPOINT = "http://servicos.cptec.inpe.br/XML/cidade/";

        internal readonly static string SUNNY_CONDITION_IMAGE_LINK = "http://s1.cptec.inpe.br/webdop/static/resources/common/assets/images/icones/tempo/icones-grandes/cl.png";
        internal readonly static string CLOUDY_CONDITION_IMAGE_LINK = "http://s0.cptec.inpe.br/webdop/static/resources/common/assets/images/icones/tempo/icones-grandes/pn.png";
        internal readonly static string RAINY_CONDITION_IMAGE_LINK = "http://s1.cptec.inpe.br/webdop/static/resources/common/assets/images/icones/tempo/icones-grandes/pc.png";
        internal readonly static string SNOWY_CONDITION_IMAGE_LINK = "http://s1.cptec.inpe.br/webdop/static/resources/common/assets/images/icones/tempo/icones-grandes/g.png";
        internal readonly static string UNKNOWN_CONDITION_IMAGE_LINK = "http://s0.cptec.inpe.br/webdop/static/resources/common/assets/images/icones/tempo/icones-grandes/nd.png";

        internal static readonly string SUNNY_CONDITION_IMAGE_BACKGROUND_LINK = "http://www.sinergiacientifica.com.br/wp-content/uploads/2013/11/light-blue-background-924x693.jpg";
        internal static readonly string CLOUDY_CONDITION_IMAGE_BACKGROUND_LINK = "https://d2v9y0dukr6mq2.cloudfront.net/video/thumbnail/GYx5MFB/solid-overcast-sky-rush-away-time-lapse-shot-of-gray-clouds-cover-whole-skies_s0obqkgge_thumbnail-full03.png";
        internal static readonly string RAINY_CONDITION_IMAGE_BACKGROUND_LINK = "https://images.unsplash.com/photo-1523907148451-986fa7602675?ixlib=rb-1.2.1&auto=format&fit=crop&w=1050&q=80";
        internal static readonly string SNOWY_CONDITION_IMAGE_BACKGROUND_LINK = "https://img.freepik.com/free-vector/snowy-christmas-landscape_1048-9040.jpg?size=338&ext=jpg";
        internal static readonly string UNKNOWN_CONDITION_IMAGE_BACKGROUND_LINK = "https://www.pcr-online.biz/.image/t_share/MTUxOTMyMzM5Njc0MDMxNTMx/cloud-question-marks-sky-mainjpg.jpg";

        internal readonly static string[] SUNNY_CONDITION_LIST = new string[] { "ps", "cl" };
        internal readonly static string[] CLOUDY_CONDITION_LIST = new string[] { "in", "pn", "e", "n", "nv", "vn" };
        internal readonly static string[] RAINY_CONDITION_LIST = new string[] { "ec", "ci", "c", "pp", "cm", "cn", "pt", "pm", "np", "pc", "cv", "ch", "t", "pnt", "psc", "pcm", "pct", "pcn", "npt", "npn", "ncn", "nct", "ncm", "npm", "npp", "ct", "ppn", "ppt", "ppm" };
        internal readonly static string[] SNOWY_CONDITION_LIST = new string[] { "g", "ne" };
        internal readonly static string UNKNOWN_CONDITION_LIST = "nd";

        internal static readonly Dictionary<string, string> TIME_MAP = new Dictionary<string, string>
        {
            {"c", "Chuva"},
            {"t", "Tempestade"},
            {"g", "Geada"},
            {"n", "Nublado"},
            {"e", "Encoberto"},
            {"vn", "Variação de Nebulosidade"},
            {"ct", "Chuva a Tarde"},
            {"ec", "Encoberto com Chuvas Isoladas"},
            {"ci", "Chuvas Isoladas"},
            {"in", "Instável"},
            {"pp", "Poss. de Pancadas de Chuva"},
            {"cm", "Chuva pela Manhã"},
            {"cn", "Chuva a Noite"},
            {"pt", "Pancadas de Chuva a Tarde"},
            {"pm", "Pancadas de Chuva pela Manhã"},
            {"np", "Nublado e Pancadas de Chuva"},
            {"pc", "Pancadas de Chuva"},
            {"pn", "Parcialmente Nublado"},
            {"cv", "Chuvisco"},
            {"ch", "Chuvoso"},
            {"ps", "Predomínio de Sol"},
            {"cl", "Céu Claro"},
            {"nv", "Nevoeiro"},
            {"ne", "Neve"},
            {"nd", "Não Definido"},
            {"pnt", "Pancadas de Chuva a Noite"},
            {"psc", "Possibilidade de Chuva"},
            {"pcm", "Possibilidade de Chuva pela Manhã"},
            {"pct", "Possibilidade de Chuva a Tarde"},
            {"pcn", "Possibilidade de Chuva a Noite"},
            {"npt", "Nublado com Pancadas a Tarde"},
            {"npn", "Nublado com Pancadas a Noite"},
            {"ncn", "Nublado com Poss. de Chuva a Noite"},
            {"nct", "Nublado com Poss. de Chuva a Tarde"},
            {"ncm", "Nubl. c/ Poss. de Chuva pela Manhã"},
            {"npm", "Nublado com Pancadas pela Manhã"},
            {"npp", "Nublado com Possibilidade de Chuva"},
            {"ppn", "Poss. de Panc. de Chuva a Noite"},
            {"ppt", "Poss. de Panc. de Chuva a Tarde"},
            {"ppm", "Poss. de Panc. de Chuva pela Manhã"}
        };
    }
}