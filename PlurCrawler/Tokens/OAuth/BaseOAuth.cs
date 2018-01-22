using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.OAuth
{
    public abstract class BaseOAuth
    {
        /// <summary>
        /// 로컬에서 사용되지 않고 있는 포트 번호를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Any, 0);
            listener.Start();

            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();

            return port;
        }

        /// <summary>
        /// 리다이렉트에서 사용되는 LoopBack IP + 포트 번호를 반환합니다.
        /// </summary>
        /// <returns></returns>
        public static string GetRedirectURI()
        {
            return $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
        }

        /// <summary>
        /// 주어진 길이만큼 URI-안전 데이터를 반환합니다.
        /// </summary>
        /// <param name="length">주어진 길이입니다. (해당 길이만큼 반환값이 길어집니다.)</param>
        /// <returns></returns>
        public static string RandomDataBase64url(uint length)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] bytes = new byte[length];
            rng.GetBytes(bytes);
            return Base64UrlEncodeNoPadding(bytes);
        }

        /// <summary>
        /// 입력된 버퍼에서 Base64URL을 가져옵니다.
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string base64 = Convert.ToBase64String(buffer);

            // Base64URL로 변환하는 과정
            base64 = base64.Replace("+", "-");
            base64 = base64.Replace("/", "_");
            // 여백 제거
            base64 = base64.Replace("=", "");

            return base64;
        }

        /// <summary>
        /// 입력된 문자열의 SHA256 해쉬를 반환합니다.
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static byte[] sha256(string inputString)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(inputString);
            SHA256Managed sha256 = new SHA256Managed();
            return sha256.ComputeHash(bytes);
        }
    }
}
