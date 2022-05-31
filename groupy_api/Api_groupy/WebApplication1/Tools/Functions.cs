using System;
using System.Globalization;
using System.Web;

namespace WebApplication1.Tools
{
    public class Functions
    {
        //##### funções auxiliares para retorno de tratamento de filtros #####
        //trata e retorna string de um query parameter recebido
        public static string GetTextFromQueryParameter(string parameters, string key)
        {
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(parameters);

            string paramText = query.Get(key);
            if (string.IsNullOrEmpty(paramText))
                paramText = string.Empty;

            return paramText;
        }

        //trata e retorna int de um query parameter recebido ou então retorna null
        public static dynamic GetIntFromQueryParameter(string parameters, string key)
        {
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(parameters);

            string paramText = query.Get(key);
            if (string.IsNullOrEmpty(paramText))
                return null;

            return int.Parse(paramText);
        }

        //trata e retorna boolean de um query parameter recebido
        public static bool GetBoolFromQueryParameter(string parameters, string key)
        {
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(parameters);

            bool paramBool;
            string paramText = query.Get(key);
            if (string.IsNullOrEmpty(paramText))
                paramBool = false;
            else
                paramBool = bool.Parse(paramText);

            return paramBool;
        }

        //trata e retorna datetime de um query parameter recebido
        public static DateTime? GetDataHoraFromQueryParameter(string parameters, string key)
        {
            try
            {
                //pega data dos params
                System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(parameters);
                string dataHora = query.Get(key);
                //se não tiver já retorna null
                if (string.IsNullOrEmpty(dataHora)) return null;

                //coloca hora final do dia para filtrar
                if (key.ToLower().Contains("final"))
                    dataHora = $"{dataHora.Substring(0, 10)} 23:59:59";
                else
                    dataHora = $"{dataHora.Substring(0, 10)} 00:00:00";

                return DateTime.Parse(dataHora, CultureInfo.CreateSpecificCulture("pt-BR"));
            }
            catch (Exception ex)
            {
                throw new Exception("Error Functions.GetDataHoraFromQueryParameter() => " + ex.Message);
            }
        }

        //trata e retorna uma lista de um query parameter recebido
        public static string[] GetListFromQueryParameter(string parameters, string key)
        {
            System.Collections.Specialized.NameValueCollection query = HttpUtility.ParseQueryString(parameters);

            string paramStatus = query.Get(key);
            string[] listParamStatus = new string[] { };
            if (!string.IsNullOrEmpty(paramStatus))
                listParamStatus = paramStatus.Split(',');

            return listParamStatus;
        }
    }
}
